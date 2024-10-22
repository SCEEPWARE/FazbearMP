using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du personnage
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de départ des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    private Vector2 moveDirection; // Direction du mouvement
    public Vector2 initialPosition = new Vector2(0, 0);
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    private bool inputsEnabled = true; // Variable pour activer/désactiver les inputs

    void Start()
    {
        // Appliquer la position initiale au transform de l'objet
        transform.position = initialPosition;

        // Récupérer le Rigidbody2D attaché à cet objet
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gérer l'activation/désactivation des inputs avec la touche P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleInputs(); // Appel de la fonction pour activer/désactiver les inputs
        }

        // Gérer le mouvement et le tir du personnage si les inputs sont activés
        if (inputsEnabled)
        {
            ProcessInputs();

            // Tirs avec la touche Espace
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    // Fonction pour activer/désactiver les inputs
    void ToggleInputs()
    {
        inputsEnabled = !inputsEnabled; // Inverse l'état de la variable
    }

    // Gérer le mouvement
    void ProcessInputs()
    {
        // Récupérer les entrées de mouvement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Définir la direction de mouvement
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Appliquer le mouvement au personnage
        rb.velocity = moveSpeed * moveDirection; // Pas besoin de multiplier par Time.deltaTime ici pour la vélocité
    }

    // Fonction pour tirer un projectile
    void Shoot()
    {
        // Créer une instance du projectile au niveau de firePoint
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

        // Ajouter un mouvement au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Le projectile se déplace vers la droite
    }
}
