using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Slider sliderHealthBar;
    [SerializeField] private Slider sliderDashCoolDown;

    private Player_Stats playerStats;

    private void Start()
    {
        Event_Manager.Subscribe(EventName.OnHealthChanged, UpdateHealthBar);

        playerStats = Player_Manager.Instance.Stats;

        sliderDashCoolDown.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Skill_Manager.Instance.Dash.skill_Unlocked)
        {
            sliderDashCoolDown.gameObject.SetActive(true);
        }
        UpdateDashCoolDown();
    }

    private void UpdateHealthBar(object _targetStats)
    {
        if (playerStats != (Entity_Stats)_targetStats)
        {
            return;
        }
        sliderHealthBar.maxValue = playerStats.GetMaxHealth();
        sliderHealthBar.value = playerStats.currHP;
    }

    private void UpdateDashCoolDown()
    {
        var dashSkill = Skill_Manager.Instance.Dash;
        if (dashSkill.coolDownTimer <= 0)
        {
            sliderDashCoolDown.maxValue = dashSkill.coolDown;
            sliderDashCoolDown.value = dashSkill.coolDown;
        }
        else
        {
            sliderDashCoolDown.value = (dashSkill.coolDownTimer - dashSkill.coolDown) * -1;
        }
    }
}
