using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Slider UiSlider => GetComponentInChildren<Slider>();
    private Entity_Stats EntityStats => GetComponentInParent<Entity_Stats>();
    private CanvasGroup CanvasGroup => GetComponentInParent<CanvasGroup>();

    private float healthBarActiveDuration = 3;
    private float timer;

    private void Start()
    {
        CanvasGroup.alpha = 0;
        UiSlider.maxValue = EntityStats.GetMaxHealth();
        UiSlider.value = EntityStats.currHP;
    }

    private void Update()
    {
        HandleHealthBarDisappear();
    }

    private void OnEnable()
    {
        Event_Manager.Subscribe(EventName.OnHealthChanged, UpdateHealthBar);
        Event_Manager.Subscribe(EventName.OnHealthChanged, HandleHealthBarActive);
    }

    private void OnDisable()
    {
        Event_Manager.Unsubscribe(EventName.OnHealthChanged, UpdateHealthBar);
        Event_Manager.Unsubscribe(EventName.OnHealthChanged, HandleHealthBarActive);
    }

    private void UpdateHealthBar(object _targetStats)
    {
        if (EntityStats != (Entity_Stats)_targetStats)
        {
            return;
        }
        UiSlider.maxValue = EntityStats.GetMaxHealth();
        UiSlider.value = EntityStats.currHP;
    }

    private void HandleHealthBarActive(object _targetStats)
    {
        if (EntityStats != (Entity_Stats)_targetStats)
        {
            return;
        }
        CanvasGroup.alpha = 1;
        timer = 0;
    }

    private void HandleHealthBarDisappear()
    {
        if(timer < healthBarActiveDuration)
        {
            timer += Time.deltaTime;
        }
        if (timer > healthBarActiveDuration)
        {
            CanvasGroup.alpha -= Time.deltaTime;
        }
    }
}
