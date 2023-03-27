using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController Controller = null;
    [SerializeField] private MovementHandler MovementHandler = null;

    [Header("Settings")]
    [SerializeField] private float mass = 1f;
    [SerializeField] private float drag = 5f;

    private bool WasGroundedLastFrame;

    public Vector3 Value { get; private set; }

    private void OnEnable() => MovementHandler.AddModifier(this);
    private void OnDisable() => MovementHandler.RemoveModifier(this);

    private void Update()
    {
        if (!WasGroundedLastFrame && Controller.isGrounded)
        {
            Value = new Vector3(Value.x, 0f, Value.z);
        }

        WasGroundedLastFrame = Controller.isGrounded;

        if (Value.magnitude < 0.2f)
        {
            Value = Vector3.zero;
        }
        Value = Vector3.Lerp(Value, Vector3.zero, drag * Time.deltaTime);
    }

    public void AddForce(Vector3 force) => Value += force / mass;
}
