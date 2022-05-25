using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    [SerializeField] private GameObject pauseMenu;

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

    public void continueButton()
    {
        Time.timeScale = 1;
        paused = false;
    }

    private void Update()
    {
        pauseMenu.SetActive(paused);
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
