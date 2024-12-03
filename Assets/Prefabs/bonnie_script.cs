 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveController : MonoBehaviour
{
    public Light targetLight; // La lumi�re � surveiller
    public Color emissiveColor = Color.white; // Couleur d'�mission
    public float emissiveIntensityOn = 10f; // Intensit� d'�mission lorsque la lumi�re est allum�e
    public float emissiveIntensityOff = 0f; // Intensit� d'�mission lorsque la lumi�re est �teinte

    private Renderer emissiveRenderer; // R�f�rence au Renderer de cet objet

    void Start()
    {
        // R�cup�rer le Renderer de cet objet
        emissiveRenderer = GetComponent<Renderer>();
        if (emissiveRenderer == null)
        {
            Debug.LogError("Aucun Renderer trouv� pour l'�mission sur cet objet !");
            return;
        }

        // V�rifier que la lumi�re cible est assign�e
        if (targetLight == null)
        {
            Debug.LogError("Aucune lumi�re cible assign�e !");
            return;
        }

        // Synchroniser imm�diatement l'intensit�
        UpdateEmissiveIntensity();
    }

    void Update()
    {
        // V�rifier si l'�tat de la lumi�re a chang� et mettre � jour l'intensit�
        UpdateEmissiveIntensity();
    }

    // Met � jour l'intensit� d'�mission en fonction de l'�tat de la lumi�re
    void UpdateEmissiveIntensity()
    {
        if (emissiveRenderer != null && targetLight != null)
        {
            float intensity = targetLight.enabled ? emissiveIntensityOn : emissiveIntensityOff;
            emissiveRenderer.material.SetColor("_EmissiveColor", emissiveColor * intensity);
        }
    }
}
