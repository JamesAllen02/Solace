using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float height;

    public bool isGrounded;
    private bool groundCheck = true;

    public Animator camAnim;

    void Start()
    {
        height = 2.01f;
    }


    void Update()
    {
        // Looking down
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if(height >= -2)
            {
                height -= 7.5f * Time.deltaTime;
            }
        } else if(height <= 2)
        {
            height += 15f * Time.deltaTime;
        }

        transform.localPosition = new Vector3(0, height, 0);


        // Landing code logic
        isGrounded = FindObjectOfType<character>().grounded;
        if(isGrounded == true && groundCheck == true)
        {
            camAnim.SetTrigger("landed");
            groundCheck = false;
        }
        if(isGrounded == false)
        {
            groundCheck = true;
        }
    }

    public void gotHit()
    {
        camAnim.SetTrigger("hitShake");
    }

}
