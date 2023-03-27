using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputProcessor : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController Controller = null;
    [SerializeField] private MovementHandler MovementHandler = null;
    [Header("Settings")]
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private float acceleration = 0.1f;

    private float CurrentSpeed;

    private Vector3 PreviousVelocity;
    private Vector2 PreviousInputDirection;

    private Transform MainCameraTransform;

    public Vector3 Value { get; private set; }

    private void OnEnable() => MovementHandler.AddModifier(this);
    private void OnDisable() => MovementHandler.RemoveModifier(this);

    private void Start() => MainCameraTransform = Camera.main.transform;

    private void Update() => Move();

    public void SetMovementInput(Vector2 inputDirection)
    {
        PreviousInputDirection = inputDirection;
    }

    private void Move()
    {
        float targetSpeed = MovementSpeed * PreviousInputDirection.magnitude;
        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection;

        if(targetSpeed != 0f)
        {
            movementDirection = forward * PreviousInputDirection.y + right * PreviousInputDirection.x;
        }
        else
        {
            movementDirection = PreviousVelocity.normalized;
        }

        Value = movementDirection * CurrentSpeed;

        PreviousVelocity = new Vector3(Controller.velocity.x, 0f, Controller.velocity.z);

        CurrentSpeed = PreviousVelocity.magnitude;
    }
}
