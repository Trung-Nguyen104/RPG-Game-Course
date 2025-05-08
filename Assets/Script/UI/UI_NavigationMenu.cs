using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NavigationMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] tabs;
    
    private Canvas menuCanvas;
    private int currentTab = 0;

    private void Start()
    {
        UpdateTabs();
        menuCanvas = GetComponent<Canvas>();
        menuCanvas.enabled = false;
    }

    private void Update()
    {
        if (Inputs.Instance.GetInputDown(InputAction.OpenMenu))
        {
            menuCanvas.enabled = !menuCanvas.enabled;
            //TimeScale(menuCanvas.isActiveAndEnabled);
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
        currentTab = (currentTab + 1) % tabs.Length;
        Invoke(nameof(UpdateTabs), 0.2f);
    }

    public void PreviousTab()
    {
        currentTab = (currentTab - 1 + tabs.Length) % tabs.Length;
        Invoke(nameof(UpdateTabs), 0.2f);
    }

    private void UpdateTabs()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(i == currentTab);
        }
    }

    private void TimeScale(bool _isMenuOpen)
    {
        var timeScale = _isMenuOpen ? 0f : 1f;
        Time.timeScale = timeScale;
    }
}
