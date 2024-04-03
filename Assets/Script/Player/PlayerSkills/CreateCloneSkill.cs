using UnityEngine;

public class CreateCloneSkill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneTimeDuration;
    [SerializeField] private float invisibleSpeed;

    public void CreateClone(Transform _transform, Vector3 _offset)
    {
        GameObject newClone = GameObject.Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetUpClone(_transform, cloneTimeDuration, invisibleSpeed, _offset);
        newClone.GetComponent<CloneSkillController>().FaceToEnemySize(_offset);
    }
}
