using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Food
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    public Item_Effect[] itemEffects;

    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Defensive Stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Damage Stats")]
    public int physicDamage;
    public int criticalRate;
    public int criticalDamage;

    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;

    public void ExecuteItemEfftect(Transform targetPosition)
    {
        foreach (var item in itemEffects)
        {
            item.ExecuteEffect(targetPosition);
        }
    }

    public void AddModifiers()
    {
        var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);

        playerStats.physicDamage.AddModifier(physicDamage);
        playerStats.criticalRate.AddModifier(criticalRate);
        playerStats.criticalDamage.AddModifier(criticalDamage);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    public void RemoveModifiers() 
    {
        var playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);

        playerStats.physicDamage.RemoveModifier(physicDamage);
        playerStats.criticalRate.RemoveModifier(criticalRate);
        playerStats.criticalDamage.RemoveModifier(criticalDamage);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }
}
