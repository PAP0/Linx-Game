using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController Controller;

    [Header("Movement")]
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float JumpForce = 1.5f;

    [Header("Grabbing")]
    [SerializeField] private float GrabDistance = 3f;
    [SerializeField] private float MaxGrabWeight = 50.0f;
    //[SerializeField] private float GravityMultiplier = 1f;

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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        Velocity += JumpForce;
    }

    private void Update()
    {
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
            Vector3 newPosition = transform.position + transform.forward * GrabDistance;

            // Update the position of the grabbed object
            HeldObject.transform.position = newPosition;
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
            Velocity += GravityMagnitude * Time.deltaTime;
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
            Controller.Move(move * GrabSpeed * Time.deltaTime);
        }
        else
        {
            Controller.Move(move * Speed * Time.deltaTime);
        }
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

        transform.Translate(movement * Speed * Time.deltaTime, Space.World);
    }

    private void GrabObject()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        // Check if the character is looking at an object that can be grabbed
        if (Physics.Raycast(ray, out hit, GrabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                // Check if the object's weight is below the maximum allowed weight
                Rigidbody objectRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (objectRigidbody.mass <= MaxGrabWeight)
                {
                    // Set the grabbed object and update IsGrabbing
                    HeldObject = hit.collider.gameObject;
                    HeldObject.GetComponent<Rigidbody>().isKinematic = true;
                    IsGrabbing = true;

                    // Different speed depending on the object weight/mass
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
        }
    }

    private void ReleaseObject()
    {
        if (HeldObject != null)
        {
            // Release the grabbed object and update the isGrabbing flag
            HeldObject.GetComponent<Rigidbody>().isKinematic = false;
            HeldObject = null;
            IsGrabbing = false;
        }
    }

    private bool IsGrounded() => Controller.isGrounded;
}
