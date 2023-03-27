using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool IsPC;

    private Vector2 Movement;
    private Vector2 MouseLook;
    private Vector2 JoystickLook;
    private Vector3 RotationTarget;
    public void OnMove(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }
    public void OnMouseLook(InputAction.CallbackContext context)
    {
        MouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        JoystickLook = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (IsPC)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(MouseLook);

            if(Physics.Raycast(ray, out hit))
            {
                RotationTarget = hit.point;
            }
            RotatePlayerWithAim();
        }
        else
        {
            if (JoystickLook.x == 0 && JoystickLook.y == 0)
            {
                MovePlayer();
            }
            else
            {
                RotatePlayerWithAim();
            }
        }
        
    }
    public void MovePlayer()
    {
        Vector3 move = new Vector3(Movement.x, 0f, Movement.y);

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15f);
        }
        transform.Translate(move * Speed * Time.deltaTime, Space.World);
    }
    public void RotatePlayerWithAim()
    {
        if (IsPC)
        {
            var lookPos = RotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(RotationTarget.x, 0f, RotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(JoystickLook.x, 0f, JoystickLook.y);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }
        Vector3 movement = new Vector3(Movement.x, 0f, Movement.y);

        transform.Translate(movement * Speed * Time.deltaTime, Space.World);

    }
}
