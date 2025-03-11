using UnityEngine;

public enum StatsType
{
    strength,
    agility,
    intelligence,
    vitality,
    maxHealth,
    armor,
    evasion,
    magicResistance,
    physicDamage,
    criticalRate,
    criticalDamage,
    fireDamage,
    iceDamage,
    lightingDamage,
}

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class Buff_Effect : Item_Effect
{
    private PlayerStats stats;
    [SerializeField] private StatsType buffType;
    [SerializeField] private int buffAmount;
    [SerializeField] private int buffDuration;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
        stats.IncreaseStatsBy(buffAmount, buffDuration, StatsToModify());
    }

    private Stats StatsToModify()
    {
        switch (buffType)
        {
            case StatsType.strength:
                return stats.strength;
            case StatsType.agility: 
                return stats.agility;
            case StatsType.intelligence:
                return stats.intelligence;
            case StatsType.vitality:
                return stats.vitality;
            case StatsType.maxHealth:
                return stats.maxHealth;
            case StatsType.armor:
                return stats.armor;
            case StatsType.evasion:
                return stats.evasion;
            case StatsType.magicResistance:
                return stats.magicResistance;
            case StatsType.physicDamage:
                return stats.physicDamage;
            case StatsType.criticalRate:
                return stats.criticalRate;
            case StatsType.fireDamage:
                return stats.fireDamage;
            case StatsType.iceDamage:
                return stats.iceDamage;
            case StatsType.lightingDamage:
                return stats.lightingDamage;
            default:
                return null;
        }
    }
}
