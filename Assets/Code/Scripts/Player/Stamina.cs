using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider StaminaSlider;
    [SerializeField] private StaminaScritableObject StaminaObject;

    [Header("Variables")]
    [SerializeField] private float MaxStamina;
    [SerializeField] private float DecreaseValue;

    // Start is called before the first frame update

    private void Start()
    {
        StaminaObject.CurrentStamina = MaxStamina;
        StaminaSlider.maxValue = MaxStamina;
    }

    public void UseEnergy(bool isDraining)
    {
        StaminaSlider.value = StaminaObject.CurrentStamina;

        if (isDraining)
        {
            if (StaminaObject.CurrentStamina != 0)
            {
                StaminaObject.CurrentStamina -= DecreaseValue * Time.deltaTime;
            }
        }
        else
        {
            if (StaminaObject.CurrentStamina <= 100)
            {
                StaminaObject.CurrentStamina += (DecreaseValue / 2) * Time.deltaTime;
            }
        }
    }
}
