using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ChildController : BasePlayerController
{
    [Header("ParamŠtres sp‚cifiques de l'enfant", order = 2)]
    [SerializeField] private float jumpForce = 1f;
    private bool jumpState;


    // Variables autres

    // Header pour l'arme
    // arme du joueur

    protected override void Update()
    {
        if(inputEnabled){
            return;
        }
        base.Update();
        if(Input.GetButtonDown("Jump") && isGrounded){
            jumpState = true;
        }
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(jumpState){
            Debug.Log("Jumping");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpState = false;
        }
    }
}
