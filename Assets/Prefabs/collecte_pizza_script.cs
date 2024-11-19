using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCollector : MonoBehaviour
{
    public int pizzaCount = 0; // Compteur de pizzas

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pizza") && pizzaCount < 6) // V�rifier si l'objet collect� est une pizza
        {
            pizzaCount++; // Augmenter le compteur
            Destroy(collision.gameObject); // Supprimer la pizza collect�e
        }
    }
}
