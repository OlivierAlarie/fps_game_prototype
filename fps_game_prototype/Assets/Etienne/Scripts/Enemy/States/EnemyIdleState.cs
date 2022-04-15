using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
        manager.Enemy.Animator.SetInteger("State", 0);
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        if (manager.Enemy.Health <= 0)
        {
            manager.SwitchState(manager.DeadState);
        }

        if (manager.Enemy.Target == null)
        {
            return;
        }

        if (manager.Enemy.Type == Enemy.EnemyType.Aggressive)
        {
            manager.SwitchState(manager.FollowState);
        }

        if (manager.Enemy.Type == Enemy.EnemyType.Static)
        {
            manager.SwitchState(manager.AttackState);
        }
    }
}