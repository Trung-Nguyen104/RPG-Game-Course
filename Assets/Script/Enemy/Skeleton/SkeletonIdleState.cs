using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, Skeleton_Enemy _skeleton) : base(_enemy, _stateMachine, animBoolName, _skeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0) 
        {
            stateMachine.ChangeState(skeleton.moveState);
        }
    }
}
