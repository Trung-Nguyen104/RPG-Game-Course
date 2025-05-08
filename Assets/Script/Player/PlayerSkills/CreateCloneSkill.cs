using UnityEngine;

public class CreateCloneSkill : Skill
{
    public int cloneAttackDamage;

    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneTimeDuration;
    [SerializeField] private float invisibleSpeed;

    public void CreateClone(Transform _transform, Vector3 _offset)
    {
        if (!skill_Unlocked)
        {
            return;
        }
        var newClone = Instantiate(clonePrefab);
        var newCloneController = newClone.GetComponent<CloneSkillController>();
        HandleCloneController(_transform, _offset, newCloneController);
    }

    private void HandleCloneController(Transform _transform, Vector3 _offset, CloneSkillController newCloneController)
    {
        newCloneController.SetUpClone(_transform, cloneTimeDuration, invisibleSpeed, _offset, cloneAttackDamage);
        newCloneController.FaceToEnemySize(_offset);
    }
}
