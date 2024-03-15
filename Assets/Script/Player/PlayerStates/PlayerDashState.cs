using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isBusy = true;
        timerState = player.dashDuration;
        player.skillManager.createClone.CreateClone(player.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (timerState < 0)
            playerStateMachine.ChangeState(player.idleState);

        if (player.WallDetected())
            playerStateMachine.ChangeState(player.wallSlideState);
    }
}
