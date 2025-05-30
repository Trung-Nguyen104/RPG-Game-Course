using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerState
{
    protected PlayerStateMachine playerStateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    protected float xInput;
    protected float timerState;
    protected bool animCalledTrigger;
    private string animationName;

    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animationName)
    {
        this.player = _player;
        this.playerStateMachine = _playerStateMachine;
        this.animationName = _animationName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animationName, true);
        rb = player.rb;
        animCalledTrigger = false;
    }

    public virtual void Update()
    {
        timerState -= Time.deltaTime;
        OnMove();
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }


    public virtual void Exit() 
    {
        player.animator.SetBool(animationName, false);
    }

    public virtual void AnimCalledTrigger()
    {
        animCalledTrigger = true;
    }

    private void OnMove()
    {
        xInput = Inputs.Instance.GetHorizontal();
        if (xInput != 0 && !player.IsBusy)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
    }
}
