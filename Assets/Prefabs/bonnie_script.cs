 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissiveController : MonoBehaviour
{
    public Light targetLight; // La lumière à surveiller
    public Color emissiveColor = Color.white; // Couleur d'émission
    public float emissiveIntensityOn = 10f; // Intensité d'émission lorsque la lumière est allumée
    public float emissiveIntensityOff = 0f; // Intensité d'émission lorsque la lumière est éteinte

    private Renderer emissiveRenderer; // Référence au Renderer de cet objet

    void Start()
    {
        // Récupérer le Renderer de cet objet
        emissiveRenderer = GetComponent<Renderer>();
        if (emissiveRenderer == null)
        {
            Debug.LogError("Aucun Renderer trouvé pour l'émission sur cet objet !");
            return;
        }

        // Vérifier que la lumière cible est assignée
        if (targetLight == null)
        {
            Debug.LogError("Aucune lumière cible assignée !");
            return;
        }

        // Synchroniser immédiatement l'intensité
        UpdateEmissiveIntensity();
    }

    void Update()
    {
        // Vérifier si l'état de la lumière a changé et mettre à jour l'intensité
        UpdateEmissiveIntensity();
    }

    // Met à jour l'intensité d'émission en fonction de l'état de la lumière
    void UpdateEmissiveIntensity()
    {
        if (emissiveRenderer != null && targetLight != null)
        {
            float intensity = targetLight.enabled ? emissiveIntensityOn : emissiveIntensityOff;
            emissiveRenderer.material.SetColor("_EmissiveColor", emissiveColor * intensity);
        }
    }
}
