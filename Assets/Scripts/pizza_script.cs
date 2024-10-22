using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizza_script : MonoBehaviour
{
    public float lifeTime = 3f;
    public int damage = 1; // Nombre de d�g�ts que le projectile inflige

    void Start()
    {
        // D�truire le projectile apr�s un certain temps
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Collision d�tect�e avec : " + hitInfo.name); // Pour voir si la collision est d�tect�e

        // V�rifier si l'objet touch� est un ennemi en cherchant le script EnemyHealth
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            // Infliger des d�g�ts � l'ennemi
            enemy.TakeDamage(damage);
        }

        // D�truire le projectile apr�s la collision
        Destroy(gameObject);
    }
}