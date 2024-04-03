using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharCommonStats
{
    private Player player;
    public override void TakeDamageHP(int _damage)
    {
        base.TakeDamageHP(_damage);
        player.TakeDamageEffect();
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        player.DeadEffect();
    }

    protected override void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }
}
