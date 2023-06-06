using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    public float CurrentBattery;

    [Header("References")]
    [SerializeField] private Slider BatterySlider;

    [Header("Variables")]
    [SerializeField] private float MaxBattery;
    [SerializeField] private float EnergyValue;

    private bool isCharging;

    public void UseEnergy(bool isDraining)
    {
        BatterySlider.value = CurrentBattery;

        if (isDraining)
        {
            if (CurrentBattery != 0)
            {
                CurrentBattery -= EnergyValue * Time.deltaTime;
            }
        }
    }

    public void Recharge()
    {
        if(isCharging)
        {
            if (CurrentBattery <= MaxBattery)
            {
                CurrentBattery += (EnergyValue * 2) * Time.deltaTime;
            }
        }
    }

    private void Awake()
    {
        BatterySlider.maxValue = MaxBattery;
        CurrentBattery = BatterySlider.maxValue;
    }

    private void Update()
    {
        Recharge();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ChargingStation"))
        {
            isCharging = true;
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("ChargingStation"))
        {
            isCharging = false;
        }
    }
}