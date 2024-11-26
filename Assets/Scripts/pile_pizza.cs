using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPilePizzas : MonoBehaviour
{
    public GameObject prefabPizza; // Le prefab de la pizza
    public Transform positionMain; // Position de la main o� les pizzas s'empilent
    public float hauteurPizza = 0.2f; // Hauteur entre deux pizzas dans la pile

    private List<GameObject> pilePizzas = new List<GameObject>(); // Liste des pizzas dans la pile

    // Fonction pour ajouter une pizza � la pile
    public void AjouterPizza()
    {
        // Cr�er une nouvelle pizza
        GameObject nouvellePizza = Instantiate(prefabPizza, positionMain);

        // Calculer la position de la nouvelle pizza dans la pile
        Vector3 positionPizza = positionMain.position + Vector3.up * (pilePizzas.Count * hauteurPizza);

        // Placer la pizza � cette position
        nouvellePizza.transform.position = positionPizza;

        // Ajouter la pizza � la liste
        pilePizzas.Add(nouvellePizza);
    }

    // Fonction pour retirer une pizza de la pile
    public void RetirerPizza()
    {
        if (pilePizzas.Count > 0)
        {
            // R�cup�rer la derni�re pizza de la pile
            GameObject dernierePizza = pilePizzas[pilePizzas.Count - 1];

            // Retirer la pizza de la liste
            pilePizzas.RemoveAt(pilePizzas.Count - 1);

            // Lancer la pizza ou appliquer un effet
            LancerPizza(dernierePizza);
        }
    }

    // Fonction pour lancer une pizza
    private void LancerPizza(GameObject pizza)
    {
        // Exemple : Ajouter une force pour simuler un tir
        Rigidbody2D corpsRigide = pizza.GetComponent<Rigidbody2D>();
        if (corpsRigide != null)
        {
            corpsRigide.isKinematic = false; // Activer la physique si n�cessaire
            corpsRigide.AddForce(Vector2.right * 5f, ForceMode2D.Impulse); // Exemple de direction
        }

        // D�truire la pizza apr�s un certain temps pour �viter de saturer la sc�ne
        Destroy(pizza, 2f);
    }
}
