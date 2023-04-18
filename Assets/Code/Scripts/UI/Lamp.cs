using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private float Intensity = 1.0f;
    [SerializeField] private float Amplitude = 1.0f;
    [SerializeField] private float Frequency = 1.0f;
    [SerializeField] private float Phase = 0.0f;
    [SerializeField] private float FadeTime = 6.0f;

    private Light LightComponent;
    private float CurrentIntensity = 0.0f;
    private float FadeStartTime = 0.0f;

    private void Start()
    {
        LightComponent = GetComponent<Light>();
        CurrentIntensity = 0.0f;
        LightComponent.intensity = CurrentIntensity;
        FadeStartTime = Time.time;
    }

    private void Update()
    {
        float timeSinceFadeStart = Time.time - FadeStartTime;
        float fadeAmount = Mathf.Clamp01(timeSinceFadeStart / FadeTime);

        float sineWaveValue = Mathf.Sin((Time.time * Frequency) + Phase) * Amplitude;
        CurrentIntensity = Mathf.Lerp(0.0f, Intensity + sineWaveValue, fadeAmount);
        LightComponent.intensity = CurrentIntensity;
    }
}
