using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSword : PlayerState
{
    public PlayerCatchSword(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timerState = .5f;
        isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, rb.velocity.y);
        if (timerState < 0)
            playerStateMachine.ChangeState(player.idleState);
    }
}
