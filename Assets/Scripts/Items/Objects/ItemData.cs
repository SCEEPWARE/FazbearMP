using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Fazbear/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public GameObject worldItem;
}
