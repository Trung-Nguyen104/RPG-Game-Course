using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_NavigationMenu : MonoBehaviour, ISaveLoadManager
{
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private UI_VolumeSilder[] volumeSettings;

    private Canvas menuCanvas;
    private int currentTab = 0;

    private void Awake()
    {
        UpdateTabs();
        menuCanvas = GetComponent<Canvas>();
        menuCanvas.enabled = false;
    }

    private void Update()
    {
        if (Inputs.Instance.GetInputDown(InputAction.OpenMenu) || Inputs.Instance.GetInputDown(InputAction.Ecs))
        {
            menuCanvas.enabled = !menuCanvas.enabled;
            AudioManager.Instance.PlaySFX(7);
            GameManager.Instance.TimeScale(menuCanvas.isActiveAndEnabled);
        }
        HandleNavigattion();

    }

    private void HandleNavigattion()
    {
        if (!menuCanvas.isActiveAndEnabled)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousTab();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextTab();
        }
    }

    public void NextTab()
    {
        AudioManager.Instance.PlaySFX(7);
        currentTab = (currentTab + 1) % tabs.Length;
        StartCoroutine(DelayUpdateTabs(0.2f));
    }

    public void PreviousTab()
    {
        AudioManager.Instance.PlaySFX(7);
        currentTab = (currentTab - 1 + tabs.Length) % tabs.Length;
        StartCoroutine(DelayUpdateTabs(0.2f));
    }

    private IEnumerator DelayUpdateTabs(float _timeDelay)
    {
        yield return new WaitForSecondsRealtime(_timeDelay);
        UpdateTabs();
    }

    private void UpdateTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == currentTab);
        }
    }

    public void LoadGame(GameData _data)
    {
        Dictionary<string, UI_VolumeSilder> volumeLookup = volumeSettings
            .Where(volumeSetting => !string.IsNullOrEmpty(volumeSetting.pramater))
            .ToDictionary(volumeSetting => volumeSetting.pramater, volumeSetting => volumeSetting);
        Debug.Log("load volume setting");
        foreach (KeyValuePair<string, float> pair in _data.volumeSetting)
        {
            if (volumeLookup.TryGetValue(pair.Key, out var volume))
            {
                volume.SetVolumeValue(pair.Value);
            }
        }
    }

    public void SaveGame(ref GameData _data)
    {
        _data.volumeSetting.Clear();

        foreach (var _volume in volumeSettings)
        {
            _data.volumeSetting.Add(_volume.pramater, _volume.GetVolumeValue());
        }
    }
}
