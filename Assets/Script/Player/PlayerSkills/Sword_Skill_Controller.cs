using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    private Animator animator;
    private CapsuleCollider2D cd2D;
    private List<Transform> enemyTarget;
    private bool canFly = true;
    private bool canReturn = false;
    private bool canBounce = false;
    private bool canPierce = false;
    private int bounceAmount;
    private int pierceAmount;
    private int targetIndex = 0;
    private float bounceSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        cd2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (canFly)
        {
            transform.right = rb.velocity;
        }
        SwordReturnHandle();
        BouncingHandle();
    }


    public void SetUpSword(Vector2 _direction, float _gravityScale, float _throwForce, Player _player)
    {
        this.player = _player;
        this.rb.AddForce(_direction * _throwForce, ForceMode2D.Impulse);
        this.rb.gravityScale = _gravityScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Enemy>()?.Damage();
        if (collision.GetComponent<Enemy>() != null)
        {
            BouncingTargetSetUp();
            PierceSwordHandle();
        }
        StuckInToTarget(collision);
    }

    #region SwordBounce

    public void SetUpSwordBounce(int _amountOfBounce, float _bounceSpeed, bool _canBounce)
    {
        this.bounceAmount = _amountOfBounce;
        this.bounceSpeed = _bounceSpeed;
        this.canBounce = _canBounce;
        enemyTarget = new();
        SwordAnimationHandle(true);
    }

    private void BouncingHandle()
    {
        if (canBounce && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                targetIndex++;
                bounceAmount--;
                if (bounceAmount < 0)
                {
                    canBounce = false;
                    canReturn = true;
                }
                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void BouncingTargetSetUp()
    {
        if (canBounce && enemyTarget.Count <= 0)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    enemyTarget.Add(hit.transform);
                }
            }
        }
    }
    #endregion

    #region SwordPierce

    public void SetUpSwordPierce(int _pierceAmount, bool _canPierce)
    {
        this.pierceAmount = _pierceAmount;
        this.canPierce = _canPierce;
    }

    private void PierceSwordHandle()
    {
        if (canPierce)
            pierceAmount--;
        if (pierceAmount <= 0)
            canPierce = false;
    }

    #endregion

    private void StuckInToTarget(Collider2D collision)
    {
        if (canPierce)
            return;

        canFly = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (canBounce && enemyTarget.Count > 0)
            return;

        cd2D.enabled = false;
        transform.parent = collision.transform;
        SwordAnimationHandle(false);
    }

    public void SwordReturnToPlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        canReturn = true;
        //TeleToSword();
    }

    private void SwordReturnHandle()
    {
        if (canReturn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 30f * Time.deltaTime);
            SwordAnimationHandle(true);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.CanCreateNewSword(true);
                player.playerStateMachine.ChangeState(player.catchSwordState);
                Destroy(gameObject);
            }
        }
    }

    private void TeleToSword()
    {
        (transform.position, player.transform.position) = (player.transform.position, transform.position);
        player.CanCreateNewSword(true);
        Destroy(gameObject);
    }

    private void SwordAnimationHandle(bool _boolValue)
    {
        animator.SetBool("SwordSpin", _boolValue);
    }

}
