using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Charger la sc�ne du jeu (remplacez "GameScene" par le nom r�el de votre sc�ne)
        SceneManager.LoadScene("jeu_arcade");
    }

    public void QuitGame()
    {
        // Quitter l'application
        Application.Quit();
    }
}
