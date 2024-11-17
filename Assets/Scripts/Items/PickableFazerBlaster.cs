using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableFazerBlaster : PickableItem
{
    public int ammo = 5;

    public override void PickUp(GameObject activator)
    {
        activator.GetComponent<ChildController>().item = this.item;
        activator.GetComponent<ChildController>().item.GetComponent<FazerblasterBehaviour>().ammo = this.ammo;
        Destroy(gameObject);
    }
}
