using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwcicle : MonoBehaviour
{
    public float travelSpeed = 7;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(travelSpeed * Time.deltaTime * transform.localScale.x, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != 11)
        {
            Destroy(gameObject);
        }
    }
}
