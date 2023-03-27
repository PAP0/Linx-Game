using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRider : MonoBehaviour
{
    private Rigidbody _RB;
    private RaycastHit _rayHit;
    Ray downray;
    private Vector3 RaySpawn;
    private bool _rayDidHit = true;

    [Header("Raycast Setup")]
    [SerializeField] private Vector3 DownDir;
    [SerializeField] private float MaxRayDistance;
    [SerializeField] private LayerMask LayersToHit;
    [SerializeField] private float GroundThreshold;
    [Header("Ride Setup")]
    [SerializeField] private float RideHeight;
    [SerializeField] private float RidespringStrength;
    [SerializeField] private float RidespringDamper;

    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody>();
        RaySpawn = new Vector3(transform.position.x, -1, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit();

    }
    private void RaycastHit()
    {
        downray = new Ray(RaySpawn, DownDir);
        Debug.DrawRay(RaySpawn, DownDir, Color.white);

        if (Physics.Raycast(downray, out _rayHit, MaxRayDistance, LayersToHit))
        {
            if (MaxRayDistance - GroundThreshold == _rayHit.distance)
            {
                _rayDidHit = false;
            }
        }
        else
        {
            _rayDidHit = true;
        }

        if (_rayDidHit == false)
        {
            Vector3 vel = _RB.velocity;
            Vector3 rayDir = transform.TransformDirection(DownDir);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = _rayHit.rigidbody;

            if (hitBody != null)
            {
                otherVel = hitBody.velocity;
            }

            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            float relVel = rayDirVel - otherDirVel;

            float x = _rayHit.distance - RideHeight;

            float springforce = (x * RidespringStrength) - (relVel * RidespringDamper);

            Debug.DrawLine(transform.position, transform.position + (rayDir * springforce), Color.red);

            _RB.AddForce(rayDir * springforce);

            if (hitBody != null)
            {
                hitBody.AddForceAtPosition(rayDir * -springforce, _rayHit.point);
            }
        }
    }
    public void UpdateUprightForce(float elapsed)
    {
        //Quaternion characterCurrent = transform.rotation;
        //Quaternion toGoal = UtilsMath.ShortestRotation(_uprightJointTargetRot, characterCurrent);
            
        //Vector3 rotAxis;
        //float rotDegrees;

        //toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        //rotAxis.Normalize();

        //float rotRadians = rotDegrees * Math.Deg2Rad;

        //_RB.AddTorque()

    }
}
