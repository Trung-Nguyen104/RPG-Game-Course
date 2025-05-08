using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class Inventory_Controller : MonoBehaviour, ISaveLoadManager
{
    private static Inventory_Controller instance;
    public static Inventory_Controller Instance { get => instance; }

    public int currency;
    [Space]

    [SerializeField] private List<InventoryItem> inventory;
    [SerializeField] private Dictionary<ItemData, InventoryItem> inventoryDictionary;
    [SerializeField] private List<InventoryItem> equipment;
    [SerializeField] private Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    [SerializeField] private GameObject dropPrefab;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    [SerializeField] private Transform statSlotParent;

    private List<ItemData> itemDatabase;
    private UI_ItemSlot[] itemSlot;
    private UI_EquipItemSlot[] equipmentSlot;
    private UI_StatSlot[] statSlot;

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
        statSlot = statSlotParent.GetComponentsInChildren<UI_StatSlot>();
    }

    public bool CanBuy(int _price)
    {
        if (_price > currency)
        {
            return false;
        }
        currency -= _price;
        return true;
    }

    private void UpdateInventoryUI()
    {
        foreach (var slot in equipmentSlot)
        {
            slot.CleanUpEquipSlot();

            foreach (var kvp in equipmentDictionary)
            {
                if (kvp.Key.equipmentType == slot.equipmentType)
                {
                    slot.UpdateEquipSlot(kvp.Value);
                    break;
                }
            }
        }

        int displayItemCount = Mathf.Min(inventory.Count, itemSlot.Length);

        foreach (var slot in itemSlot)
        {
            slot.CleanUpSlot();
        }

        for (int i = 0; i < displayItemCount; i++)
        {
            itemSlot[i].UpdateSlot(inventory[i]);
        }

        foreach (var stat in statSlot)
        {
            stat.UpdateStatValue();
        }
    }

    public void AddItem(ItemData _item, int amount = 1)
    {
        if (_item == null || amount <= 0)
            return;

        if (inventoryDictionary.TryGetValue(_item, out InventoryItem existingItem))
        {
            existingItem.stackSize += amount;
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item)
            {
                stackSize = amount
            };
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

    public void EquipItem(ItemData _item, string _ = "")
    {
        if (_item == null)
        {
            Debug.LogError("Equip Item is NULL");
            return;
        }

        if (_item is not ItemData_Equipment newEquipItem)
        {
            Debug.LogError("Item is not an equipment type!");
            return;
        }

        ItemData_Equipment itemToUnequip = null;

        foreach (var equippedItem in equipmentDictionary.Keys)
        {
            if (equippedItem.equipmentType == newEquipItem.equipmentType)
            {
                itemToUnequip = equippedItem;
                break; 
            }
        }

        if (itemToUnequip != null)
        {
            UnequipItem(itemToUnequip);
        }

        var newItem = new InventoryItem(newEquipItem);
        equipment.Add(newItem);
        equipmentDictionary[newEquipItem] = newItem;
        newEquipItem.AddModifiers();

        RemoveItem(_item);
        UpdateInventoryUI();
    }

    public void UnequipItem(ItemData_Equipment itemToUnequip)
    {
        if (itemToUnequip == null)
        {
            Debug.LogWarning("Cannot Unequip: item null");
            return;
        }

        if (equipmentDictionary.TryGetValue(itemToUnequip, out InventoryItem equippedItem))
        {
            itemToUnequip.RemoveModifiers();
            equipment.Remove(equippedItem);
            equipmentDictionary.Remove(itemToUnequip);
        }
        AddItem(itemToUnequip);
        UpdateInventoryUI();
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
        int xRandomRange = UnityEngine.Random.Range(-5, 5);
        int yRandomRange = UnityEngine.Random.Range(10, 15);
        var randomVelocity = new Vector2(xRandomRange, yRandomRange);
        instantiateDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }

    public void LoadGame(GameData _data)
    {
        currency = _data.currency;
        Dictionary<string, ItemData> itemLookup = GetItemDatabase()
            .Where(item => item != null && !string.IsNullOrEmpty(item.itemID))
            .ToDictionary(item => item.itemID, item => item);

        LoadInventory(_data, itemLookup);
        LoadEquipment(_data, itemLookup);
    }

    private void LoadEquipment(GameData _data, Dictionary<string, ItemData> itemLookup)
    {
        foreach (string itemID in _data.equipment)
        {
            if (itemLookup.TryGetValue(itemID, out ItemData itemData))
            {
                if (itemData is ItemData_Equipment equipmentItem)
                {
                    EquipItem(equipmentItem);
                }
                else
                {
                    Debug.LogWarning($"Item {itemID} is not equipment.");
                }
            }
            else
            {
                Debug.LogWarning($"Cannot Found Equipment Item With ID: {itemID}");
            }
        }
    }

    private void LoadInventory(GameData _data, Dictionary<string, ItemData> itemLookup)
    {
        foreach (var item in _data.inventory)
        {
            if (itemLookup.TryGetValue(item.Key, out ItemData itemData))
            {
                AddItem(itemData, item.Value);
            }
            else
            {
                Debug.LogWarning($"Cannot Found Item With ID: {item.Key}");
            }
        }
    }

    public void SaveGame(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipment.Clear();

        foreach (var item in inventoryDictionary)
        {
            _data.inventory[item.Key.itemID] = item.Value.stackSize;
        }

        foreach (var equipmentItem in equipmentDictionary)
        {
            _data.equipment.Add(equipmentItem.Key.itemID);
        }

        _data.currency = currency;
    }

    private List<ItemData> GetItemDatabase()
    {
        itemDatabase = new();
        var assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Resources/Items" });

        foreach(var SOname in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOname);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDatabase.Add(itemData);
        }
        return itemDatabase;
    }
}
