using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySFX(26);
    }

    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.StopSFX(26);
    }

    public override void Update()
    {
        base.Update();

        if (xInput == 0)
            playerStateMachine.ChangeState(player.IdleState);

    }
}
