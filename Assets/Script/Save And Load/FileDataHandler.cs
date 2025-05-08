using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = string.Empty;
    private string dataFileName = string.Empty;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataStore = JsonUtility.ToJson(_data, true);

            using FileStream fileStream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(fileStream);
            writer.Write(dataStore);
        }

        catch (Exception ex)
        {
            Debug.LogError("Erorr on trying to save data on file: " + fullPath + "\n" + ex);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = string.Empty;

                using FileStream fileStream = new(fullPath, FileMode.Open);
                using StreamReader reader = new(fileStream);
                dataToLoad = reader.ReadToEnd();

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception ex)
            {
                Debug.LogError("Erorr on trying to load data on file: " + fullPath + "\n" + ex);
            }
        }
        return loadData;
    }
}
