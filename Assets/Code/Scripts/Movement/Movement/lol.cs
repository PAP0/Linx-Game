using UnityEngine;
using UnityEngine.InputSystem;

public class lol : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public float grabDistance = 2f;
    [SerializeField] public float throwForce = 10f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private GameObject heldObject = null;

    // PlayerInputs/Controls
    Playerinputs playerControls;
    private InputAction move;
    private InputAction jump;
    private InputAction grab;

    private void Awake()
    {
        playerControls = new Playerinputs();
    }
    private void OnEnable()
    {
        move = playerControls.Keyboard.Move;
        move.Enable();

        jump = playerControls.Keyboard.Jump;
        jump.Enable();
        jump.performed += Jump;

        grab = playerControls.Keyboard.Grab;
        grab.Enable();
        grab.performed += Grab;
    } 
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = move.ReadValue<Vector3>();
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
    }

    private void Grab(InputAction.CallbackContext context2)
    {
        if (heldObject == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.SetParent(transform);
            }
        }
        else
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
            heldObject = null;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
