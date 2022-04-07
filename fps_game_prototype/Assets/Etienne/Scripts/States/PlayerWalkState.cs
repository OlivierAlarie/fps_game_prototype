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

        if (manager.Player.CommandManager.FirePressed)
        {
            manager.Player.WeaponManager.FireWeapon();
            manager.Player.CommandManager.FirePressed = false;
        }
    }

}
