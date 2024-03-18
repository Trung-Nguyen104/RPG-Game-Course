using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSword : PlayerState
{
    private Transform swordTransform;
    public PlayerCatchSword(Player _player, PlayerStateMachine _playerStateMachine, string _animationName) : base(_player, _playerStateMachine, _animationName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        swordTransform = player.skillManager.throwSword.swordController.transform;
        PlayerFlipToSword();
        rb.AddForce(new Vector2(2f * -player.facingDir, rb.velocity.y), ForceMode2D.Impulse);
        timerState = .5f;
        player.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (timerState < 0)
            playerStateMachine.ChangeState(player.idleState);
    }

    private void PlayerFlipToSword()
    {
        if(player.transform.position.x > swordTransform.position.x && player.facingDir == 1)
        {
            player.Flip();
        }
        else if(player.transform.position.x < swordTransform.position.x && player.facingDir == -1)
        {
            player.Flip();
        }
    }
}
