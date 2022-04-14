using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = false;
        manager.Enemy.Animator.SetInteger("State", 2);
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        Vector3 targetPos = manager.Enemy.Target.transform.position;
        Vector3 enemyPos = manager.Enemy.transform.position;
        manager.Enemy.Agent.SetDestination(targetPos);

        if (manager.Enemy.Target == null)
        {
            manager.SwitchState(manager.IdleState);
        }

        UnityEngine.AI.NavMeshHit navmeshhit;
        if (Vector3.Distance(targetPos, enemyPos) <= manager.Enemy.IdealDistance && !manager.Enemy.Agent.Raycast(targetPos, out navmeshhit))
        {
            manager.SwitchState(manager.AttackState);
        }
    }
}