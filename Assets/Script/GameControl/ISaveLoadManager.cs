using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoadManager 
{
    void LoadGame(GameData _data);
    void SaveGame(ref GameData _data);
}
