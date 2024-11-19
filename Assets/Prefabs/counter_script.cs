using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaCounterUI : MonoBehaviour
{
    public PizzaCollector pizzaCollector;
    public Text pizzaCounterText;

    private void Update()
    {
        pizzaCounterText.text = "Pizzas: " + pizzaCollector.pizzaCount;
    }
}