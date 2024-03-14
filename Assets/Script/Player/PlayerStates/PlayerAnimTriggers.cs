using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private Enemy enemy;

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
                enemy = hit.GetComponent<Enemy>();
                if (!enemy.CanBeStun())
                    hit.GetComponent<Enemy>().Damage();
            }
        }

    }

    private void ThrowSword()
    {
        SkillManager.Instance.throwSword.CreateSword();
    }
}
