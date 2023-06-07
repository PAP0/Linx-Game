using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This script moves specific objects towards the player when the ability is activated & they are within range.
/// </summary>
public class Vacuum : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Tooltip("Reference to the Battery for checking the amount of energy")]
    [SerializeField] private Battery BatteryScript;

    [Header("Ranges")]
    [Tooltip("The radius of the suction area of the player.")]
    [SerializeField] private float SuctionRadius;

    [Tooltip("The Layers the Vacuum is able to interact with.")]
    [SerializeField] private LayerMask SuckableLayers;

    [Header("Variables")]
    [Tooltip("The Amount of speed the object moves from the start of suction.")]
    [SerializeField] private float SuctionStartSpeed;

    [Tooltip("The max amount of speed the object can move by.")]
    [SerializeField] private float SuctionMaxSpeed;

    [Tooltip("The Way the object moves through the Acceleration")]
    [SerializeField] private AnimationCurve VelocityCurve;

    [Tooltip("The amount by which the object can accelerate.")]
    [SerializeField] private float AccelerationSpeed;

    [Tooltip("The Bool that check if the Vacuum ablity is active or not")]
    public bool IsSucking;

    // Current Acceleration of the objects.
    private float Acceleration;
    // Current Speed of the objects.
    private float Speed;

    #endregion

    #region UnityEvents

    // Checks every frame.
    private void Update()
    {
        if (BatteryScript == null)
        {
            BatteryScript = FindObjectOfType<Battery>();
        }

        // Turns off Vacuum when energy is low enough.
        if (BatteryScript.currentBattery <= 1)
        {
            IsSucking = false;
        }
        else
        {
            BatteryScript.UseEnergy(IsSucking);
        }

        Suck();
    }

    #endregion

    #region Public Events

    /// <summary>
    /// Activates the Vacuum Ablity when button is pressed & held on Controller and there is energy available. 
    /// </summary>
    public void OnSuck(InputAction.CallbackContext context)
    {
        if (BatteryScript.currentBattery >= 1f)
        {
            if (context.performed)
            {
                IsSucking = true;
            }
            else if (context.canceled)
            {
                IsSucking = false;
            }
        }
    }

    #endregion

    #region Private Events

    // Sucks Objects within a range towards the player.
    private void Suck()
    {
        // Finds all the interactable objects within range.
        Collider[] colliders = Physics.OverlapSphere(transform.position, SuctionRadius, SuckableLayers);
        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            Vector3 direction = transform.position - collider.transform.position;
            Acceleration += Time.deltaTime * AccelerationSpeed;
            Speed = Mathf.Lerp(SuctionStartSpeed, SuctionMaxSpeed, VelocityCurve.Evaluate(Acceleration / 1f));
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (IsSucking)
            {
                // Check if the object is within Radius.
                if (distance < SuctionRadius)
                {
                    // Object moves towards the player.
                    rb.velocity = direction.normalized * Speed;

                    // Movement gets stopped if  the object is close enough to the player.
                    if (distance - SuctionRadius >= 0.1f)
                    {
                        rb.velocity = Vector3.zero;
                        Acceleration = 0;
                    }
                }
            }
            else
            {
                // The Object's movement gets stopped.
                rb.velocity = Vector3.zero;
                Acceleration = 0;
            }
        }
    }

    #endregion
}
