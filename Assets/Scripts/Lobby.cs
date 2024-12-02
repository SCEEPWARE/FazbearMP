using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Charger la scène du jeu (remplacez "GameScene" par le nom réel de votre scène)
        SceneManager.LoadScene("jeu_arcade");
    }

    public void QuitGame()
    {
        // Quitter l'application
        Application.Quit();
    }
}
