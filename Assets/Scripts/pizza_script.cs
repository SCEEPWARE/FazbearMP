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
        // Optionnel : tu peux v�rifier si le projectile touche un certain type d'objet (par exemple, un ennemi)
        if (hitInfo.CompareTag("Enemy"))
        {
            // Code pour infliger des d�g�ts � l'ennemi, si tu as un syst�me de sant�
            Destroy(hitInfo.gameObject); // D�truire l'ennemi
        }

        // D�truire le projectile apr�s la collision
        Destroy(gameObject);
    }


}
