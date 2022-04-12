using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int Health;

    public NavMeshAgent Agent;
    public GameObject Target;
    public float IdealDistance;
    public float ClosestDistance;
    public int NumberOfAttackAttempts;

    public Animator Animator;
    public CharacterController CharacterController;
    public EnemyStateManager StateManager;
    public EnemyWeapon Weapon;


    private void Start()
    {
        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        Agent = GetComponent<NavMeshAgent>();
        StateManager = new EnemyStateManager(this);

        Weapon = GetComponentInChildren<EnemyWeapon>(true);
    }

    private void Update()
    {
        StateManager.Update();
    }
}
