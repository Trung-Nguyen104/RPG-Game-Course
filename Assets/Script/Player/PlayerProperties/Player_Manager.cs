using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager Instance { get; private set; }
    public Player Player { get; private set; }
    public Player_Stats Stats { get; private set; }

    private void Awake()
    {
        Player = GameObject.Find("PlayerManager/Player").GetComponent<Player>();
        Stats = GameObject.Find("PlayerManager/Player").GetComponent<Player_Stats>();

        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
}
