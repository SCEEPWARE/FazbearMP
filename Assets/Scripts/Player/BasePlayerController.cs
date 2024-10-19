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
    
    [SerializeField] protected float moveSpeed = 3f;

    // Variables autres
    public Rigidbody rb;
    public Vector3 moveDir;
    
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


    }

    protected virtual void FixedUpdate(){
        // Interaction avec le moteur physique

        // Mouvement clavier
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
        
    }
}
