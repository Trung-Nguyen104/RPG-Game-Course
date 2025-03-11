using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance; }
    public Player Player {  get; private set; }
    private PlayerManager() {}
    private void Awake()
    {
        Player = GameObject.Find("PlayerManager/Player").GetComponent<Player>();
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            PlayerManager.instance = this;
        }
    }
}
