using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimTriggers : MonoBehaviour
{
    private Skeleton_Enemy skeleton => GetComponentInParent<Skeleton_Enemy>();

    private void AnimationTrigger()
    {
        skeleton.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] attackCollider = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackCheckRadius);

        foreach (var hit in attackCollider)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
            }
        }

    }

    private void OpenCounterSignalTrigger() => skeleton.OpenCounterSignal();
    private void CloseCounterSignalTrigger() => skeleton.CloseCounterSignal();
}
