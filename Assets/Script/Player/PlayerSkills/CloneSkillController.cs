using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private Transform attackCheck;
    [SerializeField] private int cloneAttackDamage;
    private float invisibleSpeed;
    private float cloneTimeDuration;
    private float cloneTimer;
    private SpriteRenderer sr;
    private Animator animator;
    private float attackCheckRadius = 1.15f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleCloneDisappear();
    }

    private void HandleCloneDisappear()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * invisibleSpeed));
            if (sr.color.a < 0)
                GameObject.Destroy(gameObject);
        }
    }

    public void SetUpClone(Transform _cloneTransform, float _cloneTimeDuration, float _invisibleSpeed, Vector3 _offset, int _cloneDamage)
    {
        transform.position = _cloneTransform.position + _offset;
        cloneTimeDuration = _cloneTimeDuration;
        invisibleSpeed = _invisibleSpeed;
        cloneAttackDamage = _cloneDamage;
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
                hit.GetComponent<Enemy>().EntityStats.TakeDamageHP(cloneAttackDamage, hit.GetComponent<Enemy>().EntityStats);
            }
        }

    }

    public void FaceToEnemySize(Vector3 _offset)
    {
        if(_offset.x == 2)
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
        else
        {
            return;
        }
    }
}
