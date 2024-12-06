using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWaveSpawner : MonoBehaviour
{
    public GameObject gosse;  // Le monstre que tu as déjà créé
    public int[] monstersPerWave = new int[] { 3, 4, 5 };  // Nombre de monstres par vague
    [SerializeField] private GameObject spawnLimitMin;
    [SerializeField] private GameObject spawnLimitMax;
    public GameObject gameOverSprite1;  // Le sprite de "game over" à afficher
    public Vector3 gameOverPosition1 = new Vector3(-4, 3, -5);  // Position où afficher le sprite de "game over"
    public GameObject gameOverSprite2;  // Le sprite de "game over" à afficher
    public Vector3 gameOverPosition2 = new Vector3(-3, 2, -4);  // Position où afficher le sprite de "game over"
    public GameObject player;  // Référence à l'objet du joueur pour désactiver les entrées
    private bool inputsEnabled = true;

    private int currentWave = 0;
    private int monstersAlive = 0;  // Nombre de monstres vivants dans la vague actuelle
    public bool isFinished;

    void Start()
    {

        // Lancer le spawn des vagues au début
        SpawnWave();
    }

    void SpawnWave()
    {
        if (currentWave < monstersPerWave.Length)
        {
            int monstersToSpawn = monstersPerWave[currentWave];
            monstersAlive = monstersToSpawn;  // Réinitialiser le nombre de monstres vivants pour la nouvelle vague

            // Lancer un spawn de monstres pour cette vague
            SpawnMonsters(monstersToSpawn);
        }
    }

    void SpawnMonsters(int count){
        for(int i = 0; i < count; ++i){
            // Calculer une position aléatoire dans la zone de spawn
            Debug.Log(spawnLimitMin.gameObject.transform.position.x);
            Debug.Log(spawnLimitMin.gameObject.transform.position.y);
            Debug.Log(spawnLimitMax.gameObject);
            float x = Random.Range(spawnLimitMin.gameObject.transform.position.x, spawnLimitMax.transform.position.x);
            Debug.Log(x);
            float y = Random.Range(spawnLimitMin.gameObject.transform.position.y, spawnLimitMax.transform.position.y);
            Vector3 spawnPosition = new Vector3(x, y, 0);

            // Créer un monstre à cette position
            GameObject spawnedChild = Instantiate(gosse, spawnPosition, Quaternion.identity, transform.parent);
            spawnedChild.GetComponent<EnemyHealth>().waveSpawner = this;
        }
    }

    void Update(){
        if(monstersAlive == 0){
            // Si c'est la vague 3, afficher le game over et désactiver les entrées
            if (currentWave == 2 && !isFinished)  // La vague 3 est l'indice 2
            {
                // ShowGameOver();
                GameManager.instance.arcadeLeft--;
                isFinished = true;
            }
            else
            {
                // Passer à la vague suivante
                currentWave++;
                if (currentWave < monstersPerWave.Length)
                {
                    SpawnWave();
                }
            }
        }
    }



    // IEnumerator SpawnMonsters(int count)
    // {
    //     for (int i = 0; i < count; i++)
    //     {
    //         SpawnMonster();
    //     }

    //     // Attendre que tous les monstres soient tués
    //     yield return new WaitUntil(() => monstersAlive == 0);

    //     // Si c'est la vague 3, afficher le game over et désactiver les entrées
    //     if (currentWave == 2)  // La vague 3 est l'indice 2
    //     {
    //         ShowGameOver();
    //         GameManager.instance.arcadeLeft--;
    //     }
    //     else
    //     {
    //         // Passer à la vague suivante
    //         currentWave++;
    //         if (currentWave < monstersPerWave.Length)
    //         {
    //             SpawnWave();
    //         }
    //     }
    // }




    // Cette fonction sera appelée lorsqu'un monstre est tué
    public void OnMonsterDestroyed()
    {
        monstersAlive--;  // Réduire le nombre de monstres vivants
    }

    // Fonction pour afficher le sprite de game over et désactiver les entrées
    void ShowGameOver()
    {
        // Afficher le sprite de game over
        Instantiate(gameOverSprite1, gameOverPosition1, Quaternion.identity);
        Instantiate(gameOverSprite2, gameOverPosition2, Quaternion.identity);

        // Désactiver les entrées du joueur
        if (player != null)
        {
            inputsEnabled = !inputsEnabled;
        }
    }
}
