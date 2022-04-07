using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager
{
    public Player Player;

    private PlayerBaseState _currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerWalkState WalkState = new PlayerWalkState();
    public PlayerJumpState JumpState = new PlayerJumpState();

    public PlayerStateManager(Player player)
    {
        Player = player;
        SwitchState(IdleState);
    }

    public void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState playerBaseState)
    {
        _currentState = playerBaseState;

        _currentState.EnterState(this);
    }
}
