using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (rb.velocity.y < 0)
            playerStateMachine.ChangeState(player.AirState);

        if(player.WallDetected())
            playerStateMachine.ChangeState(player.WallSlideState);

        if (Inputs.Instance.GetInputDown(InputAction.Ultimate) && Skill_Manager.Instance.Ultimate.skill_Unlocked)
            playerStateMachine.ChangeState(player.UltimateState);
    }
}
