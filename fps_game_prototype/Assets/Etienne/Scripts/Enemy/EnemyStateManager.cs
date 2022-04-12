using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager
{
    public Enemy Enemy;

    private EnemyBaseState _currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyFollowState FollowState = new EnemyFollowState();
    public EnemyAttackState AttackState = new EnemyAttackState();

    public EnemyStateManager(Enemy enemy)
    {
        Enemy = enemy;
        SwitchState(IdleState);
    }

    public void Update()
    {
        Debug.Log(_currentState);
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState enemyBaseState)
    {
        _currentState = enemyBaseState;

        _currentState.EnterState(this);
    }
}
