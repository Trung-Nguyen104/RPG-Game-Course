using UnityEngine;

public class UI_Inventory_Canvas : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;

    private void Start()
    {
        inventoryCanvas = GetComponent<Canvas>();
        inventoryCanvas.enabled = false;
    }

    private void Update()
    {
        if (Inputs.Instance == null) { Debug.Log("null"); }
        if (Inputs.Instance.GetInputDown(InputAction.Inventory))
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
        }
    }
}
