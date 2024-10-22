using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza_script : MonoBehaviour
{
    public float lifeTime = 3f;
    public int damage = 1; // Nombre de dégâts que le projectile inflige

    void Start()
    {
        // Détruire le projectile après un certain temps
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Collision détectée avec : " + hitInfo.name); // Pour voir si la collision est détectée

        // Vérifier si l'objet touché est un ennemi en cherchant le script EnemyHealth
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            // Infliger des dégâts à l'ennemi
            enemy.TakeDamage(damage);
        }

        // Détruire le projectile après la collision
        Destroy(gameObject);
    }
}