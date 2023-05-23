using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
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
    //[Header("Grabbing")]
    //[SerializeField] private float MaxGrabDistance = 2f;   // Maximum distance the player can grab an object from.
    //[SerializeField] private float MaxGrabWeight = 50.0f;
    //[SerializeField] private float GrabSpeed = 20f;
    //[SerializeField] private LayerMask GrabMask;          // Layer mask for objects that can be grabbed
    //[SerializeField] private bool IsGrabbing;

    [Header("Animation")] 
    [SerializeField] private Animator PlayerAnimator;

    private float WaitTimer;
    private bool isReviving = false;
    private readonly float AutoReviveTime = 10f;
    
    private readonly float GravityMagnitude = Physics.gravity.y;
    private float Velocity;

    //private Rigidbody RB;

    //private GameObject heldObject;       // Object currently being held by the player

    //private Vector3 holdOffset;          // Offset between the player's hand and the center of the held object
    [HideInInspector] public Vector2 Movement;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        //RB = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnRevive(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        isReviving = !isReviving;
    }
    //public void OnGrab(InputAction.CallbackContext context)
    //{
    //    if (!context.started) return;

    //    // Check if the character is already holding an object
    //    if (IsGrabbing)
    //    {
    //        ReleaseObject();
    //    }
    //    else
    //    {
    //        GrabObject();
    //    }
    //}

    private void Update()
    {
        Gravity();
        //Revive();

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

        //if (!IsGrabbing)
        //{
        //    return;
        //}
        //else
        //{
        //    Quaternion lookRotation = Quaternion.LookRotation((heldObject.transform.position - transform.position).normalized);
        //    transform.rotation = Quaternion.Slerp(lookRotation, Quaternion.LookRotation(new Vector3(lookRotation.x, 0, lookRotation.z)), 0.15f);
        //}
    }

    //private void Revive()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, ReviveDistance, Mask);
    //    if (colliders.Length > 0 && isReviving)
    //    {
    //        WaitTimer = ReviveTime;
    //        StartCoroutine(WaitForRevive());
    //    }
    //    else if (colliders.Length < 0)
    //    {
    //        StopCoroutine(WaitForRevive());
    //    }
    //    else
    //    {
    //        WaitTimer = AutoReviveTime;
    //        StartCoroutine(WaitForRevive());
    //    }
    //}

    IEnumerator WaitForRevive()
    {
        yield return new WaitForSeconds(WaitTimer);
        PlayerStamina.UseEnergy(false);
        StaminaObj.CurrentStamina = 2;
        enabled = true;
    }

    //void GrabObject()
    //{
    //    // Raycast to find the closest grabbable object within range
    //    RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxGrabDistance, grabMask);
    //    if (hits.Length > 0)
    //    {
    //        // Find the closest object that is not the player or the player's child
    //        GameObject closestObject = null;
    //        float closestDistance = Mathf.Infinity;
    //        foreach (RaycastHit hit in hits)
    //        {
    //            float distance = Vector3.Distance(transform.position, hit.transform.position);
    //            if (distance < closestDistance && hit.transform.gameObject != gameObject && hit.transform.parent != transform)
    //            {
    //                closestObject = hit.transform.gameObject;
    //                closestDistance = distance;
    //            }
    //        }

    //        // If a grabbable object was found, hold onto it
    //        if (closestObject != null)
    //        {
    //            IsGrabbing = true;
    //            heldObject = closestObject;
    //            holdOffset = transform.position - closestObject.transform.position;
    //        }
    //    }
    //}
    //void GrabObject()
    //{
    //    // Raycast to find the closest grabbable object within range
    //    RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, MaxGrabDistance, GrabMask);
    //    if (hits.Length > 0)
    //    {
    //        // Find the closest object that is not the player or the player's child
    //        GameObject closestObject = null;
    //        float closestDistance = Mathf.Infinity;
    //        foreach (RaycastHit hit in hits)
    //        {
    //            float distance = Vector3.Distance(transform.position, hit.transform.position);
    //            if (distance < closestDistance && hit.transform.gameObject != gameObject && hit.transform.parent != transform)
    //            {
    //                closestObject = hit.transform.gameObject;
    //                closestDistance = distance;
    //            }
    //        }

    //        // If a grabbable object was found, hold onto it
    //        if (closestObject != null)
    //        {
    //            IsGrabbing = true;
    //            heldObject = closestObject;
    //            holdOffset = transform.position - closestObject.transform.position;
    //        }
    //    }
    //}
    //void ReleaseObject()
    //{
    //    heldObject = null;
    //    IsGrabbing = false;
    //}
    //void UpdateHeldObjectPosition()
    //{
    //    // Move the object towards the player's hand
    //    Vector3 targetPosition = transform.position - holdOffset;
    //    heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdOffset, GrabSpeed * Time.deltaTime);
    //}
    //void UpdateHeldObjectPosition()
    //{
    //    Rigidbody rigidbody = heldObject.GetComponent<Rigidbody>();
    //    Vector3 forceDirection = heldObject.transform.position - transform.position;

    //    forceDirection.y = 0;
    //    forceDirection.Normalize();
    //    rigidbody.AddForceAtPosition(-forceDirection * ((MaxGrabWeight - rigidbody.mass) / 100 + 0.5f), heldObject.transform.position, ForceMode.Impulse);

    //    //if (Vector3.Distance(transform.position, heldObject.transform.position) < MaxGrabDistance)
    //    //{
    //    //    forceDirection.y = 0;
    //    //    forceDirection.Normalize();
    //    //    rigidbody.AddForceAtPosition(-forceDirection * ((MaxGrabWeight - rigidbody.mass) / 100 + 0.5f), heldObject.transform.position, ForceMode.Impulse);
    //    //}
    //}
    //void UpdateHeldObjectPosition()
    //{
    //    Rigidbody rigidbody = heldObject.GetComponent<Rigidbody>();
    //    Vector3 forceDirection = heldObject.transform.position - transform.position;

    //    if (Vector3.Distance(transform.position, heldObject.transform.position) < MaxGrabDistance)
    //    {
    //        forceDirection.y = 0;
    //        forceDirection.Normalize();
    //        rigidbody.AddForceAtPosition(-forceDirection * ((MaxGrabWeight - rigidbody.mass) / 100 + 0.5f), heldObject.transform.position, ForceMode.Impulse);
    //    }
    //    else if (Vector3.Distance(transform.position, heldObject.transform.position) > MaxGrabDistance)
    //    {
    //        ReleaseObject();
    //    }
    //}
}