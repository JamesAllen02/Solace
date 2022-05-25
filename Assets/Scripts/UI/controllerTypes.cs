using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class controllerTypes : MonoBehaviour
{
    public int currentController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void change()
    {
        print("");
    }

    public void setPS4()
    {
        currentController = 1;
    }

    public void setKeyboard()
    {
        currentController = 2;
    }

}
