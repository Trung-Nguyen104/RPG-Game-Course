using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
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
        
        if (Input.GetKeyDown(KeyCode.J))
            playerStateMachine.ChangeState(player.attackState);

        if (!player.GroundDetected())
            playerStateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.UpArrow) && player.GroundDetected() && !isBusy)
            playerStateMachine.ChangeState(player.jumpState);

        if(Input.GetKeyDown(KeyCode.H))
            playerStateMachine.ChangeState(player.aimState);
    }
}
