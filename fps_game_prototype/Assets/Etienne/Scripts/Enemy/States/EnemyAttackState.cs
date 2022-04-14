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
        manager.Enemy.Animator.SetInteger("State", 1);
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
        manager.Enemy.Animator.SetInteger("State",3);
        while(!manager.Enemy.Animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
        {
            yield return null;
        }
        manager.Enemy.Weapon.Fire();
        yield return new WaitForSeconds(1f);
        manager.Enemy.Animator.SetInteger("State", 1);
        yield return new WaitForSeconds(0.5f);
        //yield return new WaitForSeconds(manager.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length);
        _attackAttempts--;
        if (_attackAttempts > 0)
        {
            _canTryAttacking = true;
        }
    }
}