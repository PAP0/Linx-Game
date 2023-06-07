using UnityEngine;

/// <summary>  
/// This script contains a few Variables that are being used in the PropPlaceback script.
/// </summary>

public class PropPlaceBackVariables : MonoBehaviour
{
    #region Variables

    [Header("Target Object")]
    [Tooltip("The predefined gameobject we want to detect")]
    // The predefined gameobject we want to detect.
    public GameObject targetObject; 
    
    [Header("Score Modifier")]
    [Tooltip("The bool that changes depending if the object is in the area or not")]
    // The bool that changes depending if the object is in the area or not
    public bool IsInPlace;

    #endregion
}