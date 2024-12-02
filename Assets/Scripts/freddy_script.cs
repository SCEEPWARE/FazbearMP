using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables pour le mouvement
    public float moveSpeed = 5f; // Vitesse de déplacement du personnage
    private Vector2 moveDirection; // Direction du mouvement
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    public Vector2 initialPosition = new Vector2(0, 0);

    // Variables pour les pizzas
    public int pizzaCount = 0; // Compteur de pizzas
    public int maxPizzas = 6; // Nombre maximum de pizzas
    public GameObject prefabPizza; // Prefab de la pizza
    public Transform pizzaPilePosition; // Position de départ de la pile (ex : main du personnage)
    public float pizzaSpacing = 0.5f; // Espacement entre chaque pizza dans la pile
    public float offsetX = 2f; // Décalage en X par rapport au personnage
    public float offsetY = 0f; // Décalage en Y par rapport au personnage
    private List<GameObject> pizzaPile = new List<GameObject>(); // Liste des pizzas dans la pile
    private float tirCooldown = 0f; // Timer pour contrôler le cooldown entre chaque tir
    public float tempsEntreTirs = 0.5f; // Temps entre chaque tir (0.5 sec pour un tir toutes les 2 secondes)

    // Variables pour le tir de projectiles
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de départ des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    public bool inputsEnabled = true; // Variable pour activer/désactiver les inputs

    void Start()
    {
        // Récupérer le Rigidbody2D attaché à cet objet
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Vérifier si le cooldown est terminé avant d'autoriser un nouveau tir
        if (tirCooldown <= 0f)
        {
            // Gérer l'activation/désactivation des inputs avec la touche P
            if (Input.GetKeyDown(KeyCode.P))
            {
                ToggleInputs(); // Appel de la fonction pour activer/désactiver les inputs
            }

            // Gérer le tir du personnage
            if (inputsEnabled && Input.GetKeyDown(KeyCode.Space) && pizzaCount > 0)
            {
                TirerPizza(); // Appeler la méthode pour retirer une pizza
                tirCooldown = tempsEntreTirs; // Réinitialiser le cooldown après chaque tir
            }
        }

        // Décrémenter le cooldown pour permettre un nouveau tir après un certain temps
        if (tirCooldown > 0f)
        {
            tirCooldown -= Time.deltaTime; // Réduire le cooldown basé sur le temps écoulé
        }

        // Gérer le mouvement si les inputs sont activés
        if (inputsEnabled)
        {
            ProcessInputs(); // Processus du mouvement
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

    // Fonction pour tirer une pizza
    void TirerPizza()
    {
        // Créer un projectile au point de départ du tir
        ShootProjectile();

        // Réduire le compteur de pizzas et retirer la dernière pizza de la pile visuelle
        pizzaCount--; // Réduire le compteur
        RetirerPizzaVisuelle(); // Retirer visuellement une pizza de la pile
    }

    // Fonction pour instancier un projectile
    void ShootProjectile()
    {
        // Créer une instance du projectile au niveau de firePoint
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

        // Ajouter un mouvement au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Le projectile se déplace vers la droite
    }

    // Fonction pour ajouter une pizza visuellement dans la pile
    private void AjouterPizzaVisuelle()
    {
        // Calculer la position de la nouvelle pizza avec l'offset et l'espacement
        Vector3 nouvellePosition = pizzaPilePosition.position +
                                   new Vector3(offsetX, offsetY + pizzaPile.Count * pizzaSpacing, 0);

        // Créer la pizza visuelle
        GameObject nouvellePizza = Instantiate(prefabPizza, nouvellePosition, Quaternion.identity);

        // Faire de la pizza un enfant de l'objet pour qu'elle bouge avec le personnage
        nouvellePizza.transform.parent = pizzaPilePosition;

        // Ajouter la pizza à la liste
        pizzaPile.Add(nouvellePizza);
    }

    // Fonction pour retirer une pizza visuellement de la pile
    public void RetirerPizzaVisuelle()
    {
        // Vérifier s'il reste des pizzas
        if (pizzaPile.Count > 0)
        {
            // Récupérer la dernière pizza ajoutée
            GameObject dernierePizza = pizzaPile[pizzaPile.Count - 1];

            // La retirer de la liste
            pizzaPile.Remove(dernierePizza);

            // La détruire (et par extension détruire le sprite)
            Destroy(dernierePizza);
        }
    }

    // Fonction pour ajouter une pizza à la pile quand le joueur ramasse une pizza
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifier si l'objet collecté est une pizza et que la pile n'est pas pleine
        if (collision.CompareTag("Pizza") && pizzaCount < maxPizzas)
        {
            pizzaCount++; // Augmenter le compteur
            Destroy(collision.gameObject); // Supprimer la pizza collectée
            AjouterPizzaVisuelle(); // Ajouter visuellement une pizza à la pile
        }
    }
}
