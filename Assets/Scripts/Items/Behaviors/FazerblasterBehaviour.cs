using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FazerblasterBehaviour : ItemBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    public override void MainFire()
    {
        if(Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit,100f, playerLayer)){
            if(TryGetComponent<AnimatronicController>(out AnimatronicController animCtrl)){
                Debug.Log(hit.collider.name);
                if(hit.collider == animCtrl.headCollider){
                    animCtrl.Stun();
                }
            }
        }
    }
}
