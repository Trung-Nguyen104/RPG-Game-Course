using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamgeState : PlayerState
{
    public PlayerTakeDamgeState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.wasDead)
        {
            playerStateMachine.ChangeState(player.DeadState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        playerStateMachine.ChangeState(player.IdleState);
    }
}
