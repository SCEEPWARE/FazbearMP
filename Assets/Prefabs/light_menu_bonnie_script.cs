using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light pointLight;
    private float nextBlinkTime;

    void Start()
    {
        pointLight = GetComponent<Light>();
        SetNextBlinkTime(GetHighBlinkTime());
    }

    void Update()
    {
        if (Time.time >= nextBlinkTime)
        {

            pointLight.enabled = !pointLight.enabled;
            SetNextBlinkTime(pointLight.enabled? GetHighBlinkTime() : GetLowBlinkTime());
        }
    }

    void SetNextBlinkTime(float delay)
    {
        nextBlinkTime = Time.time + delay;
    }

    float GetLowBlinkTime(){
        return Random.Range(0f, 0.75f);
    }

    float GetHighBlinkTime(){
        return Random.Range(0.5f, 4f);
    }
}
