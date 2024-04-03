using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance { get => instance; }

    public DashSkill dash { get; private set; }
    public CreateCloneSkill createClone { get; private set; }
    public ThrowSwordSkill throwSword { get; private set; }
    public UltimateSkill ultimateSkill { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            SkillManager.instance = this;
        }
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        createClone = GetComponent<CreateCloneSkill>();
        throwSword = GetComponent<ThrowSwordSkill>();
        ultimateSkill = GetComponent<UltimateSkill>();
    }

}
