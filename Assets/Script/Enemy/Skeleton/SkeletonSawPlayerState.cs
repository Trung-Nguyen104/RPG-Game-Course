using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSawPlayerState : EnemyState
{
    private Transform player;
    private Skeleton_Enemy skeleton;
    private int moveDir;
    public SkeletonSawPlayerState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, Skeleton_Enemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.position.x > skeleton.transform.position.x)
            moveDir = 1;
        else if (player.position.x < skeleton.transform.position.x)
            moveDir = -1;
        skeleton.SetVelocity(skeleton.sawPlayerMoveSpeed * moveDir, rb.velocity.y);

        if (!skeleton.PlayerDetected())
            stateMachine.ChangeState(skeleton.moveState);
        else if (skeleton.PlayerDetected())
        {
            if(skeleton.PlayerDetected().distance < skeleton.attackPlayerDistance) 
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }

    }
}
