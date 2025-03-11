using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Slider uiSlider => GetComponentInChildren<Slider>();
    private CharCommonStats charCommonStats => GetComponentInParent<CharCommonStats>();
    private CanvasGroup canvasGroup => GetComponentInParent<CanvasGroup>();

    private float healthBarActiveDuration = 3;
    private float timer;

    private void Start()
    {
        canvasGroup.alpha = 0;
        UpdateHealthBar();
    }

    private void Update()
    {
        HandleHealthBarDisappear();
    }

    private void OnEnable()
    {
        charCommonStats.onHealthChanged += UpdateHealthBar;
        charCommonStats.onHealthChanged += HandleHealthBarActive;
    }

    private void UpdateHealthBar()
    {
        uiSlider.maxValue = charCommonStats.GetMaxHealth();
        uiSlider.value = charCommonStats.currHP;
    }

    private void HandleHealthBarActive()
    {
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
