using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPPlayer : MonoBehaviour
{
    private float XP;
    private float level;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        XP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddXP(float value)
    {
        XP += value;
        Debug.Log("Gained XP: " + value + " total: " + XP);
        if (XP > 100)
        {
            Debug.Log("LevelUp!");
        }
    }



}
