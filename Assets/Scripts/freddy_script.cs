using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement du personnage
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de départ des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    public Vector2 initialPosition = new Vector2(0, 0);
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    private bool inputsEnabled = true; // Variable pour activer/désactiver les inputs
    private Vector2 moveDirection; // Direction du mouvement
    public float moveAmount = 20f; // Distance à déplacer à chaque étape
    public float moveInterval = 0.5f; // Intervalle de mouvement en secondes

    void Start()
    {
        // Appliquer la position initiale au transform de l'objet
        transform.position = initialPosition;

        // Récupérer le Rigidbody2D attaché à cet objet
        rb = GetComponent<Rigidbody2D>();

        // Démarrer la coroutine pour le mouvement
        StartCoroutine(MovePlayer());
    }

    void Update()
    {
        // Gérer l'activation/désactivation des inputs avec la touche P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleInputs(); // Appel de la fonction pour activer/désactiver les inputs
        }

        // Gérer le tir si les inputs sont activés
        if (inputsEnabled && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        // Gérer le mouvement basé sur les inputs
        ProcessInputs();
    }

    // Coroutine pour le mouvement
    private IEnumerator MovePlayer()
    {
        while (true)
        {
            if (inputsEnabled) // Vérifier si les inputs sont activés
            {
                // Appliquer le mouvement
                rb.position += moveDirection * (moveAmount / 100f); // Déplacer de 20 pixels
            }
            yield return new WaitForSeconds(moveInterval); // Attendre 0.5 secondes
        }
    }

    // Gérer le mouvement
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Définir la direction de mouvement
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Si on ne bouge pas, ne pas appeler MovePlayer() pour éviter des mouvements inutiles
        if (moveDirection.magnitude > 0)
        {
            // Si le personnage se déplace, mettre à jour la position
            rb.position += moveDirection * (moveAmount / 100f * Time.deltaTime); // Déplacement
        }
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

    // Fonction pour activer/désactiver les inputs
    void ToggleInputs()
    {
        inputsEnabled = !inputsEnabled; // Inverse l'état de la variable
    }
}