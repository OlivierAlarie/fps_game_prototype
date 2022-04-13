using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage;
    public GameObject Projectile;
    public float ProjectileForce;
    public Transform ProjectileSpawnPoint;
    private Animator _animator;
    [SerializeField] private float _fireDelay;
    [SerializeField] private float _meleeDelay;

    public float Fire()
    {

        //Play Fire Animation;
        //_animator.Play("Fire");
        GameObject bulletFromBarrel = Instantiate(Projectile, ProjectileSpawnPoint.position, Quaternion.identity);
        bulletFromBarrel.transform.forward = ProjectileSpawnPoint.forward;
        bulletFromBarrel.GetComponent<Rigidbody>().AddForce(ProjectileSpawnPoint.forward * ProjectileForce, ForceMode.Impulse);
        Destroy(bulletFromBarrel, 2.5f);

        return _fireDelay;
    }

    public float Melee()
    {
        //Player Melee Animation;
        return _meleeDelay;
    }
}
