using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour
{
    public bool unlocked = false;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLock;
    [SerializeField] private Image skillImage;
    [SerializeField] private SkillTypes skillType;
    [SerializeField] private string upgradeName = string.Empty;
    [SerializeField] private int skillPrice;

    private void Start()
    {
        if (unlocked)
        {
            return;
        }
        var button = GetComponentInChildren<Button>();
        skillImage = button.GetComponent<Image>();
        skillImage.color = new Color32(70, 70, 70, 255);
        button.onClick.AddListener(() => SkillUnclockCheck());
    }

    private void OnEnable()
    {
        foreach(var savedSkill in Skill_Manager.Instance.SkillNames)
        {
            var skillName = skillType + "_" + upgradeName;
            if (savedSkill != null && savedSkill == skillName)
            {
                unlocked = true;
            }
        }
    }

    public void SkillUnclockCheck()
    {
        if (!Inventory_Controller.Instance.CanBuy(skillPrice))
        {
            return;
        }

        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                return;
            }
        }

        for (int i = 0; i < shouldBeLock.Length; i++)
        {
            if (shouldBeLock[i].unlocked == true)
            {
                return;
            }
        }

        UnclockSkill();
    }

    private void UnclockSkill()
    {
        unlocked = true;
        skillImage.color = Color.white;
        Debug.Log(skillType + "_" + upgradeName);
        Skill_Manager.Instance.OnUnlockedSkill(skillType, upgradeName);
    }
}
