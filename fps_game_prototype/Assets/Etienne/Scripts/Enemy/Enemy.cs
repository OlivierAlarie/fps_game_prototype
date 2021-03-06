using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Health;

    public NavMeshAgent Agent;
    public Player Target;
    public float IdealDistance;
    public float ClosestDistance;
    public int NumberOfAttackAttempts;

    public Animator Animator;
    public CharacterController CharacterController;
    public EnemyStateManager StateManager;
    public EnemyWeapon Weapon;
    public EnemyAudioManager AudioManager;
    public EnemyType Type = EnemyType.Static;
    public GameObject TheGunToDrop;

    public enum EnemyType
    {
        Static,
        Aggressive
    }

    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        StateManager = new EnemyStateManager(this);

        AudioManager = GetComponentInChildren<EnemyAudioManager>(true);
        Weapon = GetComponentInChildren<EnemyWeapon>(true);
    }

    private void Update()
    {
        StateManager.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeapon playerWeapon = other.GetComponent<PlayerWeapon>();
        if(playerWeapon != null && playerWeapon.WeaponType == 1)
        {
            Health -= playerWeapon.Damage;
            AudioManager.PlayClip("Hurt");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        NerfBullet nerfbullet = collision.gameObject.GetComponent<NerfBullet>();
        if (nerfbullet != null)
        {
            if (nerfbullet.Source == "Player" && nerfbullet.CanDamage)
            {
                Health -= nerfbullet.Damage;
                nerfbullet.CanDamage = false;
                AudioManager.PlayClip("Hurt");
            }
        }
    }
}
