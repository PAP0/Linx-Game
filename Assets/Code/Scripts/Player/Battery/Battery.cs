using UnityEngine;
using UnityEngine.UI;

/// <summary>  
/// This script handles the battery dudes battery level and makes sure it drains/charges when it needs to.
/// </summary>

public class Battery : MonoBehaviour
{
    #region Variables

    [Header("Battery")]
    [Tooltip("The current battery level")]
    public float currentBattery;
    [Tooltip("The max battery levell")]
    [SerializeField] private float MaxBattery;
    [Tooltip("The energy consumption or charging rate")]
    [SerializeField] private float EnergyValue;

    [Header("Battery UI")]
    [Tooltip("Slider component displaying the battery level")]
    [SerializeField] private Slider BatterySlider;

    // Indicates if the battery is currently being charged.
    private bool IsCharging;

    #endregion

    #region Unity Events

    // When the script is initialized.
    private void Awake()
    {
        // Set the maximum value of the BatterySlider to the MaxBattery value.
        BatterySlider.maxValue = MaxBattery;
        // Set the currentBattery to the maximum battery level.
        currentBattery = BatterySlider.maxValue;
    }

    // Every frame.
    private void Update()
    {
        // Recharge the battery.
        Recharge();
    }

    // Check if something has entered the collider.
    private void OnTriggerStay(Collider other)
    {
        // If the collider that enters is a ChargingStation.
        if (other.CompareTag("ChargingStation"))
        {
            // The battery is will begin charging.
            IsCharging = true;
        }
    }

    // Check if something has exited the collider.
    private void OnTriggerExit(Collider other)
    {
        // If the collider that exits is a ChargingStation.
        if (other.CompareTag("ChargingStation"))
        {
            // The battery is will not be charged.
            IsCharging = false;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>  
    /// This Method drains the battery, depending on if IsDraining is true.
    /// </summary>
    public void UseEnergy(bool IsDraining)
    {
        // Update the value of the BatterySlider to reflect the current battery level.
        BatterySlider.value = currentBattery;
        // Check if IsDraining is true.
        if (IsDraining)
        {
            // Check if battery level is not 0.
            if (currentBattery != 0)
            {
                // Decrease the current battery level based on energy consumption rate over time.
                currentBattery -= EnergyValue * Time.deltaTime;
            }
        }
    }


    /// <summary>  
    /// This Method recharges the battery, depending on if IsCharging is true.
    /// </summary>
    public void Recharge()
    {
        // Check if IsCharging is true.
        if (IsCharging)
        {
            // Check if the batterylevel is less than the max.
            if (currentBattery <= MaxBattery)
            {
                // Increase the current battery level based on charging rate over time.
                currentBattery += (EnergyValue * 2) * Time.deltaTime;
            }
        }
    }

    #endregion
}