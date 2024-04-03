using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill : Skill
{
    [SerializeField] private GameObject blackHolePrefabs;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float cloneAttackCoolDown;
    
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHolePrefabs = GameObject.Instantiate(blackHolePrefabs, player.transform.position, Quaternion.identity);
        UltimateSkillController ultimateBlackHole = newBlackHolePrefabs.GetComponent<UltimateSkillController>();
        ultimateBlackHole.SetUpUltimateBlackHole(maxSize, growSpeed, cloneAttackCoolDown);
    }
}
