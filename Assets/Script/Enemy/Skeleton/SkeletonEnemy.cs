using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    public override EnemyState IdleStateProperty { get; protected set; }

    #region States
    public SkeletonIdleState IdleState {  get; private set; }
    public SkeletonMoveState MoveState {  get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    public SkeletonSawPlayerState SawPlayerState { get; private set; }
    public SkeletonTakeDamgeState TakeDamgeState { get; private set; }
    public SkeletonStunState StunState { get; private set; }
    public SkeletonDeadState DeadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        IdleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, StateMachine, "Move", this);
        SawPlayerState = new SkeletonSawPlayerState(this, StateMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
        TakeDamgeState = new SkeletonTakeDamgeState(this, StateMachine, "TakeDamge", this);
        StunState = new SkeletonStunState(this, StateMachine, "Stunned", this);
        DeadState = new SkeletonDeadState(this, StateMachine, "Dead", this);

        IdleStateProperty = IdleState;
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        
        if(wasDead)
        {
            StateMachine.ChangeState(DeadState);
        }
        else
        {
            if (isTakeDamged)
            {
                StateMachine.ChangeState(TakeDamgeState);
            }
            if (beCountered)
            {
                StateMachine.ChangeState(StunState);
                CloseCounterSignal();
            }
        }
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        StateMachine.ChangeState(DeadState);
    }
}
