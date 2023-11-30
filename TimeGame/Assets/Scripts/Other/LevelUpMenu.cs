using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject LevelUpPanel;
    public ForkHit fork;

    public void LevelUp() {
        Time.timeScale = 0;
        LevelUpPanel.SetActive(true);
        UIPanel.SetActive(false);
    }

    public void AddHealth() {
        GameObject.Find("Player").GetComponent<PlayerControls>().AddMaxHP(1);
        Time.timeScale = 1;
        LevelUpPanel.SetActive(false);
        UIPanel.SetActive(true);
    }

    public void AddDamage() {
        fork.IncrementDamage();
        Time.timeScale = 1;
        LevelUpPanel.SetActive(false);
        UIPanel.SetActive(true);
    }
    
}
