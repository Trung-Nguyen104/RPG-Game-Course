using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainSceneGame";
    [SerializeField] private Button continueBtn;

    private void Start()
    {
        if (!SaveLoad_Manager.instance.GetSavedData())
        {
            continueBtn.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Play()
    {
        SaveLoad_Manager.instance.DeleteSavedData();
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
