using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _cameraObject;
    private float xRot = 0, yRot = 0;

    [Header("ParamŠtres de la cam‚ra", order = 0)]
    [SerializeField] private float mouseSensitivity = 5f; 
    [SerializeField] private Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        _cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponent<BasePlayerController>().inputEnabled){
            return;
        }
        // Mouvement souris
        yRot += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        xRot -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f); // On bloque la rotation verticale … 90ø pour que la cam‚ra ne soit pas … l'envers
        
        gameObject.transform.rotation = Quaternion.Euler(0, yRot, 0);

        _cameraObject.transform.position = gameObject.transform.position + camOffset;
        _cameraObject.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
