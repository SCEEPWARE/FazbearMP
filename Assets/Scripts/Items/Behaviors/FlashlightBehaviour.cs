using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightBehaviour : ItemBehaviour
{
    [SerializeField] private Light flashLight;

    public void Start(){
        flashLight = GetComponentInChildren<Light>();
        flashLight.transform.SetParent(owner.GetComponent<CameraController>()._cameraObject.transform, false);
        flashLight.transform.localPosition += new Vector3(0, 0, 0.3f);
    }
    public override void MainFire(){
        flashLight.enabled = !flashLight.enabled;
    }
}
