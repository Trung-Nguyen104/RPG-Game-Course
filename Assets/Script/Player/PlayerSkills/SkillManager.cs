using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance { get => instance; }

    public Dash_Skill dash { get; private set; }
    public CreateClone_Skill createClone { get; private set; }
    public ThrowSword_Skill throwSword { get; private set; }
    public Ultimate_Skill ultimateSkill { get; private set; }

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
        dash = GetComponent<Dash_Skill>();
        createClone = GetComponent<CreateClone_Skill>();
        throwSword = GetComponent<ThrowSword_Skill>();
        ultimateSkill = GetComponent<Ultimate_Skill>();
    }

}
