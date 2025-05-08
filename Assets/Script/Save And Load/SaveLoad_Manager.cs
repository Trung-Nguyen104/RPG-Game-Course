using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveLoad_Manager : MonoBehaviour
{
    public static SaveLoad_Manager instance;

    [SerializeField] private string fileName;
    private GameData gameData;
    private List<ISaveLoadManager> saveManagers;
    private FileDataHandler fileDataHandler;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveMangers();
        LoadGame();
    }

    [ContextMenu("Delete Saved File")]
    public void DeleteSavedData()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        fileDataHandler.Delete();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.Load();
        if(this.gameData == null)
        {
            NewGame();
        }

        foreach (ISaveLoadManager saveManager in saveManagers)
        {
            saveManager.LoadGame(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (ISaveLoadManager saveManager in saveManagers)
        {
            saveManager.SaveGame(ref gameData);
        }
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveLoadManager> FindAllSaveMangers()
    {
        IEnumerable<ISaveLoadManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveLoadManager>();
        return new List<ISaveLoadManager>(saveManagers);
    }

    public bool GetSavedData() => fileDataHandler.Load() != null;
}
