using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightBehaviour : ItemBehaviour
{
    [SerializeField] private Light flashLight;

    private void OnEnable(){
        flashLight = GetComponentInChildren<Light>();
    }
    public override void MainFire(){
        flashLight.enabled = !flashLight.enabled;
    }
}
