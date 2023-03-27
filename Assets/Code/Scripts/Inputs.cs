using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    [SerializeField] private CharacterController Controller = null;
    [SerializeField] private MovementInputProcessor InputProcessor = null;
    [SerializeField] private ForceReceiver Force = null;
    // Start is called before the first frame update
    public Playerinputs playerControls;
    private InputAction move;
    private InputAction jump;

    private void Awake()
    {
        playerControls = new Playerinputs();
    }
    private void OnEnable()
    {
        move = playerControls.Keyboard.Move;
        move.Enable();
        move.performed += Move;

        jump = playerControls.Keyboard.Jump;
        jump.Enable();
        jump.performed += Jump;
    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        //Vector2 movetest = move.ReadValue<Vector2>();
        InputProcessor.SetMovementInput(move.ReadValue<Vector2>());
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Force.AddForce(new Vector3(0, 1.5f, 0));
    }
}
