using UnityEngine;

/// <summary>
/// This script takes care of the entire lighting effect for the police lights at the end of the timer.
/// </summary>

public class Lamp : MonoBehaviour
{
    #region Variables

    [Header("Light Speed and intensity")]
    [Tooltip("This value changes the light intensity")]
    [SerializeField] private float LightIntensity = 1.0f;
    [Tooltip("This value changes the light amplitude")]
    [SerializeField] private float LightAmplitude = 1.0f;
    [Tooltip("This value changes the light frequency")]
    [SerializeField] private float LightFrequency = 1.0f;
    [Tooltip("This value changes the light phase")]
    [SerializeField] private float LightPhase = 0.0f;
    [Tooltip("This value changes the light fading time")]
    [SerializeField] private float FadeTime = 6.0f;

    // Reference to the light component.
    private Light LightComponent;
    // The intensity of the light.
    private float CurrentIntensity = 0.0f;
    // The time when the fading starts.
    private float FadeStartTime = 0.0f;

    #endregion

    #region Unity Events

    // When the script is started.
    private void Start()
    {
        // This gets the light component attached to the object.
        LightComponent = GetComponent<Light>();
        // The current intensity of the light.
        CurrentIntensity = 0.0f;
        // Sets the Light components intensity equal to CurrentIntensity.
        LightComponent.intensity = CurrentIntensity;
        // Sets the FadeStartTIme equal to time.
        FadeStartTime = Time.time;
    }

    // Every frame.
    private void Update()
    {
        // Calculate the time elapsed since fading started.
        float timeSinceFadeStart = Time.time - FadeStartTime;
        // Calculate the fade amount based on the time elapsed and fade duration.
        float fadeAmount = Mathf.Clamp01(timeSinceFadeStart / FadeTime);
        // Calculate the value of the sine wave based on current time, frequency, and phase.
        float sineWaveValue = Mathf.Sin((Time.time * LightFrequency) + LightPhase) * LightAmplitude;
        // Calculate the current intensity by interpolating between 0 and the target intensity with the fade amount.
        CurrentIntensity = Mathf.Lerp(0.0f, LightIntensity + sineWaveValue, fadeAmount);
        // Set the light intensity to the current intensity
        LightComponent.intensity = CurrentIntensity;
    }

    #endregion
}