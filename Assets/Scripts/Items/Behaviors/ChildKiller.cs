using UnityEngine;

public class ChildKiller : ItemBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    public override void MainFire()
    {
        if(Physics.Raycast(owner.GetComponent<CameraController>()._cameraObject.transform.position, owner.GetComponent<CameraController>()._cameraObject.transform.forward, out RaycastHit hit, 1f, playerLayer)){
            if(hit.collider.TryGetComponent<BasePlayerController>(out BasePlayerController child)){
                Debug.Log(hit.collider.name);
                GameManager.instance.Jumpscare();
                InputEnabled = false;
            }
        }
    }
}
