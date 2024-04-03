using System.Collections;
using System.Collections.Generic;
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
            if (hit.GetComponent<Enemy>() != null)
            {
                var enemy = hit.GetComponent<Enemy>();
                var enemyTarget = hit.GetComponent<CharCommonStats>();
                
                if (!enemy.CanBeStun())
                    player.charStats.HandleDamage(enemyTarget);
            }
        }

    }

    private void ThrowSword()
    {
        SkillManager.Instance.throwSword.CreateSword();
    }
}
