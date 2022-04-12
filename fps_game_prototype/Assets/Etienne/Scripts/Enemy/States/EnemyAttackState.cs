using System.Collections;
using UnityEngine;
public class EnemyAttackState : EnemyBaseState
{
    private int _attackAttempts;
    private bool _canTryAttacking;
    public override void EnterState(EnemyStateManager manager)
    {
        manager.Enemy.Agent.isStopped = true;
        _attackAttempts = manager.Enemy.NumberOfAttackAttempts;
        _canTryAttacking = true;
    }

    public override void UpdateState(EnemyStateManager manager)
    {

        if (_canTryAttacking)
        {
            manager.Enemy.StartCoroutine(TryAttack(manager));
        }

        Quaternion lookRotation = Quaternion.LookRotation(manager.Enemy.Target.transform.position - manager.Enemy.transform.position, Vector3.up);
        manager.Enemy.transform.rotation = Quaternion.Slerp(manager.Enemy.transform.rotation, lookRotation, Time.deltaTime * 25f);

        UnityEngine.AI.NavMeshHit hit;
        if (_attackAttempts <= 0 || manager.Enemy.Agent.Raycast(manager.Enemy.Target.transform.position, out hit))
        {
            manager.SwitchState(manager.FollowState);
        }

        if(manager.Enemy.Target == null)
        {
            manager.SwitchState(manager.IdleState);
        }
    }

    IEnumerator TryAttack(EnemyStateManager manager)
    {
        _attackAttempts--;
        _canTryAttacking = false;
        if (Vector3.Distance(manager.Enemy.Target.transform.position, manager.Enemy.transform.position) > manager.Enemy.ClosestDistance)
        {
            yield return new WaitForSeconds(manager.Enemy.Weapon.Fire());
        }
        else
        {
            yield return new WaitForSeconds(manager.Enemy.Weapon.Melee());
        }

        if(_attackAttempts > 0)
        {
            _canTryAttacking = true;
        }
    }
}