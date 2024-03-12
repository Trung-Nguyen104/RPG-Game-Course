using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSildeState : PlayerState
{
    public PlayerWallSildeState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, rb.velocity.y * .7f);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            playerStateMachine.ChangeState(player.wallJumpState);

        if (player.GroundDetected() || !player.WallDetected())
            playerStateMachine.ChangeState(player.idleState);
    }
}
