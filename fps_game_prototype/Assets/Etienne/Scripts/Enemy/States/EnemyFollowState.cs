using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = false;
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        Vector3 targetPos = manager.Enemy.Target.transform.position;
        Vector3 enemyPos = manager.Enemy.transform.position;
        manager.Enemy.Agent.SetDestination(targetPos);
        Quaternion lookRotation = Quaternion.LookRotation(targetPos - enemyPos, Vector3.up);
        manager.Enemy.transform.rotation = Quaternion.Slerp(manager.Enemy.transform.rotation, lookRotation, Time.deltaTime * 25f);

        if (manager.Enemy.Target == null)
        {
            manager.SwitchState(manager.IdleState);
        }

        UnityEngine.AI.NavMeshHit hit;
        if (Vector3.Distance(targetPos, enemyPos) <= manager.Enemy.IdealDistance && !manager.Enemy.Agent.Raycast(targetPos,out hit))
        {
            manager.SwitchState(manager.AttackState);
        }
    }
}