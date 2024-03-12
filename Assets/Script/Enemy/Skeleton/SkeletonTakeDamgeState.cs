using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTakeDamgeState : EnemyState
{
    Enemy_Skeleton skeleton;
    public SkeletonTakeDamgeState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, Enemy_Skeleton _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
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
        stateMachine.ChangeState(skeleton.sawPlayerState);
    }
}
