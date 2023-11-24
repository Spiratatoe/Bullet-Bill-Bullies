using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpace : MonoBehaviour
{
    public LevelLoader loader;

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            loader.LoadNextLevel();
        }
    }
}
