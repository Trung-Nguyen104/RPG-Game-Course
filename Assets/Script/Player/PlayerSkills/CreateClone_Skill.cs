using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateClone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneTimeDuration;
    [SerializeField] private float invisibleSpeed;

    //[SerializeField] private bool canAttack;
    
    public void CreateClone(Transform _playerTransform)
    {
        GameObject newClone = GameObject.Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(_playerTransform, cloneTimeDuration, invisibleSpeed);
        newClone.GetComponent<Clone_Skill_Controller>().FaceToEnemySize(_playerTransform);
    }
}
