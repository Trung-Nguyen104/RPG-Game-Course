using UnityEngine;

public class PlayerAnimTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();


    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                var enemyTarget = hit.GetComponent<CharCommonStats>();
                if (enemy.CanBeStun())
                {
                    enemy.beCountered = true;
                }
                player.charStats.HandleDamage(enemyTarget);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.Instance.throwSword.CreateSword();
    }
}
