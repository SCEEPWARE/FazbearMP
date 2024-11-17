using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class FazerblasterBehaviour : ItemBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    public int ammo;
    public override void MainFire()
    {
        if(ammo <= 0){ // on v‚rifie qu'on ait assez de munition
            return;
        }
        --ammo; // on en enlŠve (si on tire)
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f, playerLayer)){
            if(hit.collider.TryGetComponent<AnimatronicController>(out AnimatronicController animCtrl)){
                Debug.Log(hit.collider.name);
                if(hit.collider == animCtrl.headCollider){
                    animCtrl.Stun();
                }
            }
        }
    }

    public override void DropItem()
    {
        GameObject droppedObject = Instantiate(itemData.worldItem, transform.position, transform.rotation);
        droppedObject.GetComponent<PickableFazerBlaster>().ammo = this.ammo;
        Destroy(gameObject);
    }
}
