using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int souls;
    public int lostSouls;
    public string lastCheckPoint;
    public Vector2 deadthPosition;
    public List<string> equipment;
    public List<string> skillBranches;
    public SerializableDictionary<string, bool> checkPoint;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, float> volumeSetting;

    public GameData()
    {
        souls = 0;
        lostSouls = 0;
        lastCheckPoint = string.Empty;
        checkPoint = new();
        inventory = new();
        equipment = new();
        skillBranches = new();
        deadthPosition = new();
        volumeSetting = new();
    }
}
