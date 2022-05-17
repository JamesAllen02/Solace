using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;

    public void pause(InputAction.CallbackContext context)
    {
        if (context.started && !paused)
        {
            Time.timeScale = 0;
            paused = true;
        } else if(context.started && paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
    }

    /*
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
    */
}
