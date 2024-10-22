using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement du personnage
    public GameObject projectilePrefab; // Le prefab du projectile
    public Transform firepoint; // Point de d�part des tirs
    public float projectileSpeed = 10f; // Vitesse du projectile
    private Vector2 moveDirection; // Direction du mouvement
    public Vector2 initialPosition = new Vector2(0, 0);
    private Rigidbody2D rb; // Utilisation correcte du Rigidbody2D
    private bool inputsEnabled = true; // Variable pour activer/d�sactiver les inputs

    void Start()
    {
        // Appliquer la position initiale au transform de l'objet
        transform.position = initialPosition;

        // R�cup�rer le Rigidbody2D attach� � cet objet
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // G�rer l'activation/d�sactivation des inputs avec la touche P
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleInputs(); // Appel de la fonction pour activer/d�sactiver les inputs
        }

        // G�rer le mouvement et le tir du personnage si les inputs sont activ�s
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

    // Fonction pour tirer un projectile
    void Shoot()
    {
        // Cr�er une instance du projectile au niveau de firePoint
        GameObject projectile = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);

        // Ajouter un mouvement au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = new Vector2(projectileSpeed, 0); // Le projectile se d�place vers la droite
    }
}
