using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = false;
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.SetDestination(manager.Enemy.Target.transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(manager.Enemy.Target.transform.position - manager.Enemy.transform.position,Vector3.up);
        manager.Enemy.transform.rotation = Quaternion.Slerp(manager.Enemy.transform.rotation, lookRotation, Time.deltaTime * 25f);

        if (manager.Enemy.Target == null)
        {
            manager.SwitchState(manager.IdleState);
        }

        if (Vector3.Distance(manager.Enemy.Target.transform.position, manager.Enemy.transform.position) <= manager.Enemy.IdealDistance)
        {
            manager.SwitchState(manager.AttackState);
        }
    }
}