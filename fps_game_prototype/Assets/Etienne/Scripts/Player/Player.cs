using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator Animator;
    public CharacterController CharacterController;
    public PlayerCommandManager CommandManager;
    public PlayerStateManager StateManager;
    public PlayerWeaponManager WeaponManager;
    public AudioClip slurpSoundJuice;
    public AudioClip slurpSoundMilk;

    public AudioSource audioSource;
    

    // HEALTH & STATUS VARIABLES
    public int Health = 100;

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

        PlayerWeapon[] weapons = GetComponentsInChildren<PlayerWeapon>(true);
        foreach (var weapon in weapons)
        {
            WeaponManager.AddWeapon(weapon);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        WeaponManager.SwitchWeapon(1);
    }

    private void Update()
    {
        StateManager.Update();
        CharacterController.Move(transform.TransformDirection(Motion) * Time.deltaTime);
    }

    private void LateUpdate()
    {

        TargetRotationH += CommandManager.Look.x * Time.deltaTime;
        TargetRotationV += CommandManager.Look.y * Time.deltaTime;

        TargetRotationV = Mathf.Clamp(TargetRotationV, _cameraYBounds.x, _cameraYBounds.y);

        transform.localRotation = Quaternion.AngleAxis(TargetRotationH, gameObject.transform.up);
        _playerCamera.transform.localRotation = Quaternion.AngleAxis(-TargetRotationV, Vector3.right);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponPickup"))
        {
            WeaponManager.AddWeapon(other.GetComponent<PlayerWeapon>());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EnemyWeapon"))
        {
            Health -= other.GetComponent<EnemyWeapon>().Damage;
        }
        if (other.CompareTag("JuiceBox"))
        {
            Health = Mathf.Min(Health + 20, 100);
            audioSource.clip = slurpSoundJuice;
            audioSource.Play();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("MilkCarton"))
        {
            Health = Mathf.Min(Health + 10, 100);
            audioSource.clip = slurpSoundMilk;
            audioSource.Play();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        NerfBullet nerfbullet = collision.gameObject.GetComponent<NerfBullet>();
        if (nerfbullet != null)
        {
            if(nerfbullet.Source == "Enemy" && nerfbullet.CanDamage)
            {
                Health -= nerfbullet.Damage;
                nerfbullet.CanDamage = false;
            }
        }
    }
}
