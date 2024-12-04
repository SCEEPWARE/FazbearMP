using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateArcade : MonoBehaviour
{
    public GameObject currentUser;
    public GameObject miniGame;
    public GameObject gameCam;

    [SerializeField] private Material offMaterial;
    [SerializeField] private AudioSource audioSource;

    // Cache
    private MonsterWaveSpawner arcade;

    public void AccessArcade(GameObject activator){
        if(arcade.isFinished){
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
        arcade = miniGame.GetComponentInChildren<MonsterWaveSpawner>();
        gameCam = miniGame.GetComponentInChildren<Camera>().gameObject;
        gameCam.SetActive(false);
        miniGame.GetComponentInChildren<PlayerController>().inputsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && currentUser != null || arcade.isFinished && currentUser != null){
            ExitArcade();
        }
        if(arcade.isFinished){
            audioSource.Stop();
            gameObject.GetComponent<Renderer>().material = offMaterial;
        }
    }
}
