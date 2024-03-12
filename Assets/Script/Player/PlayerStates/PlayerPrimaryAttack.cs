using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimerAttacked;
    private float comboTimer = 1f;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if( comboCounter > 2 || Time.time >= lastTimerAttacked + comboTimer)
            comboCounter = 0;
         
        player.animator.SetInteger("ComboCounter", comboCounter);
        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);
    }

    public override void Exit()
    {
        base.Exit(); 

        comboCounter++;
        lastTimerAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (animCalledTrigger)
            playerStateMachine.ChangeState(player.idleState);
    }
}
