using UnityEngine;

/// <summary>  
/// This script handles the in-game camera, always keeping the targets in the view and adapting it's position and zoom to make that possible.
/// </summary>

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    #region Variables

    [Header("Offsets")]
    [Tooltip("The offset from the center point of the targets")]
    [SerializeField] private Vector3 CenterOffset; 

    [Header("Zoom")]
    [Tooltip("The maximum field of view (zoom) of the camera")]
    [SerializeField] private float MaxZoom = 10f; 
    [Tooltip("The minimum field of view (zoom) of the camera")]
    [SerializeField] private float MinZoom = 40f; 
    [Tooltip("The distance at which the camera will be fully zoomed out")]
    [SerializeField] private float ZoomLimiter = 50f;

    [Header("Smoothing")]
    [Tooltip("The time it takes for the camera to smoothly move to its target position")]
    [SerializeField] private float SmoothTime = 0.5f;

    // The targets to follow with the camera.
    private GameObject[] TargetPlayers;
    // The current velocity of the camera movement.
    private Vector3 CameraVelocity;
    // Reference to the Camera component.
    private Camera MainCamera;

    #endregion

    #region Unity Events

    // When the script is initialized.
    private void Start()
    {
        // Assign the MainCamera variable to the scene's camera. 
        MainCamera = GetComponent<Camera>();
    }

    // Every frame.
    private void Update()
    {
        // If the camera targets are not assigned yet.
        if(TargetPlayers == null)
        {
            // Find objects that are players and assign them as targets.
            TargetPlayers = GameObject.FindGameObjectsWithTag("Player");
        }
    }

    // After all Update methods are called.
    private void LateUpdate()
    {
        // If there are no targets, exit the method.
        if (TargetPlayers.Length == 0)
            return;

        // Move the camera to the desired position.
        Move();
        // Adjust the camera's field of view (zoom).
        Zoom(); 
    }

    #endregion

    #region Private Methods

    // Moves the camera to the desired position.
    private void Move()
    {
        // Calculates the center point of all targets.
        Vector3 centerPoint = GetCenterPoint();
        // Calculates the desired new position for the camera, offset from the center point.
        Vector3 newPosition = centerPoint + CenterOffset;
        // Smoothly moves the camera towards the new position
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref CameraVelocity, SmoothTime);
    }

    // Adjusts the camera's field of view (zoom).
    private void Zoom()
    {
        // Calculates the new zoom level based on the greatest distance between targets and the zoom limiter.
        float newZoom = Mathf.Lerp(MaxZoom, MinZoom, GetGreatestDistance() / ZoomLimiter);
        // Smoothly interpolate the camera's field of view towards the new zoom level.
        MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, newZoom, Time.deltaTime);
    }

    // Calculates greatest distance between target players.
    private float GetGreatestDistance()
    {
        // Calculates the bounding box that encapsulates all targets.
        Bounds bounds = new Bounds(TargetPlayers[0].transform.position, Vector3.zero);
        for (int i = 0; i < TargetPlayers.Length; i++)
        {
            bounds.Encapsulate(TargetPlayers[i].transform.position);
        }
        // Return the size of the bounding box in the x-axis, which represents the greatest distance between targets.
        return bounds.size.x;
    }

    // Calculates the center point of the target players.
    private Vector3 GetCenterPoint()
    {
        // If there is only one target.
        if (TargetPlayers.Length == 1)
        {
            // Return its position as the center point.
            return TargetPlayers[0].transform.position;
        }

        // Calculate the bounding box that encapsulates all targets.
        Bounds bounds = new Bounds(TargetPlayers[0].transform.position, Vector3.zero);
        for (int i = 0; i < TargetPlayers.Length; i++)
        {
            bounds.Encapsulate(TargetPlayers[i].transform.position);
        }

        // Return the center point of the bounding box.
        return bounds.center;
    }

    #endregion
}