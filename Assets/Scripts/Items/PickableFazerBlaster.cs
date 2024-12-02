using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableFazerBlaster : PickableItem
{
    public int ammo = 5;

    public override void PickUp(GameObject activator)
    {
        activator.GetComponent<BasePlayerController>().item = this.item;
        activator.GetComponent<BasePlayerController>().item.GetComponent<FazerblasterBehaviour>().ammo = this.ammo;
        Destroy(gameObject);
    }
}
