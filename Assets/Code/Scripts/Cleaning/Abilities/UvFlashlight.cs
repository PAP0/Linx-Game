using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UvFlashlight : MonoBehaviour
{
    public string targetTag;
    public float fovAngle = 90f;
    public float detectionRange = 10f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, fovAngle, detectionRange, 0.1f, 1f);
    }

    private void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject target in targets)
        {
            Vector3 directionToTarget = target.transform.position - transform.position;

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2 &&
                directionToTarget.magnitude < detectionRange)
            {
                target.GetComponent<FilthStain>().IsExposed = true;
                target.GetComponent<MeshRenderer>().enabled = true;
                target.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                target.GetComponent<FilthStain>().IsExposed = false;
                target.GetComponent<MeshRenderer>().enabled = false;
                target.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
