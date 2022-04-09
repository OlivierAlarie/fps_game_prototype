using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int MaxAmmo;
    public int AmmoCount;
    public int WeaponType;
    public float Range;
    public GameObject projectile;
    public ParticleSystem particles;

    [SerializeField] private Animator _animator;

    public void Fire()
    {
        Debug.Log("Trying to Fire !");
        if (AmmoCount == 0) 
        {
            return;
        }

        AmmoCount--;
        //Fire Animation
        //InstantiateProjectile
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Range))
        {
            GameObject go = Instantiate(projectile, hit.point,Quaternion.identity);
            go.GetComponent<Rigidbody>().AddExplosionForce(250f, hit.point, 10f);
            particles.Play();
            Destroy(go, 1f);
        }

    }
    public void AddAmmo(int Ammo)
    {
        if (AmmoCount == MaxAmmo)
        {
            return;
        }

        AmmoCount += Ammo;
        AmmoCount = Mathf.Min(AmmoCount, MaxAmmo);
    }
    public void Reload()
    {
        
    }

    public void Select()
    {
        
    }

    public void Deselect()
    {

    }
}
