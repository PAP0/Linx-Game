using System.Collections;
using UnityEngine;

/// <summary>  
/// This script checks whether a TargetObject(Furniture or prop) has entered the collider of a prop ghost and fades out the alpha of the ghost material.
/// </summary>
 
public class PropPlaceback : MonoBehaviour
{
    #region Variables

    [Header("Replacement variables")]
    [Tooltip("The gameobject that gets turned on when the furniture is placed")]
    // The gameobject that gets turned on when the furniture is placed.
    public GameObject solidObject; 
    
    [Header("Script references")]
    [Tooltip("Reference to the variables script")]
    // Reference to the variables script.
    [SerializeField] private PropPlaceBackVariables PropPlaceBackVariables; 
    
    [Header("Material Variables")]
    [Tooltip("Material of which the alpha is changed once the Target object is in place")]
    // Material of which the alpha is changed once the Target object is in place.
    [SerializeField] private Material SeeThroughMaterial; 
    [Tooltip("The value of how transparent the SeeThroughMaterial is")]
    // The value of how transparent the SeeThroughMaterial is.
    [Range(0f, 0.5f)] private float AlphaValue; 
    [Tooltip("The time it takes to fade that alpha")]
    // The time it takes to fade that alpha.
    [SerializeField] private float FadeTime = 0.1f;

    #endregion

    #region Unity Events

    // When the game starts.
    private void Start()
    {
        // Turns off the solid prop.
        solidObject.SetActive(false);
        // Sets the alpha/see trough value of the prop placeback ghost object.
        SeeThroughMaterial.SetFloat("_Alpha", 0.5f);
        // Assigns AlphaValue to the Materials Alpha.
        AlphaValue = SeeThroughMaterial.GetFloat("_Alpha");
    }

    // Checks if a target object is in the collider.
    private void OnTriggerEnter(Collider other) 
    {
        // If the target(furniture) object touches the collider.
        if (other.gameObject == PropPlaceBackVariables.targetObject) 
        {
            // Set IsInPlace bool to true for game score purposes.
            PropPlaceBackVariables.IsInPlace = true;
            // Start the FadeOut coroutine.
            StartCoroutine(FadeOut()); 
        }
    }

    #endregion

    #region Coroutines

    // Fades out the placeback ghost material.
    IEnumerator FadeOut() 
    {
        // Check if alpha is more than 0.
        while (SeeThroughMaterial.GetFloat("_Alpha") > 0f)
        {
            // Change the AlphaValue over time.
            AlphaValue -= (FadeTime * Time.deltaTime);
            // Set the materials Alpha equal to AlphaValue.
            SeeThroughMaterial.SetFloat("_Alpha", AlphaValue);
            // Turns on the solid prop replacement.
            solidObject.SetActive(true);
            // Destroys the prop that has been moved into the collider.
            Destroy(PropPlaceBackVariables.targetObject);
            // Waits till the end of the frame.
            yield return null;
        }
    }

    #endregion
}