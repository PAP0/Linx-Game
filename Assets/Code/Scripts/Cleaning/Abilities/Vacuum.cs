using UnityEngine;
using UnityEngine.InputSystem;

public class Vacuum : MonoBehaviour
{
    public bool IsSucking;
    [Header("Ranges")]
    [SerializeField] private float SuctionRadius; // the radius of the suction area
    [SerializeField] private LayerMask SuckableLayers; // the layers that the vacuum can suck

    [Header("Variables")]
    [SerializeField] private float SuctionStartSpeed;
    [SerializeField] private float SuctionMaxSpeed;
    [SerializeField] private AnimationCurve VelocityCurve;
    [SerializeField] private float AccelerationSpeed;
    [SerializeField] private Battery BatteryScript;

    private float Acceleration;
    private float Speed;

    public void OnSuck(InputAction.CallbackContext context)
    {
        if(BatteryScript.currentBattery >= 1f)
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

    private void Update()
    {
        if (BatteryScript == null)
        {
            BatteryScript = FindObjectOfType<Battery>();
        }

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
    private void Suck()
    {
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
                if (distance < SuctionRadius)
                {
                    rb.velocity = direction.normalized * Speed;

                    if (distance - SuctionRadius >= 0.1f)
                    {
                        rb.velocity = Vector3.zero;
                        Acceleration = 0;
                    }
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
                Acceleration = 0;
            }
        }
    }
}
