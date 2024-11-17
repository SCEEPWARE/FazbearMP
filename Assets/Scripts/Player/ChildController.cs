using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class ChildController : BasePlayerController
{
    [Header("ParamŠtres sp‚cifiques de l'enfant", order = 2)]
    [SerializeField] private float jumpForce = 1f;
    private bool jumpState;


    // Variables autres

    [Header("Arme du joueur", order = 3)]

    [SerializeField] private GameObject _item;
    public GameObject item{
        get{
            return _item;
        }
        set{
            _item = Instantiate(value, itemSpawnPos);
        }
    }
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private Transform itemSpawnPos;

    protected override void Update()
    {
        if(!inputEnabled){
            return;
        }
        base.Update();
        if(Input.GetButtonDown("Jump") && isGrounded){
            jumpState = true;
        }

        if(item != null){
            if(Input.GetButtonDown("Fire1")){
                item.GetComponent<ItemBehaviour>().MainFire();
            }
            if(Input.GetButtonDown("Fire2")){
                item.GetComponent<ItemBehaviour>().SecondaryFire();
            }
        }
        
        itemSpawnPos.transform.rotation = Camera.main.transform.rotation;

        // Si on souhaite drop un item;
        if(item != null){
            if(Input.GetKeyDown(KeyCode.G)){
                item.GetComponent<ItemBehaviour>().DropItem();
            }
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

        // On fait un raycast pour v‚rifier qu'on peut ramasser un item
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f, itemLayer))
        {
            if (Input.GetKey(KeyCode.E) && item == null)
            {
                hit.collider.gameObject.GetComponent<PickableItem>().PickUp(gameObject);
            }
        }
    }
}