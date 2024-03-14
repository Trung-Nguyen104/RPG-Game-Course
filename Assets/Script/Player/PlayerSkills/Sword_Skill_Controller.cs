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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cd2D = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (canFly)
            transform.right = rb.velocity;
    }
    public void SetUpSword(Vector2 _direction, float _gravityScale)
    {
        rb.AddForce(_direction, ForceMode2D.Impulse);
        rb.gravityScale = _gravityScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canFly = false;
        cd2D.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;
    }
}
