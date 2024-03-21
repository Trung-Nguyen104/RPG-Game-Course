using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] public float idleTime;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [Header("CounterAttack")]
    [SerializeField] public GameObject counterSignal;
    [SerializeField] public float stunDuration;
    protected bool canBeCounter;

    [Header("PlayerDetection")]
    [SerializeField] public float sawPlayerMoveSpeed;
    [SerializeField] public float sawPlayerDistance;
    [SerializeField] public float attackPlayerDistance;
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D PlayerDetected() => Physics2D.Raycast(horizontalCheck.position, Vector2.right * facingDir, sawPlayerDistance, playerLayer);
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimCalledTrigger();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(horizontalCheck.position, new Vector3(horizontalCheck.position.x + sawPlayerDistance * facingDir, horizontalCheck.position.y));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(horizontalCheck.position, new Vector3(horizontalCheck.position.x + attackPlayerDistance * facingDir, horizontalCheck.position.y));
    }

    public virtual void OpenCounterSignal()
    {
        canBeCounter = true;
        counterSignal.SetActive(true);
    }
    public virtual void CloseCounterSignal()
    {
        canBeCounter = false;
        counterSignal.SetActive(false);
    }

    public virtual bool CanBeStun()
    {
        if (canBeCounter)
        {
            CloseCounterSignal();
            return true;
        }
        return false;
    }

    public virtual void Freeze(bool _boolValue)
    {
        if(_boolValue)
        {
            moveSpeed = 0;
            animator.speed = 0;
            SetVelocity(0, 0);
        }
        else
        {
            moveSpeed = 1;
            animator.speed = 1;
        }
    }
}
