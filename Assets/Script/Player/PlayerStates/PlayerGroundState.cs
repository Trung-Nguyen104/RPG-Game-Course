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
        
        if (Inputs.Instance.GetInputDown(InputAction.PrimaryAttack))
            playerStateMachine.ChangeState(player.attackState);

        if (!player.GroundDetected())
            playerStateMachine.ChangeState(player.airState);

        if (Inputs.Instance.GetInputDown(InputAction.Jump) && player.GroundDetected() && !player.isBusy)
            playerStateMachine.ChangeState(player.jumpState);

        if(Inputs.Instance.GetInputDown(InputAction.ThorwSword) && CanCreateSword())
            playerStateMachine.ChangeState(player.aimState);
    }

    private bool CanCreateSword()
    {
        if (player.canCreateNewSword)
        {
            return true;
        }
        else
        {
            player.skillManager.throwSword.swordController.SwordReturnToPlayer();
            return false;
        }
    }
}
