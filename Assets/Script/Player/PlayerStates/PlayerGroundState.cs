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
            playerStateMachine.ChangeState(player.AttackState);

        if (!player.GroundDetected())
            playerStateMachine.ChangeState(player.AirState);

        if (Inputs.Instance.GetInputDown(InputAction.Jump) && player.GroundDetected() && !player.IsBusy)
            playerStateMachine.ChangeState(player.JumpState);

        if(Inputs.Instance.GetInputDown(InputAction.ThorwSword) && CanCreateSword() && Skill_Manager.Instance.ThrowSword.skill_Unlocked)
            playerStateMachine.ChangeState(player.AimState);
    }

    private bool CanCreateSword()
    {
        if (player.CanThrowSword)
        {
            return true;
        }
        else
        {
            player.SkillManager.ThrowSword.swordController.SwordReturnToPlayer();
            return false;
        }
    }
}
