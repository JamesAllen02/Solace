using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkpoint : MonoBehaviour
{
    public clickDialogue checkDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        if (checkDialogue.isClose && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<damagePlayer>().hp = FindObjectOfType<damagePlayer>().maxHp;
            FindObjectOfType<energyController>().energy = FindObjectOfType<energyController>().maxEnergy;
            FindObjectOfType<character>().transform.GetChild(0).GetComponent<Animator>().SetBool("sitting", true);
        }
        if (!FindObjectOfType<DialogueManager>().isUp)
        {
            FindObjectOfType<character>().transform.GetChild(0).GetComponent<Animator>().SetBool("sitting", false);
        }
    }*/

    public void sitDown()
    {
        if (checkDialogue.isClose)
        {
            FindObjectOfType<damagePlayer>().hp = FindObjectOfType<damagePlayer>().maxHp;
            FindObjectOfType<energyController>().energy = FindObjectOfType<energyController>().maxEnergy;
            FindObjectOfType<character>().transform.GetChild(0).GetComponent<Animator>().SetBool("sitting", true);
            FindObjectOfType<checkSaver>().lastCheckPoint = FindObjectOfType<character>().transform.position;
            FindObjectOfType<checkSaver>().lastCheckScene = SceneManager.GetActiveScene().buildIndex;
        }
        if (!FindObjectOfType<DialogueManager>().isUp)
        {
            FindObjectOfType<character>().transform.GetChild(0).GetComponent<Animator>().SetBool("sitting", false);
        }
    }

}
