using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    

    void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            paused = true;
        }
        else if (paused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            paused = false;
        }
    }
}
