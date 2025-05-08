using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySkill : Skill
{
    public override void UseSkill()
    {
        base.UseSkill();
    }

    public override void OnUnlockedSkill(string _modifyName)
    {
        base.OnUnlockedSkill(_modifyName);
        if (_modifyName != null)
        {
            return;
        }
    }
}
