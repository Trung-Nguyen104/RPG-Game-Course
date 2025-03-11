using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ignite Effect", menuName = "Data/Item Effect/Ignite")]
public class Ignite_Efftect : Item_Effect
{
    [SerializeField] GameObject ignitePrefabs;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        GameObject newThunderStrike = Instantiate(ignitePrefabs, _targetTransform.position, Quaternion.identity);
        Destroy(newThunderStrike, 5f);
    }
}
