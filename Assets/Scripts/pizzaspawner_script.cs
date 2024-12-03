using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    public GameObject pizzaPrefab;
    private GameObject pizzaSpawned;
    public float spawnInterval = 3f;
    float delay;

    private void Update()
    {
        if(pizzaSpawned == null && delay < 0){
            pizzaSpawned = Instantiate(pizzaPrefab, transform.position, Quaternion.identity);
        } else if(pizzaSpawned != null){
            delay = spawnInterval;
        }
        delay -= Time.deltaTime;
    }
}
