using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider StaminaSlider;

    [Header("Variables")]
    [SerializeField] private float MaxStamina;
    [SerializeField] private float DecreaseValue;

    [HideInInspector] public float CurrentStamina;

    public static Stamina Instance;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentStamina = MaxStamina;
        StaminaSlider.maxValue = MaxStamina;
    }

    private void Update()
    {
        
    }

    public void UseEnergy(bool isDraining)
    {
        StaminaSlider.value = CurrentStamina;

        if (isDraining)
        {
            if (CurrentStamina != 0)
            {
                CurrentStamina -= DecreaseValue * Time.deltaTime;
            }
        }
        else if (!isDraining)
        {
            if (CurrentStamina <= 100)
            {
                CurrentStamina += (DecreaseValue / 2) * Time.deltaTime;
            }
        }
    }
}
