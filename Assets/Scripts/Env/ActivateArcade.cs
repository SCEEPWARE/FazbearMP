using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArcade : MonoBehaviour
{
    public GameObject currentUser;
    public GameObject miniGame;
    public GameObject gameCam;
    public void AccessArcade(GameObject activator){
        Debug.Log("arcade is accessed");
        currentUser = activator;
        activator.GetComponent<CameraController>()._cameraObject.SetActive(false);
        activator.GetComponent<BasePlayerController>().inputEnabled = false;
        gameCam.SetActive(true);

        miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = true;
    }
    void Start()
    {
        gameCam = miniGame.GetComponentInChildren<Camera>().gameObject;
        gameCam.SetActive(false);
        miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentUser != null){
            currentUser.GetComponent<CameraController>()._cameraObject.SetActive(true);
            currentUser.GetComponent<BasePlayerController>().inputEnabled = true;
            gameCam.SetActive(false);
            currentUser = null;

            miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = false;
        }
    }
}
