using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private TextMeshProUGUI statNameText;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if(statNameText != null)
        {
            statNameText.text = statName;
        }
    }

    void Start()
    {
        UpdateStatValue();
    }

    public void UpdateStatValue()
    {
        if(!Player_Manager.Instance.Player.TryGetComponent<Player_Stats>(out var playerStats))
        {
            return;
        }
        if (playerStats.GetType(statType) == null)
        {
            Debug.Log(statType.ToString() + "is NULL !");
            return;
        }
        statValueText.text = playerStats.GetType(statType).GetValue().ToString();
    }
}
