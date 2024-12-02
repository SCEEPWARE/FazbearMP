using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public ItemData itemData;
    public GameObject item;

    public virtual void PickUp(GameObject activator){
        activator.GetComponent<BasePlayerController>().item = this.item;
        Destroy(gameObject);
    }
}
