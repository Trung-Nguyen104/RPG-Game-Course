using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect/Heal")]
public class Heal_Effect : Item_Effect
{
    [Range(0, 100)]
    [SerializeField] private int healPercent;
    private Player_Stats playerStats;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        playerStats = Player_Manager.Instance.Player.GetComponent<Player_Stats>();

        playerStats.IncreaseHealth(HealAmount(), playerStats);
    }

    private int HealAmount()
    {
        return Mathf.RoundToInt(playerStats.GetMaxHealth() * (healPercent / 100));
    }
}
