using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public string SceneToLoad;
    public void PlayGame()
    {
        // Charger la scène du jeu (remplacez "GameScene" par le nom réel de votre scène)
        SceneManager.LoadScene(SceneToLoad);
    }

    public void QuitGame()
    {
        // Quitter l'application
        Application.Quit();
    }
}
