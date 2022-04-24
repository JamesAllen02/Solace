using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeUI : MonoBehaviour
{
    public Transform[] positions;
    public Transform[] icons;
    Vector3 worldPosition;
    public Transform closestPos;

    public bool canSwap = false;


    public int currentAbilities = 1;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        closestPos = GetClosestEnemy(positions);

        for (int i = 0; i < positions.Length; i++)
        {
            if(i+1 <= currentAbilities)
            {
                // print(positions[i] + " and " + closestPos);
                if(positions[i] == closestPos)
                {
                    if (Input.GetKeyUp(KeyCode.Tab) && canSwap == true)
                    {
                        GameObject.FindObjectOfType<modeSelector>().combatMode(i+1);
                        canSwap = false;
                    }
                }
                icons[i].gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        

    }
    Transform GetClosestEnemy(Transform[] sides)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = worldPosition;
            foreach (Transform t in sides)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
}
