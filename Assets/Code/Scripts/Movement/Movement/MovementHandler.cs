using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController Controller = null;

    private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

    // Update is called once per frame
    private void Update() => Move();

    public void AddModifier(IMovementModifier modifier) => modifiers.Add(modifier);
    public void RemoveModifier(IMovementModifier modifier) => modifiers.Remove(modifier);

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        foreach(IMovementModifier modifier in modifiers)
        {
            movement += modifier.Value;
        }

        Controller.Move(movement * Time.deltaTime);
    }
}
