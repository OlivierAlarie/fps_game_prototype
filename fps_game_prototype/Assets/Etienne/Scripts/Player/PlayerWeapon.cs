using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerWeapon : MonoBehaviour
{
    public int MaxAmmo;
    public int AmmoCount;
    public int WeaponType;
    public float Range;
    public float FireRate;//Number of seconds between shots
    public GameObject projectile;
    public VisualEffect particles;
    public bool isScoped = false;
    private bool isReady = false;
    private bool canFire = true;
    public GameObject crossHairUI;
    public GameObject scopeUI;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFOV = 15f;
    private float defaultFOV;
    public bool isARayCaster;
    public Transform gunBarrel;
    Vector3 rayTargetPoint;
    public float bulletForce;
    public Vector3 upwardForce;
    public bool isABalloon = false;

    [SerializeField] protected Animator _animator;

    public void Fire()
    {
        if (AmmoCount == 0 || !isReady || !canFire)
        {
            return;
        }


        AmmoCount--;
        //Fire Animation
        if(_animator != null)
        {
            _animator.Play("Fire");
        }
        if (particles != null)
        {
            particles.Play();
        }

        
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit, Range))
        {
            rayTargetPoint = hit.point;
        }
        else
        {
            rayTargetPoint = ray.GetPoint(Range);
        }

        //InstantiateProjectile
        if (isARayCaster)
        {
            //Do Nothing
        }
        else if (isABalloon)
        {
            BalloonSpawner();
        }
        else if (!isABalloon && !isARayCaster )
        {
            BulletSpawner();
        }

        StartCoroutine(DelayBetWeenShots());
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

    public void Draw()
    {
        _animator.SetInteger("WeaponState", 1);
        StartCoroutine(DelayReady());
    }

    public void Holster()
    {
        _animator.SetInteger("WeaponState", 0);
        isReady = false;
    }

    public void Aim()
    {
        if(scopeUI == null)
        {
            return;
        }
        isScoped = !isScoped;
        _animator.SetBool("isScoped", isScoped);
        SwitchCrossHair();
    }

    public void SwitchCrossHair()
    {
        if(crossHairUI.activeSelf)
        {
            crossHairUI.SetActive(false);
            StartCoroutine(DelayScopeUI());
        }
        else
        {
           crossHairUI.SetActive(true);
           scopeUI.SetActive(false);
           weaponCamera.SetActive(true);
           mainCamera.fieldOfView = defaultFOV; // apply default FOV
        }
    }
    IEnumerator DelayScopeUI()
    {
        yield return new WaitForSeconds(0.15f);
        scopeUI.SetActive(true);
        weaponCamera.SetActive(false);
        defaultFOV = mainCamera.fieldOfView; // get default FOV
        mainCamera.fieldOfView = scopedFOV; // apply new FOV (Zoom)
    }

    IEnumerator DelayReady()
    {
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Holstered"))
        {
            yield return null;
        }
        isReady = true;
    }

    IEnumerator DelayBetWeenShots()
    {
        canFire = false;
        while (!_animator.GetCurrentAnimatorStateInfo(1).IsName("Fire"))
        {
            yield return null;
        }
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(1).length);
        canFire = true;
    }
    private void BulletSpawner()
    {
        Vector3 directionOfBullet = rayTargetPoint - gunBarrel.position;
        GameObject bulletFromBarrel = Instantiate(projectile,gunBarrel.position,Quaternion.identity);
        bulletFromBarrel.transform.forward = directionOfBullet.normalized;
        bulletFromBarrel.GetComponent<Rigidbody>().AddForce(directionOfBullet.normalized * bulletForce,ForceMode.Impulse);
        Destroy(bulletFromBarrel,2.5f);
    }
    public void BalloonSpawner()
    {
        Vector3 directionOfBullet = rayTargetPoint - gunBarrel.position;
        GameObject newballoon = Instantiate(projectile,gunBarrel.position,Quaternion.identity);
        newballoon.transform.forward = directionOfBullet.normalized;
        newballoon.GetComponent<Rigidbody>().AddForce((directionOfBullet.normalized + upwardForce ) * bulletForce,ForceMode.Impulse);
        newballoon.AddComponent<WaterBalloon>();
    }

}
