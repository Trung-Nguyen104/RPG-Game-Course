using UnityEngine;

[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
public class Buff_Effect : Item_Effect
{
    private Player_Stats stats;
    [SerializeField] private StatType buffType;
    [SerializeField] private int buffAmount;
    [SerializeField] private int buffDuration;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        stats = Player_Manager.Instance.Player.GetComponent<Player_Stats>();
        stats.IncreaseStatsBy(buffAmount, buffDuration, stats.GetType(buffType));
    }
}
