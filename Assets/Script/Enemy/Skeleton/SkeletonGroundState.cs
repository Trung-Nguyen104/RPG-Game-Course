using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Skeleton_Enemy skeleton;
    protected Transform player; 
    public SkeletonGroundState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, Skeleton_Enemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
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
        if(enemy.PlayerDetected() || Vector2.Distance(skeleton.transform.position, player.transform.position) < 4)
            stateMachine.ChangeState(skeleton.sawPlayerState);
    }
}
