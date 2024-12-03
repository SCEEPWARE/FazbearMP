using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 4; // Points de vie de l'ennemi
    public Sprite normalSprite; // Sprite normal de l'ennemi
    public Sprite damagedSprite; // Sprite de l'ennemi blessé
    public float damageEffectDuration = 0.5f; // Durée de l'effet de dégât (0.5 sec)

    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer
    public MonsterWaveSpawner waveSpawner;  // Référence au MonsterWaveSpawner

    void Start()
    {
        // Récupérer le SpriteRenderer de l'ennemi
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Assigner le sprite normal au début
        spriteRenderer.sprite = normalSprite;
    }

    // Fonction pour infliger des dégâts à l'ennemi
    public void TakeDamage(int damage)
    {
        health -= damage; // Réduire les points de vie de l'ennemi

        // Changer temporairement le sprite pour l'effet de dégât
        StartCoroutine(ShowDamageEffect());

        // Vérifier si l'ennemi n'a plus de points de vie
        if (health <= 0)
        {
            Die(); // Détruire l'ennemi s'il n'a plus de points de vie
        }
    }

    // Coroutine pour afficher l'effet de dégât
    IEnumerator ShowDamageEffect()
    {
        // Changer le sprite pour montrer les dégâts
        spriteRenderer.sprite = damagedSprite;

        // Attendre 0.5 seconde
        yield return new WaitForSeconds(damageEffectDuration);

        // Remettre le sprite normal
        spriteRenderer.sprite = normalSprite;
    }

    // Fonction pour détruire l'ennemi
    void Die()
    {
        // Informer le MonsterWaveSpawner que l'ennemi est mort
        if (waveSpawner != null)
        {
            waveSpawner.OnMonsterDestroyed();  // Appeler la fonction dans MonsterWaveSpawner
        }

        // Ajouter ici des animations ou effets avant la destruction si besoin
        Destroy(gameObject); // Détruire l'ennemi
    }
}
