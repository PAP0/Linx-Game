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
    [SerializeField] private CharacterControllerEnabler characterControllerEnabler;
    [Header("References")][SerializeField]
    private CharacterController Controller;

    [Header("Movement")][SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpForce = 1.5f;

    [Header("Grabbing")]
    [SerializeField] private float GrabDistance = 3f;
    [SerializeField] private float MaxGrabWeight = 50.0f;
    [SerializeField] private BoxCollider HeldObjectCollider;
    //[SerializeField] private float GravityMultiplier = 1f;
    //[Header("Pushing")]
    //[SerializeField] private float ForceMagnitude = 1.0f;

    [Header("Throwing")]
    [SerializeField] private float ThrowForce = 50.0f;
    [SerializeField] private float PlayerThrowForce = 50.0f;
    [SerializeField] private float throwAngle = 10.0f;
    [SerializeField] private float PlayerThrowAngle = 30.0f;

    [Header("Animation")]
    [SerializeField] private PlayerAnimation playerAnimation;

 

    private GameObject HeldObject = null;

    private bool IsGrabbing;

    private readonly float GravityMagnitude = Physics.gravity.y;
    private float Velocity;
    private float GrabSpeed;

    private Vector2 Movement;
    private Vector2 JoystickLook;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        JoystickLook = context.ReadValue<Vector2>();
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        // Check if the character is already holding an object
        if (IsGrabbing)
        {
            ReleaseObject();
        }
        else
        {
            GrabObject();
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        ThrowObject();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        Velocity += JumpForce;
        playerAnimation.AnimationNumer = 1f;
    }

    private void Update()
    {
        Controller.detectCollisions = !IsGrabbing;
        Gravity();

        if (JoystickLook.x == 0 && JoystickLook.y == 0)
        {
            MovePlayer();
        }
        else
        {
            RotatePlayerWithAim();
        }

        if (IsGrabbing && HeldObject != null)
        {
            // Calculate the new position for the grabbed object
            //Vector3 newPosition = transform.position + transform.forward * Mathf.Clamp(1f, GrabDistance, MaxGrabWeight);
            Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z) +
                                  transform.forward * GrabDistance;

            // Update the position of the grabbed object
            HeldObject.transform.position = newPosition;
        }

        if(IsGrabbing)
        {
            HeldObjectCollider.enabled = true;
        }
        else
        {
            HeldObjectCollider.enabled = false;

        }
    }

    private void Gravity()
    {
        if (IsGrounded() && Velocity < 0.0f)
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
        Vector3 look = new Vector3(Movement.x, 0f, Movement.y);
        if (look != Vector3.zero)
        {
            if (IsGrabbing)
            {
                look = Vector3.zero;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), 0.15f);
            }
        }

        if (IsGrabbing)
        {
            Controller.Move(move * GrabSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Controller.Move(move * Speed * Time.fixedDeltaTime);
        }

     playerAnimation.AnimationNumer = 0.5f;

    }

    private void RotatePlayerWithAim()
    {
        Vector3 aimDirection = new Vector3(JoystickLook.x, 0f, JoystickLook.y);

        if (aimDirection != Vector3.zero)
        {
            if (IsGrabbing)
            {
                aimDirection = Vector3.zero;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }

        Vector3 movement = new Vector3(Movement.x, 0f, Movement.y);

        transform.Translate(movement * Speed * Time.fixedDeltaTime, Space.World);
    }

    private void GrabObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x, 0, transform.position.z), transform.forward);

        if (Physics.Raycast(ray, out hit, GrabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                Rigidbody objectRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (objectRigidbody.mass <= MaxGrabWeight)
                {
                    HeldObject = hit.collider.gameObject;
                    objectRigidbody.isKinematic = true;
                    IsGrabbing = true;

                    GrabSpeed = (MaxGrabWeight - objectRigidbody.mass) / 10;
                    if (GrabSpeed >= Speed)
                    {
                        GrabSpeed = Speed;
                    }
                    else if (MaxGrabWeight == objectRigidbody.mass)
                    {
                        GrabSpeed = 0.5f;
                    }
                }
            }
            else if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<CharacterController>().enabled = false;
                Rigidbody playerObjectRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (playerObjectRigidbody.mass <= MaxGrabWeight)
                {
                    HeldObject = hit.collider.gameObject;
                    playerObjectRigidbody.isKinematic = true;
                    IsGrabbing = true;

                    GrabSpeed = (MaxGrabWeight - playerObjectRigidbody.mass) / 10;
                    if (GrabSpeed >= Speed)
                    {
                        GrabSpeed = Speed;
                    }
                    else if (MaxGrabWeight == playerObjectRigidbody.mass)
                    {
                        GrabSpeed = 1.0f;
                    }
                }
            }
        }
    }

    private void ReleaseObject()
    {
        if (HeldObject != null)
        {
            if (HeldObject.CompareTag("Player")) // if holding a player object
            {
                HeldObject.GetComponent<CharacterController>().enabled = true;
            }
            else // if holding a regular grabbable object
            {
                HeldObject.GetComponent<Rigidbody>().isKinematic = false;
            }

            HeldObject.transform.parent = null;
            HeldObject = null;
            IsGrabbing = false;

            // Enable collisions
            Controller.detectCollisions = true;
        }
    }

    private void ThrowObject()
    {
        if (!IsGrabbing)
        {
            return;
        }

        if (HeldObject.CompareTag("Grabbable"))
        {
            HeldObject.GetComponent<Rigidbody>().isKinematic = false;

            HeldObject.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce + Vector3.up * throwAngle,ForceMode.Impulse);
            HeldObject.GetComponent<Rigidbody>().AddTorque(transform.right * throwAngle,ForceMode.Impulse);
        }

        if (HeldObject.CompareTag("Player"))
        {
            HeldObject.GetComponent<Rigidbody>().isKinematic = false;
            if (IsGrabbing && HeldObject != null)
            {
                // Calculate the direction to throw the object
                Vector3 throwDirection = -transform.forward;

                // Calculate the initial velocity of the object to give it an upward angle
                float angleInRadians = throwAngle * Mathf.Deg2Rad;
                float verticalVelocity = Mathf.Sin(angleInRadians) * PlayerThrowForce;
                float horizontalVelocity = Mathf.Cos(angleInRadians) * PlayerThrowForce;
                Vector3 throwVelocity = throwDirection * horizontalVelocity + Vector3.up * verticalVelocity;

                // Get the PlayerController component of the held object, if any
                PlayerController heldObjectController = HeldObject.GetComponent<PlayerController>();

                // If the held object has a PlayerController and is also holding another player, throw the held object 
                // in the opposite direction to the throwing player's facing direction
                if (heldObjectController != null && heldObjectController.IsGrabbing)
                {
                    throwVelocity = -transform.forward * horizontalVelocity + Vector3.up * verticalVelocity;
                }

                // Add velocity to the object to give it the desired angle
                Rigidbody heldObjectRigidbody = HeldObject.GetComponent<Rigidbody>();
                heldObjectRigidbody.velocity = throwVelocity;

                // Add torque to the object to give it spin
                heldObjectRigidbody.AddTorque(transform.right * PlayerThrowAngle, ForceMode.Impulse);
                StartCoroutine(HeldObject.GetComponent<CharacterControllerEnabler>().EnableCharacterController());

                // Reset held object and grab flag
                HeldObject = null;
                IsGrabbing = false;
            }
        }
        HeldObject = null;
        IsGrabbing = false;
    }
    

    private void OnControllerColliderHit(ControllerColliderHit controllerHit)
    {
        Rigidbody rigidbody = controllerHit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 forceDirection = controllerHit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * ((MaxGrabWeight / 100) * 0.5f), transform.position,
                ForceMode.Impulse);
        }
    }
    
    public bool IsGrounded() => Controller.isGrounded;

    private void OnDrawGizmosSelected()
    {
        // Draw a line in the scene view to show the grab distance
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * GrabDistance);
    }
}