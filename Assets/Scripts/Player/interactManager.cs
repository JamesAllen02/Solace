using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactManager : MonoBehaviour
{
    public void interacting(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<clickDialogue>() != null)
                {
                    go.GetComponent<clickDialogue>().startDialogue();
                }
                if (go.GetComponent<checkpoint>() != null)
                {
                    go.GetComponent<checkpoint>().sitDown();
                }
            }
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
