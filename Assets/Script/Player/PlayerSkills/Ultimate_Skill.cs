using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Skill : Skill
{
    [SerializeField] private GameObject blackHolePrefabs;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float cloneAttackCoolDown;
    
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHolePrefabs = GameObject.Instantiate(blackHolePrefabs, player.transform.position, Quaternion.identity);
        Ultimate_Skill_Controller ultimateBlackHole = newBlackHolePrefabs.GetComponent<Ultimate_Skill_Controller>();
        ultimateBlackHole.SetUpUltimateBlackHole(maxSize, growSpeed, cloneAttackCoolDown);
    }
}
