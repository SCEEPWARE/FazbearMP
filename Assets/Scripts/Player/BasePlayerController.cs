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

    protected virtual void Start()
    {
        // Initialisation des variables
        rb = GetComponent<Rigidbody>();
    }

    
    protected virtual void Update()
    {
        if(!inputEnabled){
            return;
        }
        // Initialisation variable
        

        // Mouvement clavier
        float keyX = Input.GetAxisRaw("Horizontal");
        float keyY = Input.GetAxisRaw("Vertical");
        moveDir = (transform.forward * keyY + transform.right * keyX).normalized;



        moveSpeed = Input.GetButton("Sprint") && stamina > 0 ? runSpeed : walkSpeed;
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

        animator.SetFloat("Speed", moveSpeed);


    }

    protected virtual void FixedUpdate(){
        // Interaction avec le moteur physique

        // Mouvement clavier
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        // Check si le joueur touche le sol
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, -0.1f, levelLayer);
        
    }
}