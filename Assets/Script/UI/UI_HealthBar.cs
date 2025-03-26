using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Slider uiSlider => GetComponentInChildren<Slider>();
    private Entity_Stats entityStats => GetComponentInParent<Entity_Stats>();
    private CanvasGroup canvasGroup => GetComponentInParent<CanvasGroup>();

    private float healthBarActiveDuration = 3;
    private float timer;

    private void Start()
    {
        canvasGroup.alpha = 0;
        uiSlider.maxValue = entityStats.GetMaxHealth();
        uiSlider.value = entityStats.currHP;
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
        if (entityStats != (Entity_Stats)_targetStats)
        {
            return;
        }
        uiSlider.maxValue = entityStats.GetMaxHealth();
        uiSlider.value = entityStats.currHP;
    }

    private void HandleHealthBarActive(object _targetStats)
    {
        if (entityStats != (Entity_Stats)_targetStats)
        {
            return;
        }
        canvasGroup.alpha = 1;
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
            canvasGroup.alpha -= Time.deltaTime;
        }
    }
}
