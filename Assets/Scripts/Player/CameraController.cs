using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _cameraObject;
    private float xRot = 0, yRot = 0;

    [Header("ParamŠtres de la cam‚ra", order = 0)]
    [SerializeField] private float mouseSensitivity = 5f; 
    [SerializeField] private Vector3 camOffset;
    [SerializeField] private float viewBobbingForce;
    [SerializeField] private float viewBobbingSpeed;
    [SerializeField] private Transform neck;


    private float bobbingTimer = 0;
    private float smoothSpeed = 0;

    // R‚f‚rence au player controller (pour des variables)
    [SerializeField] private BasePlayerController basePlayerController;

    void Start()
    {
        // R‚f‚rence + verrouillage du curseur
        basePlayerController = gameObject.GetComponent<BasePlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        

        // view bobbing (mouvement de la cam‚ra lorsqu'on se d‚place, on ne veut qu'un demi-cercle donc on prend la valeur absolu du sinus pour le mouvement vertical)

        Vector3 bobbingVector = new Vector3();

        float clampedSpeed = basePlayerController.moveSpeed / basePlayerController.runSpeed;
        bobbingVector = clampedSpeed * Mathf.Abs(Mathf.Sin(viewBobbingSpeed * bobbingTimer * clampedSpeed)) * viewBobbingForce * -transform.up + clampedSpeed * Mathf.Sin(viewBobbingSpeed * bobbingTimer * clampedSpeed) * viewBobbingForce * transform.right;
        bobbingTimer = basePlayerController.moveDir.magnitude > 0 ? bobbingTimer + Time.deltaTime : 0;
        // Debug.Log(clampedSpeed);

        // Si les contr“les de la cam‚ra est d‚sactiv‚e, on ne r‚cupŠre pas d'input
        if(basePlayerController.cameraEnabled){
            if(basePlayerController.controlChosen == BasePlayerController.ControlType.keyboard){
                yRot += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
                xRot -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            }
            if(basePlayerController.controlChosen == BasePlayerController.ControlType.controller){
                yRot += Input.GetAxisRaw("Controller X") * mouseSensitivity;
                xRot -= Input.GetAxisRaw("Controller Y") * mouseSensitivity;
            }
        }
        xRot = Mathf.Clamp(xRot, -90f, 90f); // On bloque la rotation verticale … 90ø pour que la cam‚ra ne soit pas … l'envers
        
        gameObject.transform.rotation = Quaternion.Euler(0, yRot, 0);

        _cameraObject.transform.position = gameObject.transform.position + camOffset + bobbingVector;
        _cameraObject.transform.rotation = Quaternion.Euler(xRot, yRot, 0);

        // neck.localRotation = _cameraObject.transform.rotation;
    }
}
