using System.Collections.Generic;
using UnityEngine;

public class UltimateSkillController : MonoBehaviour
{
    [SerializeField] private List<Transform> targets = new();
    private Player player;
    private float maxSize;
    private float growSpeed;
    private bool canUseUltimate;
    private bool canShrink;
    private int attackCount;
    private float cloneAttackTimer;
    private float cloneAttackCoolDown = .5f;
    private int cloneAttackDamage;

    private void Update()
    {
        cloneAttackTimer -= Time.deltaTime;
        CheckCanUseUltimate();
        DestroyUltimateBlackHole();
    }

    public void SetUpUltimateBlackHole(float _maxSize, float _growSpeed, float _cloneAttackCoolDown, int _damage)
    {
        maxSize = _maxSize;
        attackCount = Mathf.RoundToInt(_maxSize);
        growSpeed = _growSpeed;
        cloneAttackCoolDown = _cloneAttackCoolDown;
        canUseUltimate = true;
        cloneAttackDamage = _damage;
        player = Player_Manager.Instance.Player;
    }

    private void CheckCanUseUltimate()
    {
        if (canUseUltimate)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
            if (transform.localScale.x > maxSize - 0.3)
            {
                if(targets.Count <= 0 || attackCount <= 0)
                {
                    Invoke(nameof(TurnOffUltimate), 0.5f);
                }
                else
                {
                    player.SetTransprent(true);
                    CreateUltimateCloneAttack();
                }
            }
        }
    }

    private void TurnOffUltimate()
    {
        canUseUltimate = false;
        canShrink = true;
    }

    private void DestroyUltimateBlackHole()
    {
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), (growSpeed + 2) * Time.deltaTime);
            player.playerStateMachine.ChangeState(player.airState);
            player.SetTransprent(false);
            if (transform.localScale.x < 0f)
            {
                canShrink = true;
                Destroy(gameObject);
            }
        }
    }

    private void CreateUltimateCloneAttack()
    {
        int random = Random.Range(0, targets.Count);
        if (cloneAttackTimer < 0)
        {
            cloneAttackTimer = cloneAttackCoolDown;
            var createClone = Skill_Manager.Instance.CreateClone;
            createClone.cloneAttackDamage = this.cloneAttackDamage;
            createClone.CreateClone(targets[random], new Vector3(SetUpOffSetX(), 0));
            attackCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyCollision = collision.GetComponent<Enemy>();
        if ( enemyCollision != null)
        {
            enemyCollision.Freeze(true);
            AddEnemyToList(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.Freeze(false);

    public void AddEnemyToList(Transform _enemyTransform)
    {
        targets.Add(_enemyTransform);
    }

    private float SetUpOffSetX()
    {
        float offsetX;
        if (Random.Range(0, 100) > 50)
        {
            offsetX = 2;
        }
        else
        {
            offsetX = -2;
        }
        return offsetX;
    }
}
