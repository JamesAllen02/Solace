using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeSelector : MonoBehaviour
{

    public GameObject uiSelect;
    public Animator uiAnim;

    private floatingOrb ab1;
    private parryAttack ab2;
    private shockShield ab3;
    private heal ab4;

    public GameObject shieldCircle;
    public GameObject flyingOrb;
    public GameObject floatDistance;
    public GameObject shieldBox;

    public damagePlayer dP;

    private void Start()
    {
        ab1 = this.transform.GetComponent<floatingOrb>();
        ab2 = this.transform.GetComponent<parryAttack>();
        ab3 = this.transform.GetComponent<shockShield>();
        ab4 = this.transform.GetComponent<heal>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) /*&& Time.timeScale != 0.05f*/)
        {
            // Time.timeScale = 0.05f;
            uiSelect.GetComponent<ModeUI>().canSwap = true;
            uiAnim.SetBool("isOn", true);

        } else if(Input.GetKeyUp(KeyCode.Tab) /*&& Time.timeScale == 0.05f*/)
        {
            Time.timeScale = 1f;
            uiAnim.SetBool("isOn", false);
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 0.05f;
        } else
        {
            //Time.timeScale = 1f;
        }
    }

    public void combatMode(int number)
    {
        disableEverything();
        if (number == 3)
        {
            print("Floating mode");
            ab1.enabled = true;
            floatDistance.SetActive(true);
            flyingOrb.SetActive(true);
        }
        else if (number == 1)
        {
            ab2.enabled = true;
            print("Combat mode");
            //shieldBox.SetActive(true);

        }
        else if (number == 2)
        {
            print("Shield mode");
            ab3.enabled = true;
            shieldCircle.SetActive(true);
            dP.isMortal = false;
        }
        else if (number == 4)
        {
            print("Health mode");
            InvokeRepeating("healing", 4f, 4f);
            InvokeRepeating("replenish", 4f, 4f);
            ab4.enabled = true;
        }
    }

    public void replenish(){
        FindObjectOfType<energyController>().recieveEnergy();
    }

    public void healing()
    {
        FindObjectOfType<damagePlayer>().recieveHealth();
    }

    public void disableEverything()
    {
        ab1.enabled = false;
        ab2.enabled = false;
        ab3.enabled = false;
        ab4.enabled = false;

        dP.isMortal = true;
        ab3.shieldOn = false;

        shieldCircle.SetActive(false);
        flyingOrb.SetActive(false);
        floatDistance.SetActive(false);
        shieldBox.SetActive(false);

        CancelInvoke("healing");
    }

}
