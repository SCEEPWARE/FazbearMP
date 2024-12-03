using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 4; // Points de vie de l'ennemi
    public Sprite normalSprite; // Sprite normal de l'ennemi
    public Sprite damagedSprite; // Sprite de l'ennemi bless�
    public float damageEffectDuration = 0.5f; // Dur�e de l'effet de d�g�t (0.5 sec)

    private SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer
    public MonsterWaveSpawner waveSpawner;  // R�f�rence au MonsterWaveSpawner

    void Start()
    {
        // R�cup�rer le SpriteRenderer de l'ennemi
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Assigner le sprite normal au d�but
        spriteRenderer.sprite = normalSprite;
    }

    // Fonction pour infliger des d�g�ts � l'ennemi
    public void TakeDamage(int damage)
    {
        health -= damage; // R�duire les points de vie de l'ennemi

        // Changer temporairement le sprite pour l'effet de d�g�t
        StartCoroutine(ShowDamageEffect());

        // V�rifier si l'ennemi n'a plus de points de vie
        if (health <= 0)
        {
            Die(); // D�truire l'ennemi s'il n'a plus de points de vie
        }
    }

    // Coroutine pour afficher l'effet de d�g�t
    IEnumerator ShowDamageEffect()
    {
        // Changer le sprite pour montrer les d�g�ts
        spriteRenderer.sprite = damagedSprite;

        // Attendre 0.5 seconde
        yield return new WaitForSeconds(damageEffectDuration);

        // Remettre le sprite normal
        spriteRenderer.sprite = normalSprite;
    }

    // Fonction pour d�truire l'ennemi
    void Die()
    {
        // Informer le MonsterWaveSpawner que l'ennemi est mort
        if (waveSpawner != null)
        {
            waveSpawner.OnMonsterDestroyed();  // Appeler la fonction dans MonsterWaveSpawner
        }

        // Ajouter ici des animations ou effets avant la destruction si besoin
        Destroy(gameObject); // D�truire l'ennemi
    }
}
