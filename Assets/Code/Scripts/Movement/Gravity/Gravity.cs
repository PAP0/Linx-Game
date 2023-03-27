using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController Controller = null;
    //[SerializeField] private PlayerController MovementHandler = null;

    [Header("Settings")]
    [SerializeField] private float GroundedPullMagnitude = 5f;

    Vector3 Height;

    private readonly float GravityMagnitude = Physics.gravity.y;

    private bool WasGroundedLastFrame;

    private void Update() => ProcessGravity();

    private void ProcessGravity()
    {
        if (Controller.isGrounded)
        {
            Height = new Vector3(transform.position.x, -GroundedPullMagnitude, transform.position.z);
        }
        else if (WasGroundedLastFrame)
        {
            Height = Vector3.zero;
        }
        else
        {
            Height = new Vector3(Height.x, Height.y + GravityMagnitude * Time.deltaTime, Height.z);
        }

        WasGroundedLastFrame = Controller.isGrounded;
    }
}
