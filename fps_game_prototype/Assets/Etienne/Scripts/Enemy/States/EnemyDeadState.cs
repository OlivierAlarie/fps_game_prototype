using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
        manager.Enemy.Animator.SetInteger("State", 4);
        manager.Enemy.GetComponent<Collider>().enabled = false;
    }

    public override void UpdateState(EnemyStateManager manager)
    {

    }
}