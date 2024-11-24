using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class ChildController : BasePlayerController
{
    [Header("Param�tres sp�cifiques de l'enfant", order = 2)]
    [SerializeField] private float jumpForce = 1f;
    private bool jumpState;


    // Variables autres

    [Header("Arme du joueur", order = 3)]

    [SerializeField] private GameObject _item;
    [SerializeField] private GameObject localObject;
    public GameObject item{
        get{
            return _item;
        }
        set{
            _item = Instantiate(value, itemSpawnPos);
            _item.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            localObject = FirstPersonItemPos.GetComponent<FirstPersonItem>().fpsItemDisplay(value.GetComponent<ItemBehaviour>().localObject);
        }
    }
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private Transform itemSpawnPos;
    [SerializeField] private Transform FirstPersonItemPos;

    protected override void Start()
    {
        base.Start();
        FirstPersonItemPos.parent = Camera.main.transform;
    }

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

        // Si on souhaite drop un item;
        if(item != null){
            if(Input.GetKeyDown(KeyCode.G)){
                Destroy(localObject);
                item.GetComponent<ItemBehaviour>().DropItem();
            }
        }

        animator.SetBool("HoldItem", item != null ? true : false);
    }

    

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(jumpState){
            Debug.Log("Jumping");
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpState = false;
        }

        // On fait un raycast pour v�rifier qu'on peut ramasser un item
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f, itemLayer))
        {
            if (Input.GetKey(KeyCode.E) && item == null)
            {
                Debug.Log("A ramass� : " + hit.collider.name);
                hit.collider.gameObject.GetComponent<PickableItem>().PickUp(gameObject);
            }
        }
    }
}