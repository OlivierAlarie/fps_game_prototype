using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager
{

    private PlayerWeapon[] _weapons = new PlayerWeapon[4];
    public PlayerWeapon CurrentWeapon;


    public void FireWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Fire();
    }

    public void SwitchWeapon(int weaponselect)
    {
        if(_weapons[weaponselect] != null && _weapons[weaponselect].isActiveAndEnabled)
        {
            if(CurrentWeapon != null)
            {
                CurrentWeapon.Holster();
            }
            CurrentWeapon = _weapons[weaponselect];
            CurrentWeapon.Draw();
        }
    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        if(_weapons[weapon.WeaponType] == null)
        {
            _weapons[weapon.WeaponType] = weapon;
        }
        else if (!_weapons[weapon.WeaponType].isActiveAndEnabled)
        {
            Debug.Log("enabling");
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
