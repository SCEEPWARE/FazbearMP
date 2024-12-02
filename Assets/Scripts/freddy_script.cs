using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables pour le mouvement
    public float moveSpeed = 5f; // Vitesse de d�placement du personnage
    private Vector2 moveDirection; // Direction du mouvement
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    public Vector2 initialPosition = new Vector2(0, 0);

    // Variables pour les pizzas
    public int pizzaCount = 0; // Compteur de pizzas
    public int maxPizzas = 6; // Nombre maximum de pizzas
    public GameObject prefabPizza; // Prefab de la pizza
    public Transform pizzaPilePosition; // Position de d�part de la pile (ex : main du personnage)
    public float pizzaSpacing = 0.5f; // Espacement entre chaque pizza dans la pile
    public float offsetX = 2f; // D�calage en X par rapport au personnage
    public float offsetY = 0f; // D�calage en Y par rapport au personnage
    private List<GameObject> pizzaPile = new List<GameObject>(); // Liste des pizzas dans la pile
    private float tirCooldown = 0f; // Timer pour contr�ler le cooldown entre chaque tir
    public float tempsEntreTirs = 0.5f; // Temps entre chaque tir (0.5 sec pour un tir toutes les 2 secondes)

    // Variables pour le tir de projectiles
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de d�part des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    public bool inputsEnabled = true; // Variable pour activer/d�sactiver les inputs

    void Start()
    {
        // R�cup�rer le Rigidbody2D attach� � cet objet
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // V�rifier si le cooldown est termin� avant d'autoriser un nouveau tir
        if (tirCooldown <= 0f)
        {
            // G�rer l'activation/d�sactivation des inputs avec la touche P
            if (Input.GetKeyDown(KeyCode.P))
            {
                ToggleInputs(); // Appel de la fonction pour activer/d�sactiver les inputs
            }

            // G�rer le tir du personnage
            if (inputsEnabled && Input.GetKeyDown(KeyCode.Space) && pizzaCount > 0)
            {
                TirerPizza(); // Appeler la m�thode pour retirer une pizza
                tirCooldown = tempsEntreTirs; // R�initialiser le cooldown apr�s chaque tir
            }
        }

        // D�cr�menter le cooldown pour permettre un nouveau tir apr�s un certain temps
        if (tirCooldown > 0f)
        {
            tirCooldown -= Time.deltaTime; // R�duire le cooldown bas� sur le temps �coul�
        }

        // G�rer le mouvement si les inputs sont activ�s
        if (inputsEnabled)
        {
            ProcessInputs(); // Processus du mouvement
        }
    }

    // Fonction pour activer/d�sactiver les inputs
    void ToggleInputs()
    {
        inputsEnabled = !inputsEnabled; // Inverse l'�tat de la variable
    }

    // G�rer le mouvement
    void ProcessInputs()
    {
        // R�cup�rer les entr�es de mouvement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // D�finir la direction de mouvement
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Appliquer le mouvement au personnage
        rb.velocity = moveSpeed * moveDirection; // Pas besoin de multiplier par Time.deltaTime ici pour la v�locit�
    }

    // Fonction pour tirer une pizza
    void TirerPizza()
    {
        // Cr�er un projectile au point de d�part du tir
        ShootProjectile();

        // R�duire le compteur de pizzas et retirer la derni�re pizza de la pile visuelle
        pizzaCount--; // R�duire le compteur
        RetirerPizzaVisuelle(); // Retirer visuellement une pizza de la pile
    }

    // Fonction pour instancier un projectile
    void ShootProjectile()
    {
        // Cr�er une instance du projectile au niveau de firePoint
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

        // Ajouter un mouvement au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Le projectile se d�place vers la droite
    }

    // Fonction pour ajouter une pizza visuellement dans la pile
    private void AjouterPizzaVisuelle()
    {
        // Calculer la position de la nouvelle pizza avec l'offset et l'espacement
        Vector3 nouvellePosition = pizzaPilePosition.position +
                                   new Vector3(offsetX, offsetY + pizzaPile.Count * pizzaSpacing, 0);

        // Cr�er la pizza visuelle
        GameObject nouvellePizza = Instantiate(prefabPizza, nouvellePosition, Quaternion.identity);

        // Faire de la pizza un enfant de l'objet pour qu'elle bouge avec le personnage
        nouvellePizza.transform.parent = pizzaPilePosition;

        // Ajouter la pizza � la liste
        pizzaPile.Add(nouvellePizza);
    }

    // Fonction pour retirer une pizza visuellement de la pile
    public void RetirerPizzaVisuelle()
    {
        // V�rifier s'il reste des pizzas
        if (pizzaPile.Count > 0)
        {
            // R�cup�rer la derni�re pizza ajout�e
            GameObject dernierePizza = pizzaPile[pizzaPile.Count - 1];

            // La retirer de la liste
            pizzaPile.Remove(dernierePizza);

            // La d�truire (et par extension d�truire le sprite)
            Destroy(dernierePizza);
        }
    }

    // Fonction pour ajouter une pizza � la pile quand le joueur ramasse une pizza
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifier si l'objet collect� est une pizza et que la pile n'est pas pleine
        if (collision.CompareTag("Pizza") && pizzaCount < maxPizzas)
        {
            pizzaCount++; // Augmenter le compteur
            Destroy(collision.gameObject); // Supprimer la pizza collect�e
            AjouterPizzaVisuelle(); // Ajouter visuellement une pizza � la pile
        }
    }
}
