using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager
{
    //0 - Melee, 1 - Pistol, 2 - Automatic Rile, 3 - Sniper, 4 - Grenade ?
    private PlayerWeapon[] _weapons = new PlayerWeapon[5];
    public PlayerWeapon CurrentWeapon;


    public void FireWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Fire();
    }

    public void FireMeleeWeapon()
    {
        if(_weapons[0] == null || (CurrentWeapon != null && CurrentWeapon.isScoped))
        {
            return;
        }

        ((MeleeWeapon)_weapons[0]).Fire();
    }

    public void SwitchWeapon(int weaponselect)
    {
        //If weapon doesn't exist, hasn't been picked up yet or is in scope view
        if (_weapons[weaponselect] == null || !_weapons[weaponselect].isActiveAndEnabled || CurrentWeapon.isScoped)
        {
            return;
        }

        if(CurrentWeapon != null)
        {
            CurrentWeapon.Holster();
        }
        CurrentWeapon = _weapons[weaponselect];
        CurrentWeapon.Draw();
    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        if(_weapons[weapon.WeaponType] == null)
        {
            _weapons[weapon.WeaponType] = weapon;
        }
        else if (!_weapons[weapon.WeaponType].isActiveAndEnabled)
        {
            _weapons[weapon.WeaponType].gameObject.SetActive(true);
            _weapons[weapon.WeaponType].AddAmmo(weapon.AmmoCount);
            if (CurrentWeapon == null)
            {
                CurrentWeapon = _weapons[weapon.WeaponType];
                CurrentWeapon.Draw();
            }
        }
        else
        {
            _weapons[weapon.WeaponType].AddAmmo(weapon.AmmoCount);
        }
    }

    public void PlayAimAnimation()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Aim();
    }
}
