using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private Skeleton_Enemy skeleton;
    private float stunForce;
    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _stateMachine, string animBoolName, Skeleton_Enemy _skeleton) : base(_enemy, _stateMachine, animBoolName)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        stunForce = skeleton.knockBackForce + 2;
        enemy.fx.InvokeRepeating("StunEffect", 0, .1f);
        rb.AddForce(new Vector2(1 * -skeleton.facingDir, 1) * stunForce , ForceMode2D.Impulse);
        stateTimer = skeleton.stunDuration;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelStunEffect", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.idleState);
    }
}
