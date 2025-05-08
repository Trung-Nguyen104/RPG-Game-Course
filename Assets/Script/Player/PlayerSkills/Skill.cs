using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool skill_Unlocked;
    public float coolDown;
    public float coolDownTimer;

    protected Player player;

    protected virtual void Start()
    {
        player = Player_Manager.Instance.Player;
    }

    protected virtual void Update()
    {
        if(coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(coolDownTimer <= 0)
        {
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }

    public virtual void OnUnlockedSkill(string _getUpgrade)
    {
        skill_Unlocked = true;
    }
}
