using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonItem : MonoBehaviour
{
    [SerializeField] private GameObject currentItem;

    public GameObject fpsItemDisplay(GameObject item){
        currentItem = Instantiate(item, transform);
        currentItem.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return currentItem;
    }

    public void fpsItemDestroy(){
        Destroy(currentItem);
    }
}
