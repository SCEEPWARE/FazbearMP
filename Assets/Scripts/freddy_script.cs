using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement du personnage
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de d�part des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    public Vector2 initialPosition = new Vector2(0, 0);
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    private bool inputsEnabled = true; // Variable pour activer/d�sactiver les inputs
    private Vector2 moveDirection; // Direction du mouvement
    public float moveAmount = 20f; // Distance � d�placer � chaque �tape
    public float moveInterval = 0.5f; // Intervalle de mouvement en secondes

    void Start()
    {
        // Appliquer la position initiale au transform de l'objet
        transform.position = initialPosition;

        // R�cup�rer le Rigidbody2D attach� � cet objet
        rb = GetComponent<Rigidbody2D>();

        // D�marrer la coroutine pour le mouvement
        StartCoroutine(MovePlayer());
    }

    void Update()
    {
        // G�rer l'activation/d�sactivation des inputs avec la touche P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleInputs(); // Appel de la fonction pour activer/d�sactiver les inputs
        }

        // G�rer le tir si les inputs sont activ�s
        if (inputsEnabled && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // G�rer le mouvement bas� sur les inputs
        ProcessInputs();
    }

    // Coroutine pour le mouvement
    private IEnumerator MovePlayer()
    {
        while (true)
        {
            if (inputsEnabled) // V�rifier si les inputs sont activ�s
            {
                // Appliquer le mouvement
                rb.position += moveDirection * (moveAmount / 100f); // D�placer de 20 pixels
            }
            yield return new WaitForSeconds(moveInterval); // Attendre 0.5 secondes
        }
    }

    // G�rer le mouvement
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // D�finir la direction de mouvement
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Si on ne bouge pas, ne pas appeler MovePlayer() pour �viter des mouvements inutiles
        if (moveDirection.magnitude > 0)
        {
            // Si le personnage se d�place, mettre � jour la position
            rb.position += moveDirection * (moveAmount / 100f * Time.deltaTime); // D�placement
        }
    }

    // Fonction pour tirer un projectile
    void Shoot()
    {
        // Cr�er une instance du projectile au niveau de firePoint
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

        // Ajouter un mouvement au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Le projectile se d�place vers la droite
    }

    // Fonction pour activer/d�sactiver les inputs
    void ToggleInputs()
    {
        inputsEnabled = !inputsEnabled; // Inverse l'�tat de la variable
    }
}