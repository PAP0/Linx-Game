using UnityEngine;

public class PushCheck : MonoBehaviour
{
    [SerializeField] private float MaxDistance = 2f;
    [SerializeField] private LayerMask Mask;

    private void Update()
    {
        RangeCheck();
    }

    private void RangeCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, MaxDistance, Mask);
        CapsuleCollider capsuleCollider;
        if (colliders.Length > 1)
        {
            foreach (Collider collider in colliders)
            {
                capsuleCollider = collider.GetComponentInParent<CapsuleCollider>();
                capsuleCollider.enabled = true;
            }
        }
        else
        {
            foreach (Collider collider in colliders) 
            {
                capsuleCollider = collider.GetComponentInParent<CapsuleCollider>();
                capsuleCollider.enabled = false;
            }
        }
    }
}
