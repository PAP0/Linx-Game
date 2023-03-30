using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarEnable : MonoBehaviour
{
    [SerializeField] private Slider Slider;  // Reference to the slider asset
    [SerializeField] private float Value;
    [SerializeField] private List<ObjectToActivate> ObjectsToActivate = null;



    void Update()
    {
        if (ObjectsToActivate.Count == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < ObjectsToActivate.Count; i++)
            {
                ObjectsToActivate[i].activationValue = (i * Value) + Value;
                if (Slider.value >= ObjectsToActivate[i].activationValue)
                {
                    // Activate the game object
                    ObjectsToActivate[i].objectToActivate.SetActive(true);
                }
            }
        }
    }
}
[Serializable]
public class ObjectToActivate
{
    public GameObject objectToActivate;
    public float activationValue;
}