using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;
    public PlayerCommandInvoker PlayerCommandInvoker;
    public PlayerStateManager PlayerStateManager;

    // MOVEMENT VARIABLES

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

    public Vector2 _cameraMoveInput;
    [SerializeField]
    Vector2 _cameraSensitivity = new Vector2(360f, 360f);
    [SerializeField]
    Vector2 _cameraYBounds = new Vector2(-45f, 45f);
    [SerializeField]
    Transform _playerCamera;

    float TargetRotationH = 0f;
    float TargetRotationV = 0f;


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        PlayerCommandInvoker = new PlayerCommandInvoker(this);
        PlayerStateManager = new PlayerStateManager(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        PlayerCommandInvoker.Inputs.Enable();
    }
    private void OnDisable()
    {
        PlayerCommandInvoker.Inputs.Disable();
    }

    void HandleGravity()
    {
        if (CharacterController.isGrounded)
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
        if (!_isJumping && CharacterController.isGrounded && _isJumpPressed)
        {
            _isJumping = true;
            _moveDirection.y += _jumpForce;
        }
        else if (!_isJumpPressed && _isJumping && CharacterController.isGrounded)
        {
            _isJumping = false;
        }
    }

    void Update()
    {
        CharacterController.Move(transform.TransformDirection(_moveDirection) * _moveSpeed * Time.deltaTime);
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
