using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect")]
public class Item_Effect : ScriptableObject
{
    public virtual void ExecuteEffect(Transform _targetTransform)
    {

    }
}
