using System;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTypes
{
    None,
    Dash,
    CreateClone,
    Parry,
    ThrowSword,
    Ultimate,
}

public class Skill_Manager : MonoBehaviour, ISaveLoadManager
{
    private static Skill_Manager instance;
    public static Skill_Manager Instance { get => instance; }

    public DashSkill Dash { get; private set; }
    public CreateCloneSkill CreateClone { get; private set; } 
    public ParrySkill Parry { get; private set; }
    public ThrowSwordSkill ThrowSword { get; private set; }
    public UltimateSkill Ultimate { get; private set; }

    public List<string> SkillNames { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            Skill_Manager.instance = this;
        }
    }

    private void Start()
    {
        SkillNames = new();
        Dash = GetComponent<DashSkill>();
        CreateClone = GetComponent<CreateCloneSkill>();
        Parry = GetComponent<ParrySkill>();
        ThrowSword = GetComponent<ThrowSwordSkill>();
        Ultimate = GetComponent<UltimateSkill>();
    }

    private void Update()
    {
        var inventory = Inventory_Controller.Instance;
        inventory.textCurrency.text = inventory.currency.ToString("#,#");
    }

    public void OnUnlockedSkill(SkillTypes _skill, string _upgradeName)
    {
        SkillNames.Add(_skill + "_" + _upgradeName);
        switch (_skill)
        {
            case SkillTypes.Dash:
                Dash.OnUnlockedSkill(_upgradeName);
                break;
            case SkillTypes.CreateClone:
                CreateClone.OnUnlockedSkill(_upgradeName);
                break;
            case SkillTypes.Parry:
                Parry.OnUnlockedSkill(_upgradeName);
                break;
            case SkillTypes.ThrowSword:
                ThrowSword.OnUnlockedSkill(_upgradeName);
                break;
            case SkillTypes.Ultimate:
                Ultimate.OnUnlockedSkill(_upgradeName);
                break;
        }
    }

    public void LoadGame(GameData _data)
    {
        foreach(var skillSaved in _data.skillBranches)
        {
            string[] parts = skillSaved.Split('_');
            if (Enum.TryParse(parts[0], out SkillTypes _skillType))
            {
                string _upgradeName = parts.Length > 1 ? parts[1] : "";
                OnUnlockedSkill(_skillType, _upgradeName);
            }
        }
    }

    public void SaveGame(ref GameData _data)
    {
        _data.skillBranches.Clear();
        foreach(var skill in SkillNames)
        {
            _data.skillBranches.Add(skill);
        }
    }
}
