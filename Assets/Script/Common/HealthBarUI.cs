using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Slider uiSlider => GetComponentInChildren<Slider>();
    private CharCommonStats charCommonStats => GetComponentInParent<CharCommonStats>();

    private void Start()
    {
        UpdateHealthBar();
        charCommonStats.onHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        uiSlider.maxValue = charCommonStats.SetUpCurrentHealth();
        uiSlider.value = charCommonStats.currHP;
    }

    private void OnDisable()
    {
        charCommonStats.onHealthChanged -= UpdateHealthBar;
    }
}
