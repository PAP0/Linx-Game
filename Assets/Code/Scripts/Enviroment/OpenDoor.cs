using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
/// This script checks whether a TargetPlayer has entered the collider of a button and plays an animation to open a mechanical door.
/// </summary>

public class OpenDoor : MonoBehaviour
{
    #region Variables

    [Header("Door Animator")]
    [Tooltip("The animator that opens the door")]
    // Reference to the animator that opens the mechanical doors.
    [SerializeField] private Animator DoorAnimator;

    #endregion

    #region Unity Events

    // When the script is initialized.
    private void Start()
    {
        // Stop/Do not play the animation.
        DoorAnimator.speed = 0;
    }

    // Check if something has entered the collider.
    private void OnTriggerEnter(Collider other)
    {
        // If the collider that enters is a player.
        if(other.CompareTag("Player"))
        {
            // Play the door animation.
            DoorAnimator.speed = 1;
        }
    }

    #endregion
}