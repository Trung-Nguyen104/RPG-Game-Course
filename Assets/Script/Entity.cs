using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("KnockBack")]
    [SerializeField] public float knockBackForce;
    [SerializeField] protected float knockBackTime;
    protected bool isTakeDamged;

    [Header("Collision Check")]
    [SerializeField] public Transform attackCheck;
    [SerializeField] public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform horizontalCheck;
    [SerializeField] protected float horizontalCheckDistance;
    [SerializeField] protected LayerMask platformLayer;

    public float facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        StartCoroutine("KnockBack");
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isTakeDamged)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual bool GroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, platformLayer);

    public virtual bool WallDetected() => Physics2D.Raycast(horizontalCheck.position, Vector2.right, horizontalCheckDistance * facingDir, platformLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(horizontalCheck.position, new Vector3(horizontalCheck.position.x + horizontalCheckDistance * facingDir, horizontalCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _xInput)
    {
        if (_xInput > 0 && !facingRight)
            Flip();
        else if (_xInput < 0 && facingRight)
            Flip();
    }

    public virtual IEnumerator KnockBack()
    {
        isTakeDamged = true;
        rb.AddForce(new Vector2(1 * -facingDir, 1) * knockBackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        isTakeDamged = false;
    }
}