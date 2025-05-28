using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : Entity_Stats
{
    public Enemy enemy;
    private ItemDrop itemDrop;

    [Header("Level")]
    [SerializeField] private int level;
    public Stats soulsDropAmount;

    [Range(0f, 1f)]
    [SerializeField] private float percentageModifier;

    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(Random.Range(10, 100));
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

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(physicDamage);
        Modify(criticalRate);
        Modify(criticalDamage);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

        Modify(soulsDropAmount);
    }

    private void Modify(Stats stats)
    {
        for (int i = 1; i < level; i++)
        {
            var modifier = stats.GetValue() * percentageModifier;
            stats.AddModifier(Mathf.RoundToInt(modifier));
        }
    }

    public override void TakeDamageHP(int _damage, Entity_Stats _targetStats)
    {
        base.TakeDamageHP(_damage, _targetStats);
        enemy.TakeDamageEffect();
    }

    protected override void HandleDie()
    {
        base.HandleDie();
        enemy.wasDead = true;
        enemy.DeadEffect();
        itemDrop.GenerateDrop();
        Skill_Manager.Instance.souls += soulsDropAmount.GetValue();
    }
}
