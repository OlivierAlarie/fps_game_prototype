using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage;
    public GameObject Projectile;
    public float ProjectileForce;
    public Transform ProjectileSpawnPoint;
    public Transform EnemySource;
    [SerializeField] private VisualEffect _vfx;
    [SerializeField] private float _fireDelay;
    [SerializeField] private float _meleeDelay;

    public void Fire()
    {
        if(_vfx != null)
        {
            _vfx.Play();
        }
        if(Projectile != null)
        {
            GameObject bulletFromBarrel = Instantiate(Projectile, ProjectileSpawnPoint.position, Quaternion.identity);
            bulletFromBarrel.transform.forward = EnemySource.forward;
            bulletFromBarrel.GetComponent<Rigidbody>().AddForce(EnemySource.forward * ProjectileForce, ForceMode.Impulse);
            Destroy(bulletFromBarrel, 2.5f);
        }

    }
}
