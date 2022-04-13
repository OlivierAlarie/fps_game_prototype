using UnityEngine;
public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager manager)
    {
        manager.Player.Motion.y = manager.Player.GroundedGravity;
    }

    public override void UpdateState(PlayerStateManager manager)
    {
        manager.Player.Motion.x = manager.Player.CommandManager.Direction.x * manager.Player.WalkSpeed;
        manager.Player.Motion.z = manager.Player.CommandManager.Direction.y * manager.Player.WalkSpeed;

        if(manager.Player.CommandManager.Direction == Vector2.zero)
        {
            manager.SwitchState(manager.IdleState);
        }

        //if the player is trying to Jump
        if (manager.Player.CommandManager.JumpPressed)
        {
            manager.SwitchState(manager.JumpState);
        }

        if (!manager.Player.CharacterController.isGrounded)
        {
            manager.SwitchState(manager.FallState);
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

        if (manager.Player.CommandManager.MeleePressed)
        {
            manager.Player.CommandManager.MeleePressed = false;
            manager.Player.WeaponManager.FireMeleeWeapon();
        }
    }

}
