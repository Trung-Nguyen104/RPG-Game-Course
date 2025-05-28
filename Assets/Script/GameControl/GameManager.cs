using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveLoadManager
{
    public static GameManager Instance { get; private set; }
    public string LastCheckPointID { get; set; }
    public  Vector2 DeathPosition { get; set; }
    public int LostSouls { get; set; }

    [SerializeField] private CheckPointController[] checkPoints;
    [SerializeField] private GameObject graveYardPrefabs;
    [SerializeField] private GameObject afterImagePrefabs;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
        checkPoints = FindObjectsOfType<CheckPointController>();
    }

    public void RestartGame()
    {
        SaveLoadManager.Instance.SaveGame();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadGame(GameData _data)
    {
        LoadGraveYard(_data);
        LoadCheckPoint(_data);
    }

    public void SaveGame(ref GameData _data)
    {
        _data.lostSouls = LostSouls;
        _data.deadthPosition = DeathPosition;
        _data.lastCheckPoint = LastCheckPointID;

        _data.checkPoint.Clear();
        foreach(var _checkPoint in checkPoints)
        {
            _data.checkPoint.Add(_checkPoint.ID, _checkPoint.actived);
        }
    }

    private void LoadCheckPoint(GameData _data)
    {
        foreach (var _checkPoint in checkPoints)
        {
            if (_data.checkPoint.TryGetValue(_checkPoint.ID, out bool isActive) && isActive)
            {
                _checkPoint.SetCheckPoint();
            }
            if (_data.lastCheckPoint == _checkPoint.ID)
            {
                Player_Manager.Instance.Player.transform.position = _checkPoint.transform.position;
            }
        }
    }

    private void LoadGraveYard(GameData _data)
    {
        if(_data.deadthPosition != Vector2.zero)
        {
            Debug.Log("Load GraveYard");
            var graveYard = Instantiate(graveYardPrefabs, 
                new Vector2(_data.deadthPosition.x, _data.deadthPosition.y + -0.45f),
                Quaternion.identity);
            graveYard.GetComponent<GraveyardController>().SetLostSouls(_data.lostSouls);
            LostSouls = 0;
        }
    }

    public void TimeScale(bool _pauseGame)
    {
        var timeScale = _pauseGame ? 0 : 1;
        Time.timeScale = timeScale;
    }

    public GameObject GetAfterImagePrfabs() => Instantiate(afterImagePrefabs);
}
