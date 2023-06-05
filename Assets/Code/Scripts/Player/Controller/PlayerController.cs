using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController Controller;

    [Header("Movement")]
    [SerializeField] private float Speed = 5.0f;

    [Header("Revive")]
    [SerializeField] private float ReviveDistance;
    [SerializeField] private float ReviveTime;
    [SerializeField] private LayerMask Mask;

    [Header("Animation")] 
    [SerializeField] private Animator PlayerAnimator;
    
    private readonly float GravityMagnitude = Physics.gravity.y;
    private float Velocity;

    private Vector2 Movement;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
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
}