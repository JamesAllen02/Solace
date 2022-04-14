using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject mainCam;
    
    void Update()
    {
        this.transform.position = new Vector3(mainCam.transform.position.x, this.transform.position.y, 0);
    }
}
