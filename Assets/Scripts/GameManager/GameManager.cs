using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Information d'initialisation de la partie
    [Header("Paramätres d'initialisation", order = 0)]

    public GameObject[] arcadeSpawn;
    public GameObject[] arcadeObject;
    public GameObject[] players;
    public int arcadeLeft;

    [SerializeField] private GameObject jumpScareCamera;
    [SerializeField] private Animator jumpScareFreddy;
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas gameOverScreen; 
    [SerializeField] private String lobbyScene;

    // Information de cours de partie
    [Header("Paramätres de partie en cours", order = 1)]

    // public ChildController[] children;
    public bool gameOver;

    private void Awake(){
        if(instance == null){ instance = this;} else { Destroy(gameObject);}
    }

    // Start is called before the first frame update
    void Start()
    {
        gameCanvas.gameObject.SetActive(true);
        gameOverScreen.gameObject.SetActive(false);
        jumpScareCamera.SetActive(false);

        arcadeSpawn = GameObject.FindGameObjectsWithTag("SpawnArcade");

        arcadeLeft = arcadeObject.Length - 1;
        players = GameObject.FindGameObjectsWithTag("Player");

        InitialiseArcade();
    }

    // Update is called once per frame
    void Update()
    {
        if(arcadeLeft == 0 && !gameOver){
            EndGame("Gregory");
        }
    }

    public void InitialiseArcade(){
        for(int i = 0; i < arcadeObject.Length; ++i){
            int j = Random.Range(0, arcadeSpawn.Length - 1);
            arcadeObject[i].transform.position = arcadeSpawn[j].transform.position;
            arcadeObject[i].transform.rotation = arcadeSpawn[j].transform.rotation;

            
        }

        for(int i = 0; i < arcadeObject.Length; ++i){
            int j = Random.Range(0, arcadeSpawn.Length);
            while(arcadeSpawn[j] == null){
                j = Random.Range(0, arcadeSpawn.Length);
            }
            Debug.Log("Spawn arcade " + i + " : " + j);

            arcadeObject[i].transform.position = arcadeSpawn[j].transform.position;
            arcadeObject[i].transform.rotation = arcadeSpawn[j].transform.rotation;

            Destroy(arcadeSpawn[j]);
            arcadeSpawn[j] = null;

            Debug.Log(arcadeSpawn[j]);
        }

        foreach(GameObject arcade in arcadeSpawn){
            Destroy(arcade);
        }
    }

    public void Jumpscare(){
        StartCoroutine(JumpscareCoroutine());
    }

    public void DisplayGameOverScreen(String winner){
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.GetComponentInChildren<TMP_Text>().text = winner + " won";
        gameOverScreen.GetComponent<AudioSource>().Play();
    }

    public void EndGame(String winner){
        gameOver = true;
        StartCoroutine(EndGameCoroutine(winner));
    }

    private IEnumerator JumpscareCoroutine(){
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        for(int i = 0; i < cameras.Length; ++i){
            cameras[i].gameObject.SetActive(false);
        }
        gameCanvas.gameObject.SetActive(false);
        jumpScareFreddy.SetTrigger("Jumpscare");
        jumpScareFreddy.gameObject.GetComponent<AudioSource>().Play();
        jumpScareCamera.SetActive(true);

        yield return new WaitForSeconds(1);

        jumpScareFreddy.gameObject.GetComponent<AudioSource>().Stop();
        EndGame("Freddy");
    }

    private IEnumerator EndGameCoroutine(String winner){
        DisplayGameOverScreen(winner);

        yield return new WaitForSeconds(5);

        // Retourner au menu
        SceneManager.LoadScene(lobbyScene);
    }

}
