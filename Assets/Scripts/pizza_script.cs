using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza_script : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime = 3f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Optionnel : tu peux vérifier si le projectile touche un certain type d'objet (par exemple, un ennemi)
        if (hitInfo.CompareTag("Enemy"))
        {
            // Code pour infliger des dégâts à l'ennemi, si tu as un système de santé
            Destroy(hitInfo.gameObject); // Détruire l'ennemi
        }

        // Détruire le projectile après la collision
        Destroy(gameObject);
    }


}
