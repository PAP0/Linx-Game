using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

/// 
/// Authors: Bjornraaf & PAP0
/// 
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private CharacterController Controller;

    [Header("Movement")]
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpForce = 1.5f;

    [Header("Grabbing")]
    [SerializeField] private float MaxGrabWeight = 50.0f;

    [Header("Animation")] 
    [SerializeField] private Animator PlayerAnimator;

    private readonly float GravityMagnitude = Physics.gravity.y;
    private float Velocity;
    private Rigidbody RB;

    private Vector2 Movement;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        RB = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Gravity();

        if (Movement.x >= 0.1f || Movement.y >= 0.1f || Movement.x <= -0.1f || Movement.y <= -0.1f)
        {
            PlayerAnimator.SetBool("IsRunning", true);
            MovePlayer();
        }
        else
        {
            PlayerAnimator.SetBool("IsRunning", false);
        }
    }

    private void Gravity()
    {
        if (Velocity < 0.0f)
        {
            Velocity = -1.0f;
        }
        else
        {
            Velocity += GravityMagnitude * Time.fixedDeltaTime;
        }
    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(Movement.x, Velocity, Movement.y);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Movement.x, 0, Movement.y)), 0.15f);
        Controller.Move(move * Speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit controllerHit)
    {
        Rigidbody rigidbody = controllerHit.collider.attachedRigidbody;

        if (rigidbody != null && controllerHit.gameObject.CompareTag("Grabbable"))
        {
            if (MaxGrabWeight >= rigidbody.mass)
            {
                Vector3 forceDirection = controllerHit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * ((MaxGrabWeight - rigidbody.mass) / 100 + 0.5f), transform.position, ForceMode.Impulse);
            }
        }
        
    }
    private void Drag()
    {
        
    }

}