using UnityEngine;

/// <summary>  
/// This script contains a few Variables that are being used in the PropPlaceback script.
/// </summary>

public class PropPlaceBackVariables : MonoBehaviour
{
    [Header("Target Object")]
    [Tooltip("The predefined gameobject we want to detect")]
    public GameObject TargetObject; // The predefined gameobject we want to detect.
    
    [Header("Score Modifier")]
    [Tooltip("The bool that changes depending if the object is in the area or not")]
    public bool IsInPlace; // The bool that changes depending if the object is in the area or not
}
