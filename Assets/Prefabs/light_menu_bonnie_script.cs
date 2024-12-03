using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light pointLight;
    private float nextBlinkTime;

    void Start()
    {
        pointLight = GetComponent<Light>();
        SetNextBlinkTime();
    }

    void Update()
    {
        if (Time.time >= nextBlinkTime)
        {

            pointLight.enabled = !pointLight.enabled;
            SetNextBlinkTime();
        }
    }

    void SetNextBlinkTime()
    {
        float randomDelay = Random.Range(0f, 1f);
        nextBlinkTime = Time.time + randomDelay;
    }
}
