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
    private bool isScoped = false;
    private bool isReady = false;
    public GameObject crossHairUI;
    public GameObject scopeUI;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFOV = 15f;
    private float defaultFOV;

    [SerializeField] private Animator _animator;

    public void Fire()
    {
        if (AmmoCount == 0)
        {
            return;
        }

        AmmoCount--;
        //Fire Animation
        if (particles != null)
        {
            particles.Play();
        }
        //InstantiateProjectile
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.forward, out hit, Range))
        {
            GameObject go = Instantiate(projectile, hit.point,Quaternion.identity);
            go.GetComponent<Rigidbody>().AddExplosionForce(250f, hit.point, 10f);
            Destroy(go, 2.5f);
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

    public void Draw()
    {
        _animator.SetInteger("WeaponState", 1);
        isReady = true;
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
}
