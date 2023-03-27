using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent (typeof (Camera))]

public class CameraScript : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float MaxZoom = 10f;
    [SerializeField] private float MinZoom = 40f;
    [SerializeField] private float ZoomLimiter = 50f;

    [SerializeField] private Vector3 velocity;
    [SerializeField] private float smoothtime = .5f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }

    private void Zoom()
    {

        float newZoom = Mathf.Lerp(MaxZoom, MinZoom, GetGreatestDistance() / ZoomLimiter);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);

    }

    private void Move()
    {
        Vector3 centerpoint = GetCenterPoint();

        Vector3 newPosition = centerpoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothtime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate (targets[i].position);
        }

        return bounds.center;
    }

}
