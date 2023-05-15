using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///----------------------------------------$$$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  
///----------------------------------------$$  __$$\ $$  __$$\ $$  __$$\ $$$ __$$\ 
///----------------------------------------$$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$\ $$ |
///----------Author------------------------$$$$$$$  |$$$$$$$$ |$$$$$$$  |$$\$$\$$ |
///----------Patryk Podworny---------------$$  ____/ $$  __$$ |$$  ____/ $$ \$$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      $$ |\$$$ |
///----------------------------------------$$ |      $$ |  $$ |$$ |      \$$$$$$  /
///----------------------------------------\__|      \__|  \__|\__|       \______/ 

/// <summary>  
/// This script checks whether a TargetObject(Furniture or prop) has entered the collider of a prop ghost and fades out the alpha of the ghost material.
/// </summary>
 
public class PropPlaceback : MonoBehaviour
{
    [Header("Replacement variables")]
    [Tooltip("The gameobject that gets turned on when the furniture is placed")]
    public GameObject SolidObject; // The gameobject that gets turned on when the furniture is placed.
    
    [Header("Script references")]
    [Tooltip("Reference to the variables script")]
    [SerializeField] private PropPlaceBackVariables propPlaceBackVariables; // Reference to the variables script.
    
    [Header("Material Variables")]
    [Tooltip("Material of which the alpha is changed once the Target object is in place")]
    [SerializeField] private Material SeeThroughMaterial; // Material of which the alpha is changed once the Target object is in place.
    [Tooltip("The value of how transparent the SeeThroughMaterial is")]
    [Range(0f, 0.5f)] private float AlphaValue; // The value of how transparent the SeeThroughMaterial is.
    [Tooltip("The time it takes to fade that alpha")]
    [SerializeField] private float FadeTime = 0.1f; // The time it takes to fade that alpha.

    private void Start() // When the game starts
    {
        SolidObject.SetActive(false); // Turns off the solid prop .
        SeeThroughMaterial.SetFloat("_Alpha", 0.5f); // Sets the alpha/see trough value of the prop placeback ghost object.
        AlphaValue = SeeThroughMaterial.GetFloat("_Alpha"); // Assigns AlphaValue to the Materials Alpha.
    }

    private void OnTriggerEnter(Collider other) // Check if a target object is in the collider.
    {
        if (other.gameObject == propPlaceBackVariables.TargetObject) //  If the target(furniture) object touches the collider.
        {
            propPlaceBackVariables.IsInPlace = true; // Set IsInPlace bool to true for game score purposes.
            StartCoroutine(FadeOut()); // Start the FadeOut coroutine.
        }
    }
    
    IEnumerator FadeOut() // Fade out the placeback ghost material
    {
        while (SeeThroughMaterial.GetFloat("_Alpha") > 0f) // Check if alpha is more than 0.
        {
            AlphaValue -= (FadeTime * Time.deltaTime); // Change the AlphaValue over time.
            SeeThroughMaterial.SetFloat("_Alpha", AlphaValue); // Set the materials Alpha equal to AlphaValue.
            SolidObject.SetActive(true); // Turns on the solid prop replacement.
            Destroy(propPlaceBackVariables.TargetObject); // Destroys the prop that has been moved into the collider.
            yield return new WaitForEndOfFrame();
        }
    }
}
