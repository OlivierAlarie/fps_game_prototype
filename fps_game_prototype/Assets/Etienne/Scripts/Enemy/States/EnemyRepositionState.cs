using UnityEngine;

public class EnemyRepositionState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = false;
        int flip;
        if(Random.value < 0.5f)
        {
            flip = 1;
        }
        else
        {
            flip = -1;
        }
        manager.Enemy.Agent.SetDestination(manager.Enemy.transform.right*Random.Range(3,5)*flip + manager.Enemy.transform.position);

        manager.Enemy.Animator.SetInteger("State", 2);
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        if(!manager.Enemy.Agent.pathPending && manager.Enemy.Agent.remainingDistance < 0.5f)
        {
            manager.SwitchState(manager.FollowState);
        }

        if (manager.Enemy.Health <= 0)
        {
            manager.SwitchState(manager.DeadState);
        }
    }
}