using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPlaceback : MonoBehaviour
{
    [Tooltip("The gameobject that gets turned on when the furniture is placed")]
    public GameObject SolidObject; // The gameobject that gets turned on when the furniture is placed
    
    [Tooltip("Reference to the variables script")]
    [SerializeField] private PropPlaceBackVariables propPlaceBackVariables; // Reference to the variables script
    [Tooltip("Material of which the alpha is changed once the Target object is in place")]
    [SerializeField] private Material SeeThroughMaterial; // Material of which the alpha is changed once the Target object is in place
    [Tooltip("The time it takes to fade that alpha")]
    [SerializeField] private float FadeTime = 0.1f; // The time it takes to fade that alpha
    [Tooltip("The value of how transparent the SeeThroughMaterial is")]
    [Range(0f, 0.5f)] private float AlphaValue; // The value of how transparent the SeeThroughMaterial is

    private void Start()
    {
        SolidObject.SetActive(false);
        SeeThroughMaterial.SetFloat("_Alpha", 0.5f);
        AlphaValue = SeeThroughMaterial.GetFloat("_Alpha");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == propPlaceBackVariables.TargetObject)
        {
            propPlaceBackVariables.IsInPlace = true;
            Debug.Log(propPlaceBackVariables.TargetObject.name + " has entered the trigger.");
            propPlaceBackVariables.TargetObject.transform.position = transform.parent.position;
            StartCoroutine(FadeOut());
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == propPlaceBackVariables.TargetObject)
        {
            propPlaceBackVariables.IsInPlace = false;
            Debug.Log(propPlaceBackVariables.TargetObject.name + " has exited the trigger.");
            StartCoroutine(FadeIn());
        }
    }
    */
    
    IEnumerator FadeOut()
    {
        while (SeeThroughMaterial.GetFloat("_Alpha") > 0f)
        {
            AlphaValue -= (FadeTime * Time.deltaTime);
            SeeThroughMaterial.SetFloat("_Alpha", AlphaValue);
            SolidObject.SetActive(true);
            Destroy(propPlaceBackVariables.TargetObject);
            yield return new WaitForEndOfFrame();
        }
    }
    
    /*
    IEnumerator FadeIn()
    {
        while (SeeThroughMaterial.GetFloat("_Alpha") < 0.5f)
        {
            AlphaValue += (FadeTime * Time.deltaTime);
            SeeThroughMaterial.SetFloat("_Alpha", AlphaValue);
            yield return new WaitForEndOfFrame();
        }
    }
    */
}
