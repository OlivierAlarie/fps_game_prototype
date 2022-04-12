using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCommandManager
{
    private InputActions _inputs;

    public Vector2 Direction;
    public Vector2 Look;

    public bool JumpPressed = false;
    public bool FirePressed = false;
    public bool MeleePressed = false;
    public bool WeaponSelectionPressed = false;
    public int WeaponSelection = -1;
    public bool AimPressed = false;


    // Start is called before the first frame update
    public PlayerCommandManager()
    {
        _inputs = new InputActions();

        // PLAYER ACTIONS
        _inputs.Player.Move.started += OnMovement;
        _inputs.Player.Move.performed += OnMovement;
        _inputs.Player.Move.canceled += OnMovement;
        _inputs.Player.Fire.performed += OnFire;
        _inputs.Player.Fire.canceled += OnFire;
        _inputs.Player.Look.started += OnLook;
        _inputs.Player.Look.performed += OnLook;
        _inputs.Player.Look.canceled += OnLook;
        _inputs.Player.Jump.started += OnJump;
        _inputs.Player.Jump.canceled += OnJump;
        _inputs.Player.Interact.started += OnInteract;
        _inputs.Player.Interact.canceled += OnInteract;
        _inputs.Player.Aim.performed += OnAim;
        _inputs.Player.Aim.canceled += OnAim;


        //WeaponSelect
        _inputs.Player.Weapon1.started += OnWeaponSelect;
        _inputs.Player.Weapon1.canceled += OnWeaponSelect;
        _inputs.Player.Weapon2.started += OnWeaponSelect;
        _inputs.Player.Weapon2.canceled += OnWeaponSelect;
        _inputs.Player.Weapon3.started += OnWeaponSelect;
        _inputs.Player.Weapon3.canceled += OnWeaponSelect;

        _inputs.Player.Melee.performed += OnMelee;
        _inputs.Player.Melee.canceled += OnMelee;

        _inputs.Enable();
    }
    private void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        Direction.x = movementInput.x;
        Direction.y = movementInput.y;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        FirePressed = context.control.IsPressed();
    }

    private void OnMelee(InputAction.CallbackContext context)
    {
        MeleePressed = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        JumpPressed = context.control.IsPressed();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        //_isInteracting = context.ReadValueAsButton();
    }

    private void OnWeaponSelect(InputAction.CallbackContext context)
    {
        WeaponSelectionPressed = context.ReadValueAsButton();
        if(context.action.name == "Weapon1")
        {
            WeaponSelection = 1;
        }
        else if(context.action.name == "Weapon2")
        {
            WeaponSelection = 2;
        }
        else if(context.action.name == "Weapon3")
        {
            WeaponSelection = 3;
        }
    }

    private void OnAim(InputAction.CallbackContext context)
    {
        AimPressed = context.control.IsPressed();
    }
}
