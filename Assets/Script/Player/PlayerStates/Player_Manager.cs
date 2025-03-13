using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    private static Player_Manager instance;
    public static Player_Manager Instance { get => instance; }
    public Player Player {  get; private set; }

    private void Awake()
    {
        Player = GameObject.Find("PlayerManager/Player").GetComponent<Player>();
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            Player_Manager.instance = this;
        }
    }
}
