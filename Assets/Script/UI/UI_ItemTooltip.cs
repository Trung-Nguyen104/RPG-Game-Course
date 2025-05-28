using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemTooltip : MonoBehaviour
{
    public static UI_ItemTooltip Instance;

    [SerializeField] private RectTransform tooltipPanel;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private Canvas canvas;
    private Vector2 offset = new(15f, -15f);

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        canvas = tooltipPanel.GetComponentInParent<Canvas>();
        HideTooltip();
    }

    void Update()
    {
        if (tooltipPanel.gameObject.activeSelf)
        {
            Vector2 position = Input.mousePosition;
            tooltipPanel.position = GetTooltipPosition(position);
        }
    }

    private Vector2 GetTooltipPosition(Vector2 mousePosition)
    {
        Vector2 newPos = mousePosition + offset;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 tooltipSize = tooltipPanel.sizeDelta;
        float pivotX = 1f, pivotY = 1f;

        if (newPos.x + tooltipSize.x > screenWidth)
        {
            newPos.x = mousePosition.x - tooltipSize.x - offset.x;
            pivotX = 0f;
        }

        if (newPos.y - tooltipSize.y < 0)
        {
            newPos.y = mousePosition.y + tooltipSize.y - offset.y;
            pivotY = 0f;
        }

        tooltipPanel.pivot = new Vector2(pivotX, pivotY);
        return newPos;
    }

    public void ShowTooltip(ItemData_Equipment _data)
    {
        itemNameText.text = _data.name;
        itemTypeText.text = _data.itemType.ToString();
        itemDescription.text = _data.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideTooltip() => gameObject.SetActive(false);
}
