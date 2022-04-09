using UnityEngine;
public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager manager)
    {
        manager.Player.Motion = Vector3.zero;
        manager.Player.Motion.y = manager.Player.GroundedGravity;
    }

    public override void UpdateState(PlayerStateManager manager)
    {
        //If the player is trying to move
        if(manager.Player.CommandManager.Direction != Vector2.zero)
        {
            manager.SwitchState(manager.WalkState);
        }

        //if the player is trying to Jump
        if (manager.Player.CommandManager.JumpPressed)
        {
            manager.SwitchState(manager.JumpState);
        }

        if (manager.Player.CommandManager.FirePressed)
        {
            manager.Player.WeaponManager.FireWeapon();
            manager.Player.CommandManager.FirePressed = false;
        }

        if (manager.Player.CommandManager.AimPressed)
        {
            manager.Player.WeaponManager.PlayAimAnimation();
            manager.Player.CommandManager.AimPressed = false;
        }

        if (manager.Player.CommandManager.WeaponSelectionPressed)
        {
            manager.Player.CommandManager.WeaponSelectionPressed = false;
            manager.Player.WeaponManager.SwitchWeapon(manager.Player.CommandManager.WeaponSelection);
        }
    }

}
