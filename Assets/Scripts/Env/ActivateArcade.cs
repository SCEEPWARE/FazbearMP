using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArcade : MonoBehaviour
{
    public GameObject currentUser;
    public GameObject miniGame;
    public GameObject gameCam;
    public void AccessArcade(GameObject activator){
        if(miniGame.GetComponentInChildren<MonsterWaveSpawner>().isFinished){
            return;
        }
        Debug.Log("arcade is accessed");
        currentUser = activator;
        activator.GetComponent<BasePlayerController>().cameraEnabled = false;
        activator.GetComponent<BasePlayerController>().inputEnabled = false;
        gameCam.SetActive(true);

        miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = true;
    }

    public void ExitArcade(){
        currentUser.GetComponent<BasePlayerController>().cameraEnabled = true;
        currentUser.GetComponent<BasePlayerController>().inputEnabled = true;
        gameCam.SetActive(false);
        currentUser = null;

        miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = false;
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
        if(Input.GetKeyDown(KeyCode.Escape) && currentUser != null || miniGame.GetComponentInChildren<MonsterWaveSpawner>().isFinished && currentUser != null){
            ExitArcade();
        }
    }
}
