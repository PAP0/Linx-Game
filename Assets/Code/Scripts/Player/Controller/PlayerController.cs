using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This Script makes sure the player can move through the scene using the new InputSystem.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Variables

    // Gravity value that will always be the same.
    private static readonly float GravityMagnitude = Physics.gravity.y;

    [Header("References")]
    [Tooltip("Reference to the CharacterController used for movement")]
    [SerializeField] private CharacterController Controller;

    [Header("Animation")]
    [Tooltip("The Animator that plays and holds animations of the player")]
    [SerializeField] private Animator PlayerAnimator;

    // Checks if the player can move or not.
    public bool CanWalk = true;

    // The Value of how quickly the player moves towards a position.
    private readonly float Speed = 5.0f;

    // The Value of how quickly the player moves back down towards the ground.
    private float Velocity;

    // The Vector of what direction the player is supposed to move to.
    private Vector2 Movement;

    #endregion

    #region Unity Events

    // When the script is initialized.
    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
    }

    // Checks every frame.
    private void Update()
    {
        Gravity();

        // Checks if there's enough input from the joystick to move.
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

    #endregion

    #region Public Events

    /// <summary>
    /// Checks where the Joystick is pointed towards and gives this value back.
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    #endregion

    #region Private Events

    // Move the player down when they are in the air.
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

    // Grabs the Movement from the Joystick and moves the player towards that postition.
    private void MovePlayer()
    {   
        if (CanWalk)
        {
            // Sets the values needed for the movement.
            Vector3 move = new Vector3(Movement.x, Velocity, Movement.y);
            // Rotates the player to look where it moves towards.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Movement.x, 0, Movement.y)), 0.15f);
            Controller.Move(move * Speed * Time.deltaTime);
        }
    }

    #endregion
}