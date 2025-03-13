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
}

public class Inputs : MonoBehaviour
{
    public static Inputs Instance { get => instance; }
    private static Inputs instance;
    private Dictionary<InputAction, KeyCode> inputs = new ();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            Inputs.instance = this;
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
        };
    }

    public float PlayerInputHorizontal() => Input.GetAxisRaw("Horizontal");

    public float PlayerInputVertical() => Input.GetAxisRaw("Vertical");

    public bool GetInput(InputAction action) => inputs.ContainsKey(action) && Input.GetKey(inputs[action]);

    public bool GetInputDown(InputAction action) => inputs.ContainsKey(action) && Input.GetKeyDown(inputs[action]);

    public bool GetInputUp(InputAction action) => inputs.ContainsKey(action) && Input.GetKeyUp(inputs[action]);
}
