using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public ItemData itemData;
    public bool InputEnabled;

    public virtual void MainFire(){}
    public virtual void SecondaryFire(){}
    public virtual void DropItem(){
        Instantiate(itemData.worldItem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}