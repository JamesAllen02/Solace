using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelIncrease : MonoBehaviour
{
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    FindObjectOfType<ModeUI>().currentAbilities++;
        //    FindObjectOfType<character>().gameObject.transform.position = new Vector3(93, 1, 0);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "player")
        {
            FindObjectOfType<ModeUI>().currentAbilities++;
        }
    }
}
