using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputAction
{
    Jump, 
    PrimaryAttack, 
    Inventory,
    Dash,
    Ultimate,
    ThorwSword,
    OpenMenu,
    Save,
    Ecs,
}

public class Inputs : MonoBehaviour
{
    public static Inputs Instance { get; private set; }

    private Dictionary<InputAction, KeyCode> inputs = new ();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        inputs = new() {
            { InputAction.Jump, KeyCode.UpArrow },
            { InputAction.PrimaryAttack, KeyCode.Q },
            { InputAction.Inventory, KeyCode.I },
            { InputAction.Dash, KeyCode.W },
            { InputAction.Ultimate, KeyCode.R },
            { InputAction.ThorwSword, KeyCode.E },
            { InputAction.OpenMenu, KeyCode.Tab},
            { InputAction.Save, KeyCode.S },
            { InputAction.Ecs, KeyCode.Escape},
        };
    }

    public float GetHorizontal() => Input.GetAxisRaw("Horizontal");

    public float GetVeritcal() => Input.GetAxisRaw("Vertical");

    public bool GetInput(InputAction action) => inputs.ContainsKey(action) && Input.GetKey(inputs[action]);

    public bool GetInputDown(InputAction action) => inputs.ContainsKey(action) && Input.GetKeyDown(inputs[action]);

    public bool GetInputUp(InputAction action) => inputs.ContainsKey(action) && Input.GetKeyUp(inputs[action]);
}
