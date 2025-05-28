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
        if (!SaveLoadManager.Instance.GetSavedData())
        {
            continueBtn.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        StartCoroutine(LoadSceneAfter(1.5f));
    }

    public void Play()
    {
        SaveLoadManager.Instance.DeleteSavedData();
        StartCoroutine(LoadSceneAfter(1.5f));
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneAfter(float _second)
    {
        UI_FadeSrceen.Instance.FadeOut();
        yield return new WaitForSeconds(_second);
        SceneManager.LoadScene(sceneName);
    }
}
