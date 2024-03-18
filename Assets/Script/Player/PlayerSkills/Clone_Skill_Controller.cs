using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Clone_Skill_Controller : MonoBehaviour
{
    [SerializeField] private Transform attackCheck;
    private float invisibleSpeed;
    private float cloneTimeDuration;
    private float cloneTimer;
    private SpriteRenderer sr;
    private Animator animator;
    private Enemy enemy;
    private float attackCheckRadius = 1.15f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if( cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * invisibleSpeed));
            if(sr.color.a < 0 )
                GameObject.Destroy(gameObject);
        }
    }

    public void SetUpClone(Transform _cloneTransform, float _cloneTimeDuration, float _invisibleSpeed)
    {
        transform.position = _cloneTransform.position;
        cloneTimeDuration = _cloneTimeDuration;
        invisibleSpeed = _invisibleSpeed;
        cloneTimer = cloneTimeDuration;
        animator.SetInteger("AttackNumber", Random.Range(1, 4));
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    enemy = hit.GetComponent<Enemy>();
                    if (!enemy.CanBeStun())
                        hit.GetComponent<Enemy>().Damage();
                }
            }
        }

    }

    public void FaceToEnemySize(Transform _playerTransform)
    {
        gameObject.transform.rotation = _playerTransform.rotation;
    }
}
