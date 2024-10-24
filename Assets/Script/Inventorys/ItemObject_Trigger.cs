using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject ItemObject => GetComponentInParent<ItemObject>();
    private bool canPickup = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke(nameof(PickupDelay), 2);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PickupItem(collision);
    }

    private void PickupDelay()
    {
        canPickup = true;
    }

    private void PickupItem(Collider2D collision)
    {
        if (canPickup)
        {
            if (collision.GetComponent<Player>() != null)
            {
                ItemObject.PickupItem();
                canPickup = false;
            }
        }
    }
}
