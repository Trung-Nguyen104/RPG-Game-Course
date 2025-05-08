using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipment;
    public List<string> skillBranches;

    public GameData()
    {
        currency = 0;
        inventory = new();
        equipment = new();
        skillBranches = new();  
    }
}
