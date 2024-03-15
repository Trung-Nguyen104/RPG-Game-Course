using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Skill_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Player player;
    private Animator animator;
    private CapsuleCollider2D cd2D;
    private bool canFly = true;
    private bool canReturn = false;

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
        /* call take the sword function
        else
        {
            TakeTheSwordHandle();
        }
        */
    }

    public void SetUpSword(Vector2 _direction, float _gravityScale, Player _player)
    {
        player = _player;
        rb.AddForce(_direction, ForceMode2D.Impulse);
        rb.gravityScale = _gravityScale;
        SwordAnimationHandle(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canFly = false;
        cd2D.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
        SwordAnimationHandle(false);
    }

    public void SwordReturnToPlayer()
    {
        rb.isKinematic = false;
        transform.parent = null;
        canReturn = true;
    }

    private void SwordReturnHandle()
    {
        if(canReturn) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 15f * Time.deltaTime);
            SwordAnimationHandle(true);
            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.CanCreateNewSword(true);
                player.playerStateMachine.ChangeState(player.catchSwordState);
                Destroy(gameObject);
            }
        }
    }

    private void SwordAnimationHandle(bool _boolValue)
    {
        animator.SetBool("SwordSpin", _boolValue);
    }

    /* Take The Sword Ability
    private void TakeTheSwordHandle()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            player.CanCreateNewSword(true);
            Destroy(gameObject);
        }
    }
    */

}
