using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector2 initialPosition = new Vector2(-9, 0);
    void Start()
    {
        // Appliquer la position initiale au transform de l'objet
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
