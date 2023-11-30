using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPlayer : MonoBehaviour
{

    private static int XP_max = 75;
    private static float XP = 0;
    private static int level = 0;

    public float XPValue { get => XP; set => XP = value; }
    public int Level { get => level; set => level = value; }
    public int XPMax { get => XP_max; set => XP_max = value; }

    public void AddXP(float value)
    {
        XP += value;
        if (XP >= XP_max)
        {
            level += 1;
            XP = 0;
            GameObject.Find("GameUI").GetComponent<LevelUpMenu>().LevelUp();
        }
    }
}
