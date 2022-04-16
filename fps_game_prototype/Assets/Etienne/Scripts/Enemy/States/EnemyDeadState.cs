using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
        manager.Enemy.Animator.SetInteger("State", 4);
        manager.Enemy.GetComponent<Collider>().enabled = false;
        if (manager.Enemy.TheGunToDrop != null)
        {
            GameObject.Instantiate(manager.Enemy.TheGunToDrop, manager.Enemy.transform.position, manager.Enemy.transform.rotation);
        }
    }

    public override void UpdateState(EnemyStateManager manager)
    {

    }
}