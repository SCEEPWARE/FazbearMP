using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ChildController : BasePlayerController
{
    [Header("ParamŠtres sp‚cifiques de l'enfant", order = 1)]
    public float jumpForce = 1f;




    // Variables autres
    [SerializeField]private bool isGrounded;
    [SerializeField] private bool jumpState;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
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
