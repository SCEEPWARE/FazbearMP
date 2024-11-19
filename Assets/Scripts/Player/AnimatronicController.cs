using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicController : BasePlayerController
{
    [Header("Paramätres de l'animatronique", order = 3)]
    [SerializeField] private float rayDistance;
    public Collider headCollider;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(!inputEnabled){
            return;
        }

        // Pour tuer l'enfant
        if(Input.GetButtonDown("Fire1")){
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.TryGetComponent<ChildController>(out ChildController childCtrl))
                {
                    childCtrl.inputEnabled = false;
                    // Destroy(childCtrl.gameObject);
                }
            }
        }
    }

    public void Stun(){
        Debug.Log("Step Animatronic I'm stun!!");
    }
}
