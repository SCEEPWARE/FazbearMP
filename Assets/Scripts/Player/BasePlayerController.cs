using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /*
    A FAIRE:
    S‚parer la main Camera du RigidBody (en mettant le joueur et la cam‚ra dans le mˆme pr‚fab mais s‚par‚s)
    Faire en sorte de pouvoir tourner la cam‚ra sur l'axe X.
    */

    public bool inputEnabled = false;
    
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float mouseSensitivity = 50f; 




    // Variables autres
    private Rigidbody rb;
    private Vector3 moveDir;

    private float xRot = 0, yRot = 0;
    
    void Start()
    {
        // Initialisation des variables
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        // Initialisation variable
        

        // Mouvement clavier
        float keyX = Input.GetAxisRaw("Horizontal");
        float keyY = Input.GetAxisRaw("Vertical");
        moveDir = (transform.forward * keyY + transform.right * keyX).normalized;

        // Mouvement souris
        yRot += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        xRot -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
    }

    void FixedUpdate(){
        // Interaction avec le moteur physique

        // Mouvement clavier
        rb.velocity = moveDir * moveSpeed;

        // Mouvement souris
        rb.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
