using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private ItemData itemData;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private void SetupVisual()
    {
        if (itemData == null)
        {
            Debug.LogWarning("ItemData is NULL");
            return;
        }
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object - " + itemData.name;
    }

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetupVisual();
    }

    public void PickupItem()
    {
        Inventory_Controller.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
