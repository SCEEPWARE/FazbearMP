using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CameraController))]
public class BasePlayerController : MonoBehaviour
{
    /*
    A FAIRE:
    S‚parer la main Camera du RigidBody (en mettant le joueur et la cam‚ra dans le mˆme pr‚fab mais s‚par‚s)
    Faire en sorte de pouvoir tourner la cam‚ra sur l'axe X.
    */


    [Header("ParamŠtres des mouvements", order = 0)]
    public bool inputEnabled = false;
    public bool cameraEnabled = true;

    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    [SerializeField] protected float maxStamina;
    protected float stamina{
        get{
            return _stamina;
        } set{
            _stamina = Mathf.Clamp(value, 0f, maxStamina);
        }
    }
    [SerializeField] protected float staminaRegainTime;
    [SerializeField] protected float staminaRegainSpeed;
    protected float staminaWaitTime;
    [NonSerialized] public float moveSpeed;

    // Variables autres
    public Rigidbody rb;
    public Vector3 moveDir;
    [SerializeField] private LayerMask levelLayer;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] protected Animator animator;

    [Header("Informations", order = 1)]
    public bool isGrounded;
    [SerializeField] protected float _stamina;
    [SerializeField] public enum ControlType{
        controller,
        keyboard
    };

    [SerializeField] public ControlType controlChosen;
    [SerializeField] private LayerMask itemLayer;

    [SerializeField] private GameObject _item;
    [SerializeField] private Transform itemSpawnPos;
    [SerializeField] private bool canAccessArcade;
    public GameObject item{
        get{
            return _item;
        }
        set{
            _item = Instantiate(value, itemSpawnPos);
            _item.layer = LayerMask.NameToLayer(objectLayer);
            foreach(Transform child in _item.transform){
                child.gameObject.layer = LayerMask.NameToLayer(objectLayer);
            }
            _item.GetComponent<ItemBehaviour>().owner = gameObject;
            // _item.GetComponent<ItemBehaviour>().PostInitialization();
        }
    }
    [SerializeField] private String objectLayer;

    [SerializeField] private GameObject staminaBar;
    [SerializeField] private float maxBarSize;

    protected virtual void Start()
    {
        // Initialisation des variables
        rb = GetComponent<Rigidbody>();
        item = item;
    }

    
    protected virtual void Update()
    {
        if(!inputEnabled){
            rb.velocity = new Vector3(0,0,0);
            return;
        }
        float keyX = 0, keyY = 0; 
        // Initialisation variable
        

        // Mouvement clavier
        if(controlChosen == ControlType.keyboard){
            keyX = Input.GetAxisRaw("Horizontal");
            keyY = Input.GetAxisRaw("Vertical");

            if(item != null){
                if(Input.GetButtonDown("Fire1")){
                    item.GetComponent<ItemBehaviour>().MainFire();
                }
                if(Input.GetButtonDown("Fire2")){
                    item.GetComponent<ItemBehaviour>().SecondaryFire();
                }
            }

            moveSpeed = Input.GetButton("Sprint") && stamina > 0 ? runSpeed : walkSpeed;
        }

        // Mouvement manette
        if(controlChosen == ControlType.controller){
            keyX = Input.GetAxisRaw("HorizontalController");
            keyY = Input.GetAxisRaw("VerticalController");

            if(item != null){
                if(Input.GetButtonDown("Fire1Controller")){
                    item.GetComponent<ItemBehaviour>().MainFire();
                }
                if(Input.GetButtonDown("Fire2Controller")){
                    item.GetComponent<ItemBehaviour>().SecondaryFire();
                }
            }

            moveSpeed = Input.GetButton("SprintController") && stamina > 0 ? runSpeed : walkSpeed;
        }

        moveDir = (transform.forward * keyY + transform.right * keyX).normalized;

        moveSpeed = moveDir.magnitude > 0? moveSpeed : 0;

        if(moveSpeed == runSpeed && moveDir.magnitude > 0){
            stamina -= Time.deltaTime;
            // Debug.Log(stamina);
            staminaWaitTime = staminaRegainTime;
        }

        if(moveSpeed != runSpeed){
            staminaWaitTime -= Time.deltaTime;
            if(staminaWaitTime < 0){
                stamina += Time.deltaTime * staminaRegainSpeed;
                // Debug.Log(stamina);
            }
        }

        // Check utilisation de l'arcade

        if(Input.GetKeyDown(KeyCode.E) && canAccessArcade){
            if (Physics.Raycast(GetComponent<CameraController>()._cameraObject.transform.position, GetComponent<CameraController>()._cameraObject.transform.forward, out RaycastHit hit, 5f, itemLayer)){
                Debug.Log(hit.collider);
                if(hit.collider.TryGetComponent<ActivateArcade>(out ActivateArcade component)){
                    component.AccessArcade(gameObject);
                }
            }
        }










        // graphismes

        staminaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(maxBarSize * stamina/maxStamina, staminaBar.GetComponent<RectTransform>().sizeDelta.y);

        staminaBar.SetActive(stamina == maxStamina? false : true);

        animator.SetFloat("Speed", moveSpeed);
    }

    protected virtual void FixedUpdate(){
        // Interaction avec le moteur physique

        // Mouvement
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        // Check si le joueur touche le sol (sert … rien depuis que la feature de saut a ‚t‚ d‚gag‚e de ce monde o7)
        // isGrounded = Physics.CheckSphere(groundCheckPosition.position, -0.1f, levelLayer);


        // On fait un raycast pour v‚rifier qu'on peut ramasser un item (abandonn‚, feature retir‚)
        // if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f, itemLayer))
        // {
        //     if (Input.GetKey(KeyCode.E) && item == null)
        //     {
        //         Debug.Log("A ramass‚ : " + hit.collider.name);
        //         hit.collider.gameObject.GetComponent<PickableItem>().PickUp(gameObject);
        //     }
        // }
    }
}