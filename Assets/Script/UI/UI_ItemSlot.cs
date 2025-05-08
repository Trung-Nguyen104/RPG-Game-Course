using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemImage.color = Color.white;
        if (item != null)
        {
            itemImage.sprite = item.itemData.icon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }

    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.itemData == null)
        {
            return;
        }
        if (item.itemData.itemType == ItemType.Equipment)
        {
            Inventory_Controller.Instance.EquipItem(item.itemData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || item.itemData == null)
        {
            return;
        }
        Debug.Log(UI_ItemTooltip.Instance);
        UI_ItemTooltip.Instance.ShowTooltip(item.itemData as ItemData_Equipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null || item.itemData == null)
        {
            return;
        }
        UI_ItemTooltip.Instance.HideTooltip();
    }
}
