using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : PlayerWeapon
{

    public new void Fire()
    {
        _animator.Play("Strike");
    }
}
