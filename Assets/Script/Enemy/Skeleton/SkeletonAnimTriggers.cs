using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimTriggers : MonoBehaviour
{
    private SkeletonEnemy skeleton => GetComponentInParent<SkeletonEnemy>();

    private void AnimationTrigger()
    {
        skeleton.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            var player = hit.GetComponent<Player>();
            if (player != null)
            {
                var playerTarget =  hit.GetComponent<Entity_Stats>();
                skeleton.EntityStats.HandleDamage(playerTarget);
            }
        }

    }

    private void OpenCounterSignalTrigger() => skeleton.OpenCounterSignal();
    private void CloseCounterSignalTrigger() => skeleton.CloseCounterSignal();
}
