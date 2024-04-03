using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharCommonStats
{
    private Enemy enemy;

    public override void TakeDamageHP(int _damage)
    {
        base.TakeDamageHP(_damage);
        enemy.TakeDamageEffect();
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        enemy.DeadEffect();
    }

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }
}
