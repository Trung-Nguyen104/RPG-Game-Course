using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharCommonStats
{
    private Enemy enemy;
    private ItemDrop itemDrop;

    [Header("Level")]
    [SerializeField] private int level;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier;

    protected override void Start()
    {
        ApplyLevelModifiers();
        base.Start();
        enemy = GetComponent<Enemy>();
        itemDrop = GetComponent<ItemDrop>();
    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(health);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(physicDamage);
        Modify(criticalRate);
        Modify(criticalDamage);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);
    }

    private void Modify(Stats stats)
    {
        for (int i = 1; i < level; i++)
        {
            var modifier = stats.GetValue() * percentageModifier;
            stats.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamageHP(int _damage)
    {
        base.TakeDamageHP(_damage);
        enemy.TakeDamageEffect();
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        enemy.DeadEffect();
        itemDrop.GenerateDrop();
    }
}
