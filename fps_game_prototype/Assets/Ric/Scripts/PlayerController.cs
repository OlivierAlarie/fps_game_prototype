using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions _inputActions;
    CharacterController _myCharacterController;

    // MOVEMENT VARIABLES

    Vector2 _movementInput;
    public Vector3 _moveDirection;
    [SerializeField]
    float _moveSpeed = 3f;

    // JUMPING VARIABLES
    bool _isJumpPressed = false;
    [SerializeField]
    float _jumpForce = 5f;
    bool _isJumping = false;

    // GRAVITY VARIABLES

    [SerializeField]
    float _groundedGravity = -.05f;
    [SerializeField]
    float _gravity = -9.8f;

    // CAMERA VARIABLES

    Vector2 _cameraMoveInput;
    Vector2 _cameraMoveDirection;
    [SerializeField]
    Vector2 _cameraSensitivity = new Vector2(360f, 360f);
    [SerializeField]
    Vector2 _cameraYBounds = new Vector2(-45f, 45f);
    [SerializeField]
    Transform _playerCamera;

    float TargetRotationH = 0f;
    float TargetRotationV = 0f;

    // OTHER VARIABLES
    bool _isFiring;
    bool _isInteracting;

    void Awake()
    {
        _inputActions = new InputActions();
        _myCharacterController = GetComponent<CharacterController>();


        // PLAYER ACTIONS
        _inputActions.Player.Move.started += OnMovement;
        _inputActions.Player.Move.performed += OnMovement;
        _inputActions.Player.Move.canceled += OnMovement;
        _inputActions.Player.Fire.performed += OnFire;
        _inputActions.Player.Fire.canceled += OnFire;
        //_playerAction.Player.Look.started += OnLook;
        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Look.canceled += OnLook;
        _inputActions.Player.Jump.started += OnJump;
        _inputActions.Player.Jump.canceled += OnJump;
        _inputActions.Player.Interact.started += OnInteract;
        _inputActions.Player.Interact.canceled += OnInteract;
    }

    void OnMovement(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _moveDirection.x = _movementInput.x;
        _moveDirection.z = _movementInput.y;
        //_moveDirection = transform.TransformDirection(_moveDirection);
    }

    void OnLook(InputAction.CallbackContext context)
    {
        _cameraMoveInput = context.ReadValue<Vector2>();
    }

    void OnFire(InputAction.CallbackContext context)
    {
        _isFiring = context.control.IsPressed();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        _isJumpPressed = context.control.IsPressed();        
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        _isInteracting = context.ReadValueAsButton();
        Debug.Log("You used the Interact Button");
    }

    void HandleGravity()
    {
        if (_myCharacterController.isGrounded)
        {
            _moveDirection.y = _groundedGravity;
        }
        else
        {
            _moveDirection.y += _gravity * Time.deltaTime;
        }
    }



    void HandleJump()
    {
        if (!_isJumping && _myCharacterController.isGrounded && _isJumpPressed)
        {
            _isJumping = true;
            _moveDirection.y += _jumpForce;
        }
        else if (!_isJumpPressed && _isJumping && _myCharacterController.isGrounded)
        {
            _isJumping = false;
        }
    }

    void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    void Update()
    {
        _myCharacterController.Move(transform.TransformDirection(_moveDirection) * _moveSpeed * Time.deltaTime);
        if (_isFiring)
        {
            Debug.Log("We fired");
        }
        HandleGravity();
        HandleJump();
    }

    void LateUpdate()
    {
        TargetRotationH += _cameraMoveInput.x * Time.deltaTime * _cameraSensitivity.x;
        TargetRotationV += _cameraMoveInput.y * Time.deltaTime * _cameraSensitivity.y;

        TargetRotationV = Mathf.Clamp(TargetRotationV, _cameraYBounds.x, _cameraYBounds.y);

        transform.localRotation = Quaternion.AngleAxis(TargetRotationH, gameObject.transform.up);
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(-TargetRotationV, Vector3.right);
    }
}
