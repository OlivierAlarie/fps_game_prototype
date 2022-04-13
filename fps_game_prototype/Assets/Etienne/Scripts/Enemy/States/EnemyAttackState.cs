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

        Quaternion lookRotation = Quaternion.LookRotation(manager.Enemy.Target.transform.position - manager.Enemy.transform.position, Vector3.up);
        manager.Enemy.transform.rotation = Quaternion.Slerp(manager.Enemy.transform.rotation, lookRotation, Time.deltaTime * 25f);

        Ray r = new Ray(manager.Enemy.transform.position, manager.Enemy.transform.forward);
        LayerMask lm = LayerMask.GetMask("Player");
        if (_canTryAttacking && Physics.Raycast(r,float.PositiveInfinity,lm))
        {
            manager.Enemy.StartCoroutine(TryAttack(manager));
        }

        UnityEngine.AI.NavMeshHit navmeshhit;
        if (_attackAttempts <= 0 || manager.Enemy.Agent.Raycast(manager.Enemy.Target.transform.position, out navmeshhit))
        {
            manager.Enemy.StopAllCoroutines();
            manager.SwitchState(manager.ReloadState);
        }

        if(manager.Enemy.Target == null)
        {
            manager.Enemy.StopAllCoroutines();
            manager.SwitchState(manager.IdleState);
        }
    }

    IEnumerator TryAttack(EnemyStateManager manager)
    {
        _canTryAttacking = false;
        if (Vector3.Distance(manager.Enemy.Target.transform.position, manager.Enemy.transform.position) > manager.Enemy.ClosestDistance)
        {
            yield return new WaitForSeconds(manager.Enemy.Weapon.Fire());
        }
        else
        {
            yield return new WaitForSeconds(manager.Enemy.Weapon.Melee());
        }
        _attackAttempts--;
        if (_attackAttempts > 0)
        {
            _canTryAttacking = true;
        }
    }
}