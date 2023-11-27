using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textNextLevel;
    [SerializeField] private Image bar_fill;

    private int level;
    private float XP;

    private void Update() {

        XP = player.GetComponent<XPPlayer>().XPValue;
        level = player.GetComponent<XPPlayer>().Level;

        textLevel.text = level.ToString();
        textNextLevel.text = (level + 1).ToString();
        UpdateXPBar();
    }

    private void UpdateXPBar()
    {
        float fillAmount = XP /  player.GetComponent<XPPlayer>().XPMax;
        bar_fill.fillAmount = fillAmount;
    }
}
