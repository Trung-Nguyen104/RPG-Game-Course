using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    #region States
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState {  get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonSawPlayerState sawPlayerState { get; private set; }
    public SkeletonTakeDamgeState takeDamgeState { get; private set; }
    public SkeletonStunState stunState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        sawPlayerState = new SkeletonSawPlayerState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        takeDamgeState = new SkeletonTakeDamgeState(this, stateMachine, "TakeDamge", this);
        stunState = new SkeletonStunState(this, stateMachine, "Stunned", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        if(wasDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else
        {
            if (isTakeDamged)
            {
                stateMachine.ChangeState(takeDamgeState);
            }
        }
    }

    public override bool CanBeStun()
    {
        if (base.CanBeStun())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        animator.speed = 0;
        wasDead = true;
        stateMachine.ChangeState(deadState);
    }
}
