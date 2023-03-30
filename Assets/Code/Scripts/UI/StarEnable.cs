using UnityEngine;
using UnityEngine.UI;

public class StarEnable : MonoBehaviour
{
    [SerializeField]private Slider slider;  // Reference to the slider asset
    private float activationValue1 = 33f;  // The value at which to activate the first game object
    private float activationValue2 = 66f;  // The value at which to activate the second game object
    private float activationValue3 = 99f;  // The value at which to activate the third game object
    [SerializeField] private GameObject objectToActivate1;  // The first game object to activate
    [SerializeField] private GameObject objectToActivate2;  // The second game object to activate
    [SerializeField] private GameObject objectToActivate3;  // The third game object to activate

    void Update()
    {
        // Check if the slider value is greater than or equal to the activation value
        if (slider.value >= activationValue1)
        {
            // Activate the game object
            objectToActivate1.SetActive(true);
        }

        // Check if the slider value is greater than or equal to the activation value
        if (slider.value >= activationValue2)
        {
            // Activate the game object
            objectToActivate2.SetActive(true);
        }

        // Check if the slider value is greater than or equal to the activation value
        if (slider.value >= activationValue3)
        {
            // Activate the game object
            objectToActivate3.SetActive(true);
        }
    }
}