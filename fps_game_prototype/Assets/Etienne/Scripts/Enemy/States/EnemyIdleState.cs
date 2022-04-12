public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
    }

    public override void UpdateState(EnemyStateManager manager)
    {

        if(manager.Enemy.Target != null)
        {
            manager.SwitchState(manager.FollowState);
        }
    }
}