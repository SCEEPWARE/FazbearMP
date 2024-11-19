using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSpawner : MonoBehaviour
{
    public GameObject pizzaPrefab;
    public float spawnInterval = 5f;

    private void Start()
    {
        InvokeRepeating("SpawnPizza", 0f, spawnInterval);
    }

    private void SpawnPizza()
    {
        Instantiate(pizzaPrefab, transform.position, Quaternion.identity);
    }
}
