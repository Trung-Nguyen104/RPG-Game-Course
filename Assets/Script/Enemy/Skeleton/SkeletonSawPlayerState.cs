using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSawPlayerState : EnemyState
{
    private Player player;
    private SkeletonEnemy skeleton;
    private int moveDir;
    public SkeletonSawPlayerState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, SkeletonEnemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.Player;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        CheckPlayerPosition();

        skeleton.SetVelocity(skeleton.sawPlayerMoveSpeed * moveDir, rb.velocity.y);

        CheckPlayerDetection();
    }

    private void CheckPlayerPosition()
    {
        if (player.wasDead)
            return;

        if (player.transform.position.x > skeleton.transform.position.x)
            moveDir = 1;
        else if (player.transform.position.x < skeleton.transform.position.x)
            moveDir = -1;
    }

    private void CheckPlayerDetection()
    {
        if (!skeleton.PlayerDetected())
        {
            stateMachine.ChangeState(skeleton.moveState);
        }
        else if (skeleton.PlayerDetected())
        {
            if (skeleton.PlayerDetected().distance < skeleton.attackPlayerDistance)
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }
    }
}
