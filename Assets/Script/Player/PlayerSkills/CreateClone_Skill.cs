using UnityEngine;

public class CreateClone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneTimeDuration;
    [SerializeField] private float invisibleSpeed;

    public void CreateClone(Transform _transform, Vector3 _offset)
    {
        GameObject newClone = GameObject.Instantiate(clonePrefab);
        newClone.GetComponent<Clone_Skill_Controller>().SetUpClone(_transform, cloneTimeDuration, invisibleSpeed, _offset);
        newClone.GetComponent<Clone_Skill_Controller>().FaceToEnemySize(_offset);
    }
}
