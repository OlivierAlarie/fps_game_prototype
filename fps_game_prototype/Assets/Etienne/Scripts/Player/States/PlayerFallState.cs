using UnityEngine;
public class PlayerFallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager manager)
    {
        manager.Player.CommandManager.JumpPressed = false;
    }

    public override void UpdateState(PlayerStateManager manager)
    {
        manager.Player.Motion.y += manager.Player.Gravity * Time.deltaTime;

        //Only Switch to other states if the character is grounded
        if(!manager.Player.CharacterController.isGrounded)
        { 
            return; 
        }

        //If there is movement input, switch to walk state, if not idle
        if(manager.Player.CommandManager.Direction != Vector2.zero)
        {
            manager.SwitchState(manager.WalkState);
        }
        else
        {
            manager.SwitchState(manager.IdleState);
        }
    }

}
