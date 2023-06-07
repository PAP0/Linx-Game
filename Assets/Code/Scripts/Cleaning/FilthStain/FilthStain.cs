using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

/// <summary>  
/// This script check if the vacuum player is nearby the filth and check if the player is using their, ability, allowing them to pickup the filth.
/// </summary>

public class FilthStain : MonoBehaviour
{
    #region Variables

    [Header("Vacuum")]
    [Tooltip("Reference to a Vacuum script")]
    public Vacuum vacuumScript; 

    [Header("Pickup Range")]
    [Tooltip("The range within which the filth stain can be detected")]
    [SerializeField] private float Range; 

    [Header("Filth Type")]
    [Tooltip("Indicates if the filth stain is a hidden type or not")]
    public bool IsHiddenFilth;   
    [Tooltip("Indicates if the filth stain is exposed")]
    public bool IsExposed;

    [Header("Score")]
    [Tooltip("Reference to a ScoreScriptableObject for score keeping")]
    [SerializeField] private ScoreScriptableObject ScoreHolder;

    #endregion

    #region Unity Events

    // Every frame.
    void Update()
    {
        // Find the game object with the tag "AbilityBarrierVac" and assign it to the vacuumGuy variable.
        GameObject vacuumGuy = GameObject.FindGameObjectWithTag("AbilityBarrierVac");
        // Calculate the distance between the filth stain's position and the vacuumGuy's position.
        float distanceVacuum = Vector3.Distance(transform.position, vacuumGuy.transform.position);
        // Check if the distance between the filth stain and the vacuumGuy is within the range, 
        // the vacuum is sucking, and the filth stain is not a hidden type.
        if (distanceVacuum <= Range && vacuumScript.IsSucking && !IsHiddenFilth)
        {
            // Increase the score value in ScoreHolder.
            ScoreHolder.ScoreValue++;
            // Destroy the filth stain game object.
            Destroy(gameObject);
        }

        // Check if the distance between the filth stain and the vacuumGuy is within the range,
        // the vacuum is sucking, the filth stain is exposed, and the filth stain is a hidden type.
        if (distanceVacuum <= Range && vacuumScript.IsSucking && IsExposed && IsHiddenFilth)
        {
            // Increase the score value in ScoreHolder.
            ScoreHolder.ScoreValue++;
            // Destroy the filth stain game object.
            Destroy(gameObject);
        }
    }

    // Draws a gizmo in the editor, representing the detection range.
    private void OnDrawGizmos()
    {
        // Set the color of Gizmos to gray.
        Gizmos.color = Color.gray;
        // Draw a wire sphere Gizmo at the position of the filth stain with the specified range.
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    #endregion
}