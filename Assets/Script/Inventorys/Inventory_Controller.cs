using System.Collections.Generic;
using UnityEngine;


public class Inventory_Controller : MonoBehaviour
{
    private static Inventory_Controller instance;
    public static Inventory_Controller Instance { get => instance; }

    [SerializeField] private List<InventoryItem> inventory;
    [SerializeField] private Dictionary<ItemData, InventoryItem> inventoryDictionary;
    [SerializeField] private List<InventoryItem> equipment;
    [SerializeField] private Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    [SerializeField] private GameObject dropPrefab;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    private UI_ItemSlot[] itemSlot;
    private UI_EquipItemSlot[] equipmentSlot;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            Inventory_Controller.instance = this;
        }

    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        itemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipItemSlot>();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].CleanUpEquipSlot();
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlot[i].equipmentType)
                {
                    equipmentSlot[i].UpdateEquipSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].CleanUpSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            itemSlot[i].UpdateSlot(inventory[i]);
        }
    }

    public void AddItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            var newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionary.Add(_item, newItem);
        }
        UpdateInventoryUI();
    }

    private void RemoveItem(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }
        UpdateInventoryUI();
    }

    public void EquipItem(ItemData _item)
    {
        if (_item == null)
        {
            Debug.LogError("EquipItem is NULL");
            return;
        }

        var newEquipItem = _item as ItemData_Equipment;
        var newItem = new InventoryItem(newEquipItem);
        ItemData_Equipment itemToUnequip = null;

        foreach (var item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipItem.equipmentType)
            {
                itemToUnequip = item.Key;
            }
        }

        UnequipItem(itemToUnequip);

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipItem, newItem);
        newEquipItem.AddModifiers();

        RemoveItem(_item);
        UpdateInventoryUI();
    }

    public void UnequipItem(ItemData_Equipment itemToUnequip)
    {
        if (itemToUnequip != null)
        {
            AddItem(itemToUnequip);
            if (equipmentDictionary.TryGetValue(itemToUnequip, out InventoryItem value))
            {
                equipment.Remove(value);
                equipmentDictionary.Remove(itemToUnequip);
                itemToUnequip.RemoveModifiers();
            }
            UpdateInventoryUI();
        }
    }

    public void LoseAllItems()
    {
        foreach (InventoryItem item in inventory)
        {
            if (item == null)
            {
                return;
            }
            DropItem(item.itemData);
        }

        inventory.Clear();
        inventoryDictionary.Clear();
    }

     public ItemData_Equipment GetEquipment(EquipmentType type)
    {
        ItemData_Equipment equipedItem = null;
        foreach (var item in equipmentDictionary)
        {
            if (item.Key.equipmentType == type)
            {
                equipedItem = item.Key;
            }
        }
        return equipedItem;
    }

    private void DropItem(ItemData _itemData)
    {
        var playerTransfrom = Player_Manager.Instance.Player.transform;
        var instantiateDrop = Instantiate(dropPrefab, playerTransfrom.position, Quaternion.identity);
        int xRandomRange = Random.Range(-5, 5);
        int yRandomRange = Random.Range(10, 15);
        Vector2 randomVelocity = new Vector2(xRandomRange, yRandomRange);
        instantiateDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }

}
