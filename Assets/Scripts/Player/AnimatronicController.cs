using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicController : BasePlayerController
{
    [Header("Paramätres de l'animatronique", order = 3)]
    [SerializeField] private float rayDistance;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if(Input.GetButtonDown("Fire1")){
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,rayDistance)){
                if(hit.collider.TryGetComponent<ChildController>(out ChildController childCtrl)){
                    childCtrl.inputEnabled = false;
                    Destroy(childCtrl.gameObject);
                }
            }
        }
    }
}
