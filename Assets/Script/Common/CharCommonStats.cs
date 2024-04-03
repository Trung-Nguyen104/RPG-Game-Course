using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCommonStats : MonoBehaviour
{
    public Stats damage;
    public Stats strength;
    public Stats maxHP;
    [SerializeField] protected int currHP;

    protected virtual void Start()
    {
        currHP = maxHP.GetValue();
    }
    
    public virtual void HandleDamage(CharCommonStats _targetStats)
    {
        var totalDamage = damage.GetValue() + strength.GetValue();
        _targetStats.TakeDamageHP(totalDamage);
    }

    public virtual void TakeDamageHP(int _damage)
    {
        currHP -= _damage;
        Debug.Log(currHP);
        if (currHP <= 0) 
        {
            HandleDie();
        }
    }

    protected virtual void HandleDie()
    {
        Debug.Log(gameObject + "Died");
    }
}
