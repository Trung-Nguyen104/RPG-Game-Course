using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateState : PlayerState
{
    private float flyTime = .4f;
    private bool canUseUltimate;
    private float defaultGravity;
    public PlayerUltimateState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canUseUltimate = true;
        timerState = flyTime;
        defaultGravity = rb.gravityScale;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = defaultGravity;
    }

    public override void Update()
    {
        base.Update();
        player.isBusy = true;
        if(timerState > 0)
        {
            player.SetVelocity(0, 5f);
        }
        else if(timerState < 0)
        {
            player.SetVelocity(0, 0.01f);
            rb.gravityScale = 0;
            if(canUseUltimate)
            {
                Skill_Manager.Instance.Ultimate.CanUseSkill();
                canUseUltimate = false;
            }
        }
    }
}
