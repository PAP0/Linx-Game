using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent (typeof (Camera))]

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] Targets;
    [SerializeField] private Vector3 Offset;
    [Header("Zoom")]
    [SerializeField] private float MaxZoom = 10f;
    [SerializeField] private float MinZoom = 40f;
    [SerializeField] private float ZoomLimiter = 50f;
    [Header("Smoothing")]
    [SerializeField] private float smoothtime = .5f;

    private Vector3 Velocity;
    private Camera MainCamera;

    private void Start()
    {
        MainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Targets = GameObject.FindGameObjectsWithTag("Player");
    }
    private void LateUpdate()
    {
        if (Targets.Length == 0)
            return;
        Move();
        Zoom();
    }

    private void Zoom()
    {

        float newZoom = Mathf.Lerp(MaxZoom, MinZoom, GetGreatestDistance() / ZoomLimiter);

        MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, newZoom, Time.deltaTime);

    }

    private void Move()
    {
        Vector3 centerpoint = GetCenterPoint();

        Vector3 newPosition = centerpoint + Offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref Velocity, smoothtime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(Targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < Targets.Length; i++)
        {
            bounds.Encapsulate(Targets[i].transform.position);
        }
        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (Targets.Length == 1)
        {
            return Targets[0].transform.position;
        }

        var bounds = new Bounds(Targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < Targets.Length; i++)
        {
            bounds.Encapsulate (Targets[i].transform.position);
        }

        return bounds.center;
    }

}
