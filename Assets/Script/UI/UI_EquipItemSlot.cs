using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_EquipItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    public EquipmentType equipmentType;
    public InventoryItem item;

    private void OnValidate()
    {
        gameObject.name = "Equipment Item Slot - " + equipmentType;
    }

    public void UpdateEquipSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemImage.color = new Color32(171,171,171,255);
        if (item != null)
        {
            itemImage.sprite = item.itemData.icon;
        }

    }

    public void CleanUpEquipSlot()
    {
        item = null;
        itemImage.sprite = null;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.itemData == null)
        {
            return;
        }

        if (item.itemData.itemType == ItemType.Equipment)
        {
            var itemToUnequip = item.itemData as ItemData_Equipment;
            Inventory_Controller.Instance.UnequipItem(itemToUnequip);
        }
    }
}
