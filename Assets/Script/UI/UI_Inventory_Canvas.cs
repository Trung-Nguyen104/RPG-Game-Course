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
            TimeScale(inventoryCanvas.isActiveAndEnabled);
        }
    }

    private void TimeScale(bool _isInventoryOpen)
    {
        if (_isInventoryOpen)
        {
            Time.timeScale = 0f; 
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
