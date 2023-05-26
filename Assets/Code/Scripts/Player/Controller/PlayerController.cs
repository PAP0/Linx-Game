using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Stamina PlayerStamina;
    [SerializeField] private StaminaScritableObject StaminaObj;

    [Header("Movement")]
    [SerializeField] private float Speed = 5.0f;

    [Header("Revive")]
    [SerializeField] private float ReviveDistance;
    [SerializeField] private float ReviveTime;
    [SerializeField] private LayerMask Mask;

    [Header("Animation")] 
    [SerializeField] private Animator PlayerAnimator;

    private float WaitTimer;
    private bool IsStarted = false;
    private readonly float AutoReviveTime = 10f;
    
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

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        IsStarted = !IsStarted;
    }

    private void Update()
    {
        Gravity();

        if (StaminaObj.CurrentStamina <= 1)
        {
            IsStarted = false;
            Revive();
        }
        else
        {
            PlayerStamina.UseEnergy(IsStarted);
        }

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
        if (StaminaObj.CurrentStamina <= 1) 
        {
            PlayerAnimator.SetBool("IsRunning", false);
            Controller.Move(Vector3.zero);
        }
        else
        {
            Vector3 move = new Vector3(Movement.x, Velocity, Movement.y);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Movement.x, 0, Movement.y)), 0.15f);
            Controller.Move(move * Speed * Time.deltaTime);
        }
    }

    private void Revive()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ReviveDistance, Mask);
        if (colliders.Length > 1)
        {
            StopCoroutine(WaitForRevive());
            Debug.Log("Revive");
            WaitTimer = ReviveTime;
            StartCoroutine(WaitForRevive());
        }
        else
        {
            StopCoroutine(WaitForRevive());
            Debug.Log("AutoRevive");
            WaitTimer = AutoReviveTime;
            StartCoroutine(WaitForRevive());
        }
    }

    IEnumerator WaitForRevive()
    {
        yield return new WaitForSeconds(WaitTimer);
        PlayerStamina.UseEnergy(false);
    }
}