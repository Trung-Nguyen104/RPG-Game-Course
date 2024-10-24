using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Handler : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;

    public InventoryItem item;
    private void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();
        inventoryCanvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
        }
    }
}
