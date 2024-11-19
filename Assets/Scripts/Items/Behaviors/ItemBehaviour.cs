using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public ItemData itemData;
    public bool InputEnabled;
    public Vector3 itemOffset;
    public GameObject localObject;

    public void Start(){
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = itemOffset;
    }
    public virtual void MainFire(){}
    public virtual void SecondaryFire(){}
    public virtual void DropItem(){
        Instantiate(itemData.worldItem, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
