using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Heal")]
public class Heal_Effect : Item_Effect
{
    [Range(0, 100)]
    [SerializeField] private int healPercent;
    private PlayerStats playerStats;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        playerStats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        playerStats.IncreaseHealth(HealAmount());
    }

    private int HealAmount()
    {
        return Mathf.RoundToInt(playerStats.GetMaxHealth() * (healPercent / 100));
    }
}
