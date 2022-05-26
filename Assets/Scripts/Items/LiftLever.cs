using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftLever : MonoBehaviour
{
    [SerializeField] private Lift liftObject;

    [SerializeField] private GameObject onSprite;
    [SerializeField] private GameObject offSprite;

    private bool canHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onSprite.SetActive(canHit);
        offSprite.SetActive(!canHit);

        if(liftObject.moveUp == true && !liftObject.onBottom || liftObject.onTop && !liftObject.onBottom)
        {
            canHit = true;
        }
        else if(liftObject.moveUp == false)
        {
            canHit = false;
        }
    }

    public void trigger()
    {
        liftObject.moveUp = false;
        liftObject.canMove = true;
        liftObject.isMoving = true;
    }
}
