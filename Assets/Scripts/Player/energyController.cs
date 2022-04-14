using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class energyController : MonoBehaviour
{
    public float energy = 10;
    public TextMeshProUGUI text;
    private float maxEnergy;

    void Start()
    {
        maxEnergy = energy;
    }

    // Update is called once per frame
    void Update()
    {

        text.text = "Energy: " + energy + "/10";
    }

    public void reduceEnergy(float energyReduction)
    {
        energy -= energyReduction;
    }

    public void recieveEnergy()
    {
        //print("trying to charge");
        //print(energy);
        energy++;
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }
}
