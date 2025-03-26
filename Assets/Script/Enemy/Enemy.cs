using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity_Behavior
{
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] public float idleTime;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;

    [Header("CounterAttack")]
    [SerializeField] public GameObject counterSignal;
    [SerializeField] public float stunDuration;
    public bool beCountered = false;
    protected bool canBeCounter;

    [Header("PlayerDetection")]
    [SerializeField] public float sawPlayerMoveSpeed;
    [SerializeField] public float sawPlayerDistance;
    [SerializeField] public float attackPlayerDistance;
    public EnemyStateMachine StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
        if (canBeCounter)
        {
            isTakeDamged = false;
        }
    }

    public virtual RaycastHit2D PlayerDetected() => Physics2D.Raycast(horizontalCheck.position, Vector2.right * facingDir, sawPlayerDistance, playerLayer);
    public virtual void AnimationTrigger() => StateMachine.currentState.AnimCalledTrigger();

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
        beCountered = false;
        counterSignal.SetActive(false);
    }

    public virtual bool CanBeStun()
    {
        if (canBeCounter)
        {
            return true;
        }
        return false;
    }

    public virtual void Freeze(bool _boolValue)
    {
        if(_boolValue)
        {
            moveSpeed = 0;
            SetVelocity(0, 0);
            StateMachine.currentState.Pause(true);
        }
        else
        {
            moveSpeed = 1;
            StateMachine.currentState.Pause(false);
        }
    }

    public virtual void FreezaTimeFor(float _duration)
    {
        StartCoroutine(FreezeCoroutine(_duration));
    }

    protected virtual IEnumerator FreezeCoroutine(float _seconds)
    {
        Freeze(true);
        yield return new WaitForSeconds(_seconds);
        Freeze(false);
    }
}
