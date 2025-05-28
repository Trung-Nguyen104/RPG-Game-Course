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
    public virtual EnemyState IdleStateProperty { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
        if (canBeCounter)
        {
            isTakeDamged = false;
        }
    }

    public virtual RaycastHit2D PlayerDetected() => Physics2D.Raycast(horizontalCheck.position, Vector2.right * facingDir, sawPlayerDistance, playerLayer);
    public virtual void AnimationTrigger() => StateMachine.CurrentState.AnimCalledTrigger();

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
            StateMachine.CurrentState.Pause(true);
        }
        else
        {
            moveSpeed = 1;
            StateMachine.CurrentState.Pause(false);
        }
    }

    public virtual void FreezaTimeFor(float _duration)
    {
        StartCoroutine(FreezeCoroutine(_duration));
    }

    public override void DeadEffect()
    {
        base.DeadEffect();
        animator.speed = 1;
        cd2D.enabled = false;
        rb.isKinematic = true;
        SetVelocity(0, 0);
        StartCoroutine(FadeOutAndDisappear());
    }

    protected virtual IEnumerator FreezeCoroutine(float _seconds)
    {
        Freeze(true);
        yield return new WaitForSeconds(_seconds);
        Freeze(false);
    }

    private IEnumerator FadeOutAndDisappear()
    {
        if (sr == null)
        {
            yield break;
        }

        float duration = 5f;
        float elapsed = 0f;
        var originalColor = sr.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            sr.color = new(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        sr.color = new(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}
