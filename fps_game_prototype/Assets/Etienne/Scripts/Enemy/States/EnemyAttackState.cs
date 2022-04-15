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

        if(manager.Enemy.Type == Enemy.EnemyType.Aggressive)
        {
            if (_canTryAttacking && Physics.Raycast(r, float.PositiveInfinity, lm))
            {
                manager.Enemy.StartCoroutine(TryAttack(manager));
            }

            UnityEngine.AI.NavMeshHit navmeshhit;
            if (_attackAttempts <= 0 || manager.Enemy.Agent.Raycast(manager.Enemy.Target.transform.position, out navmeshhit))
            {
                manager.Enemy.StopAllCoroutines();
                manager.SwitchState(manager.RepositionState);
            }

            if (manager.Enemy.Target == null)
            {
                manager.Enemy.StopAllCoroutines();
                manager.SwitchState(manager.IdleState);
            }
        }
        else if(manager.Enemy.Type == Enemy.EnemyType.Static)
        {
            //Attacks indefinitely;
            _attackAttempts = manager.Enemy.NumberOfAttackAttempts;
            //If the player is in the range
            RaycastHit hit;
            if (_canTryAttacking && Physics.Raycast(r, out hit, manager.Enemy.IdealDistance) && hit.collider.CompareTag("Player"))
            {
                manager.Enemy.StartCoroutine(TryAttack(manager));
            }
        }


        if (manager.Enemy.Health <= 0)
        {
            manager.Enemy.StopAllCoroutines();
            manager.SwitchState(manager.DeadState);
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
        if(manager.Enemy.Weapon != null)
        {
            manager.Enemy.Weapon.Fire();
            manager.Enemy.AudioManager.PlayClip("Attack");
        }
        yield return new WaitForSeconds(manager.Enemy.Animator.GetCurrentAnimatorStateInfo(0).length);
        manager.Enemy.Animator.SetInteger("State", 1);
        yield return new WaitForSeconds(0.5f);
        _attackAttempts--;
        if (_attackAttempts > 0)
        {
            _canTryAttacking = true;
        }
    }
}