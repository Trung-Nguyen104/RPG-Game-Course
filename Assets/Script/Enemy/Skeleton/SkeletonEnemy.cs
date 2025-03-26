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

        idleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, StateMachine, "Move", this);
        sawPlayerState = new SkeletonSawPlayerState(this, StateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
        takeDamgeState = new SkeletonTakeDamgeState(this, StateMachine, "TakeDamge", this);
        stunState = new SkeletonStunState(this, StateMachine, "Stunned", this);
        deadState = new SkeletonDeadState(this, StateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        if(wasDead)
        {
            StateMachine.ChangeState(deadState);
        }
        else
        {
            //if (isTakeDamged)
            //{
            //    StateMachine.ChangeState(takeDamgeState);
            //}
            if(beCountered)
            {
                StateMachine.ChangeState(stunState);
                CloseCounterSignal();
            }
        }
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        StateMachine.ChangeState(deadState);
    }
}
