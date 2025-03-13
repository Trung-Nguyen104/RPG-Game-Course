using UnityEngine;

[CreateAssetMenu(fileName = "Freeze Effect", menuName = "Data/Item Effect/Freeze Effect")]
public class Freeze_Effect : Item_Effect
{
    [SerializeField] private int duration;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        _targetTransform.GetComponent<Enemy>()?.FreezaTimeFor(duration);
    }
}
