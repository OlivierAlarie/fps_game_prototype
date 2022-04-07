using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;
    public PlayerCommandManager CommandManager;
    public PlayerStateManager StateManager;
    public PlayerWeaponManager WeaponManager;
    //public PlayerEventManager EventManager; ? for Collisions and Game Events

    // MOVEMENT VARIABLES
    public Vector3 Motion;
    public float WalkSpeed = 3f;

    // JUMPING VARIABLES
    public float JumpHeight = 5f;

    // GRAVITY VARIABLES
    public float GroundedGravity = -2f;
    public float Gravity = -9.8f;

    // CAMERA VARIABLES
    [SerializeField]
    private Vector2 _cameraSensitivity = new Vector2(360f, 360f);
    [SerializeField]
    private Vector2 _cameraYBounds = new Vector2(-45f, 45f);
    [SerializeField]
    private Transform _playerCamera;

    private float TargetRotationH = 0f;
    private float TargetRotationV = 0f;


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        CommandManager = new PlayerCommandManager();
        WeaponManager = new PlayerWeaponManager();
        StateManager = new PlayerStateManager(this);

        WeaponManager.AddWeapon(GetComponentInChildren<PlayerWeapon>(true));
        WeaponManager.SwitchWeapon(2);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        StateManager.Update();
        CharacterController.Move(transform.TransformDirection(Motion) * Time.deltaTime);
    }

    void LateUpdate()
    {

        TargetRotationH += CommandManager.Look.x * Time.deltaTime * _cameraSensitivity.x;
        TargetRotationV += CommandManager.Look.y * Time.deltaTime * _cameraSensitivity.y;

        TargetRotationV = Mathf.Clamp(TargetRotationV, _cameraYBounds.x, _cameraYBounds.y);

        transform.localRotation = Quaternion.AngleAxis(TargetRotationH, gameObject.transform.up);
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(-TargetRotationV, Vector3.right);
    }
}
