using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager
{

    private PlayerWeapon[] _weapons = new PlayerWeapon[3];
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
        if(_weapons[weaponselect] != null)
        {
            //CurrentWeapon.Deselect()
            CurrentWeapon = _weapons[weaponselect];
            //CurrentWeapon.Select()
        }
    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        if(_weapons[weapon.WeaponType] == null)
        {
            _weapons[weapon.WeaponType] = weapon;
        }
        else
        {
            _weapons[weapon.WeaponType].AddAmmo(weapon.AmmoCount);
        }
    }
}
