using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveSpawner : MonoBehaviour
{
    public GameObject gosse;  // Le monstre que tu as d�j� cr��
    public int[] monstersPerWave = new int[] { 3, 6, 9 };  // Nombre de monstres par vague
    public Vector2 spawnAreaMin = new Vector2(-2, 10);  // Coin inf�rieur gauche de la zone de spawn
    public Vector2 spawnAreaMax = new Vector2(10, -5);    // Coin sup�rieur droit de la zone de spawn
    public GameObject gameOverSprite1;  // Le sprite de "game over" � afficher
    public Vector3 gameOverPosition1 = new Vector3(-4, 3, -5);  // Position o� afficher le sprite de "game over"
    public GameObject gameOverSprite2;  // Le sprite de "game over" � afficher
    public Vector3 gameOverPosition2 = new Vector3(-3, 2, -4);  // Position o� afficher le sprite de "game over"
    public GameObject player;  // R�f�rence � l'objet du joueur pour d�sactiver les entr�es
    private bool inputsEnabled = true;

    private int currentWave = 0;
    private int monstersAlive = 0;  // Nombre de monstres vivants dans la vague actuelle

    void Start()
    {
        // Lancer le spawn des vagues au d�but
        SpawnWave();
    }

    void SpawnWave()
    {
        if (currentWave < monstersPerWave.Length)
        {
            int monstersToSpawn = monstersPerWave[currentWave];
            monstersAlive = monstersToSpawn;  // R�initialiser le nombre de monstres vivants pour la nouvelle vague

            // Lancer un spawn de monstres pour cette vague
            StartCoroutine(SpawnMonsters(monstersToSpawn));
        }
    }

    System.Collections.IEnumerator SpawnMonsters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Calculer une position al�atoire dans la zone de spawn
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);

            // Cr�er un monstre � cette position
            Instantiate(gosse, spawnPosition, Quaternion.identity);
        }

        // Attendre que tous les monstres soient tu�s
        yield return new WaitUntil(() => monstersAlive == 0);

        // Si c'est la vague 3, afficher le game over et d�sactiver les entr�es
        if (currentWave == 2)  // La vague 3 est l'indice 2
        {
            ShowGameOver();
        }
        else
        {
            // Passer � la vague suivante
            currentWave++;
            if (currentWave < monstersPerWave.Length)
            {
                SpawnWave();
            }
        }
    }

    // Cette fonction sera appel�e lorsqu'un monstre est tu�
    public void OnMonsterDestroyed()
    {
        monstersAlive--;  // R�duire le nombre de monstres vivants
    }

    // Fonction pour afficher le sprite de game over et d�sactiver les entr�es
    void ShowGameOver()
    {
        // Afficher le sprite de game over
        Instantiate(gameOverSprite1, gameOverPosition1, Quaternion.identity);
        Instantiate(gameOverSprite2, gameOverPosition2, Quaternion.identity);

        // D�sactiver les entr�es du joueur
        if (player != null)
        {
            inputsEnabled = !inputsEnabled;
        }
    }
}