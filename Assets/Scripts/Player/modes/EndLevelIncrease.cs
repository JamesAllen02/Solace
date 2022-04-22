using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelIncrease : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "player")
        {
            FindObjectOfType<ModeUI>().currentAbilities++;
        }
    }
}
