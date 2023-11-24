using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject InfoPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            UIPanel.SetActive(false);
        }    
    }

    public void Resume() {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    public void Info() {
        if (InfoPanel.activeSelf) { InfoPanel.SetActive(false); }
        else { InfoPanel.SetActive(true); }
    }

    public void Exit() {
        Application.Quit();
    }
}
