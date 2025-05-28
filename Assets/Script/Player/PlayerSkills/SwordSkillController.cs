using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    private Animator animator;
    private CapsuleCollider2D cd2D;
    private List<Transform> enemyTarget;
    private bool canFly = true;
    private bool canReturn = false;
    private bool isSwordBounce = false;
    private bool isSwordPierce = false;
    private int bounceAmount;
    private int pierceAmount;
    private int targetIndex = 0;
    private float bounceSpeed;
    public int SwordDamage { get; set; }


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
        if (collision.TryGetComponent<Enemy>(out var enemyCollision))
        {
            player.EntityStats.HandleMagicalDamage(enemyCollision.EntityStats);
            if(isSwordBounce)
            {
                BouncingTargetSetUp();
            }
            if (isSwordPierce)
            {
                PierceSwordHandle();
            }
        }
        StuckInToTarget(collision);
    }

    #region SwordBounce

    public void SetUpSwordBounce(int _amountOfBounce, float _bounceSpeed, bool _canBounce, int _swordDamage)
    {
        this.bounceAmount = _amountOfBounce;
        this.bounceSpeed = _bounceSpeed;
        this.isSwordBounce = _canBounce;
        this.SwordDamage = _swordDamage;
        enemyTarget = new();
        SwordAnimationHandle(true);
    }

    private void BouncingHandle()
    {
        if (isSwordBounce && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                targetIndex++;
                bounceAmount--;
                if (bounceAmount < 0)
                {
                    isSwordBounce = false;
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
        if (enemyTarget.Count <= 0)
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

    public void SetUpSwordPierce(int _pierceAmount, bool _canPierce, int _swordDamage)
    {
        this.pierceAmount = _pierceAmount;
        this.isSwordPierce = _canPierce;
        this.SwordDamage = _swordDamage;
    }

    private void PierceSwordHandle()
    {
        pierceAmount--;
        if (pierceAmount <= 0)
        {
            isSwordPierce = false;
        }
    }

    #endregion

    private void StuckInToTarget(Collider2D collision)
    {
        if (isSwordPierce || (isSwordBounce && enemyTarget.Count > 0))
            return;

        StopAndFreezeSword();

        cd2D.enabled = false;
        transform.parent = collision.transform;
        SwordAnimationHandle(false);
    }

    private void StopAndFreezeSword()
    {
        canFly = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
                player.PlayerStateMachine.ChangeState(player.CatchSwordState);
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
