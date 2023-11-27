using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPlayer : MonoBehaviour
{
    public ForkHit fork;

    private int XP_max = 75;
    private float XP;
    private int level;

    public float XPValue { get => XP; set => XP = value; }
    public int Level { get => level; set => level = value; }
    public int XPMax { get => XP_max; set => XP_max = value; }

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        XP = 0;
    }

    public void AddXP(float value)
    {
        XP += value;
        Debug.Log("Gained XP: " + value + " total: " + XP);
        if (XP >= XP_max)
        {
            level += 1;
            // fork.IncrementDamage();
            // gameObject.GetComponent<PlayerControls>().AddMaxHP(1);
            XP = 0;
        }
    }
}
