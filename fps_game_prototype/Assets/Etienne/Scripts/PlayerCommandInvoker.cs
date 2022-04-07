using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCommandInvoker
{
    public Player Player;
    public InputActions Inputs;

    // Start is called before the first frame update
    public PlayerCommandInvoker(Player player)
    {
        Player = player;
        Inputs = new InputActions();

        // PLAYER ACTIONS
        Inputs.Player.Move.started += OnMovement;
        Inputs.Player.Move.performed += OnMovement;
        Inputs.Player.Move.canceled += OnMovement;
        Inputs.Player.Fire.performed += OnFire;
        Inputs.Player.Fire.canceled += OnFire;
        Inputs.Player.Look.started += OnLook;
        Inputs.Player.Look.performed += OnLook;
        Inputs.Player.Look.canceled += OnLook;
        Inputs.Player.Jump.started += OnJump;
        Inputs.Player.Jump.canceled += OnJump;
        Inputs.Player.Interact.started += OnInteract;
        Inputs.Player.Interact.canceled += OnInteract;
    }
    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        Player._moveDirection.x = movementInput.x;
        Player._moveDirection.z = movementInput.y;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Player._cameraMoveInput = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        //_isFiring = context.control.IsPressed();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        //_isJumpPressed = context.control.IsPressed();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        //_isInteracting = context.ReadValueAsButton();
    }
}
