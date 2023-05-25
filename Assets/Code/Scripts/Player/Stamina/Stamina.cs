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

    private void Awake()
    {
        StaminaSlider.maxValue = MaxStamina;
        StaminaObject.CurrentStamina = StaminaSlider.maxValue;
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
            if (StaminaObject.CurrentStamina <= MaxStamina)
            {
                StaminaObject.CurrentStamina += (DecreaseValue / 2) * Time.deltaTime;
            }
        }
    }
}
