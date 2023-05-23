using UnityEngine;
using UnityEngine.InputSystem;

public class Vacuum : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Stamina PlayerStamina;
    [SerializeField] private StaminaScritableObject StaminaObject;

    [Header("Ranges")]
    [SerializeField] private float SuctionRadius; // the radius of the suction area
    [SerializeField] private LayerMask SuckableLayers; // the layers that the vacuum can suck

    [Header("Variables")]
    [SerializeField] private float SuctionStartSpeed;
    [SerializeField] private float SuctionMaxSpeed;
    [SerializeField] private AnimationCurve VelocityCurve;
    [SerializeField] private float AccelerationSpeed;

    private float Acceleration;
    private float Speed;
    private bool IsSucking = false;

    public void OnSuck(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        IsSucking = !IsSucking;

    }

    private void Update()
    {
        Suck();

        if (StaminaObject.CurrentStamina <= 1)
        {
            IsSucking = false;
        }
        else
        {
            PlayerStamina.UseEnergy(IsSucking);
        }
        
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
