using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ignite Effect", menuName = "Data/Item Effect/Ignite")]
public class Ignite_Efftect : Item_Effect
{
    [SerializeField] GameObject ignitePrefabs;

    public override void ExecuteEffect(Transform _targetTransform)
    {
        if (_targetTransform.GetComponentInChildren<Ignite_Controller>() != null)
        {
            return;
        }

        var targetBehaviour = _targetTransform.GetComponentInParent<Entity_Behavior>();
        var igniteEffect = Instantiate(ignitePrefabs, _targetTransform.position, Quaternion.identity);

        igniteEffect.GetComponent<Ignite_Controller>().targetBehaviour = targetBehaviour;
        igniteEffect.transform.parent = _targetTransform.transform;
        Destroy(igniteEffect, 3f);
    }
}
