public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
        manager.Enemy.Animator.SetInteger("State", 0);
    }

    public override void UpdateState(EnemyStateManager manager)
    {

        if(manager.Enemy.Target != null)
        {
            manager.SwitchState(manager.FollowState);
        }
    }
}