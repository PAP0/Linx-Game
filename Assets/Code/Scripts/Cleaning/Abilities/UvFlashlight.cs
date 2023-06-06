using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
/// This script handles the battery dude's UV Flashlight.
/// The flashlight has a specified field of view angle and detection range. 
/// The script detects game objects within the flashlight's set field of view and range.
/// </summary>

public class UVFlashlight : MonoBehaviour
{
    #region Variables

    [Header("Target tag")]
    [Tooltip(" Tag of the objects to be detected by the flashlight")]
    [SerializeField] private string TargetTag;

    [Header("Detection Settings")]
    [Tooltip("Field of view angle of the flashlight in which hidden filth can be detected")]
    [SerializeField] private float FovAngle = 90f;
    [Tooltip("Max range in which hidden filth can be detected")]
    [SerializeField] private float DetectionRange = 6.15f;

    #endregion

    #region Unity Events

    // Every frame.
    private void Update()
    {
        // Find all game objects with the specified tag and assign them to the targets array.
        GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTag);

        // A loop for each of the objects in the targets array.
        foreach (GameObject target in targets)
        {
            // Calculate the direction from the flashlight to the target.
            Vector3 directionToTarget = target.transform.position - transform.position;
            // Check if the angle between the flashlight's forward direction and the direction to the target is within half of
            // the flashlight's field of view angle, and if the distance to the target is within the detection range.
            if (Vector3.Angle(transform.forward, directionToTarget) < FovAngle / 2 && directionToTarget.magnitude < DetectionRange)
            {
                // Set the IsExposed bool of the target's FilthStain script to true.
                target.GetComponent<FilthStain>().IsExposed = true;
                // Enable the mesh renderer of the target
                target.GetComponent<MeshRenderer>().enabled = true;
                // Set the isKinematic of the target's Rigidbody component to false
                // So that the vacuum's pulling force does not affect it.
                target.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                // Set the IsExposed bool of the target's FilthStain script to false.
                target.GetComponent<FilthStain>().IsExposed = false;
                // Disable the mesh renderer of the target.
                target.GetComponent<MeshRenderer>().enabled = false;
                // Set the isKinematic of the target's Rigidbody component to true
                // So that the vacuum's pulling force does affect it.
                target.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Draws a gizmo in the editor, representing the fov range.
    private void OnDrawGizmosSelected()
    {
        // Set the color of Gizmo to yellow.
        Gizmos.color = Color.yellow;
        // Set the matrix of Gizmo to the transformation matrix of the flashlight's position, rotation, and scale.
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        // Draw a Gizmo representing the flashlight's field of view.
        Gizmos.DrawFrustum(Vector3.zero, FovAngle, DetectionRange, 0.1f, 1f);
    }

    #endregion
}