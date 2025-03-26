using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item Effect/Thunder_Strike")]
public class ThunderStrike_Effect : Item_Effect
{
    [SerializeField] GameObject thunderStrikePrfabs;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        Instantiate(thunderStrikePrfabs, _targetTransform.position, Quaternion.identity);
    }
}
