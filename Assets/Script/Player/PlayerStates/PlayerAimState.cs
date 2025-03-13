using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skillManager.throwSword.SetActiveTrajectoryLine(true);
        player.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, rb.velocity.y);
        if (Inputs.Instance.GetInputUp(InputAction.ThorwSword))
        {
            playerStateMachine.ChangeState(player.idleState);
        }
    }
}
