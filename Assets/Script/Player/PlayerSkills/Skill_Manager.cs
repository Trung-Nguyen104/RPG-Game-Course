using System;
using System.Collections.Generic;
using TMPro;
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
    public static Skill_Manager Instance {  get; private set; }

    public DashSkill Dash { get; private set; }
    public CreateCloneSkill CreateClone { get; private set; } 
    public ParrySkill Parry { get; private set; }
    public ThrowSwordSkill ThrowSword { get; private set; }
    public UltimateSkill Ultimate { get; private set; }

    public List<string> SkillNames { get; private set; }
    public TextMeshProUGUI soulsAmountText;
    public int souls;
    private float soulsAmount;
    [SerializeField] private GameObject skillTreeTab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        SkillNames = new();
        Dash = GetComponent<DashSkill>();
        CreateClone = GetComponent<CreateCloneSkill>();
        Parry = GetComponent<ParrySkill>();
        ThrowSword = GetComponent<ThrowSwordSkill>();
        Ultimate = GetComponent<UltimateSkill>();
    }

    private void Update()
    {
        if(skillTreeTab != null && skillTreeTab.activeSelf)
        {
            SoulsAmoutDisplay();
        }
        else
        {
            soulsAmount = 0;
            soulsAmountText.text = soulsAmount.ToString();
        }
    }

    private void SoulsAmoutDisplay()
    {
        if (soulsAmount < souls)
        {
            float speed = 100f + (souls - soulsAmount) * 5f; 
            soulsAmount += Time.unscaledDeltaTime * speed;

            if (soulsAmount > souls)
                soulsAmount = souls;
        }
        else
        {
            soulsAmount = souls;
        }

        soulsAmountText.text = ((int)soulsAmount).ToString("#,#");
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
        souls = _data.souls;

        foreach (var skillSaved in _data.skillBranches)
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
        _data.souls = souls;

        foreach (var skill in SkillNames)
        {
            _data.skillBranches.Add(skill);
        }
    }

    public bool CanBuy(int _price)
    {
        if (_price > souls)
        {
            return false;
        }
        souls -= _price;
        return true;
    }

}
