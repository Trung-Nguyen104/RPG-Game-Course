using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.velocity.y);
        player.isBusy = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (xInput != 0 && !player.isBusy)
            playerStateMachine.ChangeState(player.moveState);
    }
}
