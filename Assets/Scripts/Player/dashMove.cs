using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashMove : MonoBehaviour
{

    private Rigidbody2D rb;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool canDash = true;

    private int direction;
    private float playerLooking;
    private float playerEnergy;

    [SerializeField] private GameObject dashParticle; 


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        dashTime = startDashTime;
    }

    void Update()
    {
        // Gets the direction of the player
        playerLooking = FindObjectOfType<character>().looking;

        //Makes it so the player gains dash when grounded
        if(FindObjectOfType<character>().grounded){
            canDash = true;
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.F) && (FindObjectOfType<energyController>().energy - 1) >= 0 && canDash)
            {
                var prefab = Instantiate(dashParticle, this.transform.position, Quaternion.identity);
                Destroy(prefab, 1f);
                direction = 1;
                FindObjectOfType<energyController>().reduceEnergy(1f);
                canDash = false;
            }
        } else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            } else
            {
                dashTime -= Time.deltaTime;
            }
            if (direction == 1)
            {
                rb.velocity = Vector2.right * dashSpeed * playerLooking;
            }
        }
    }

    public void enemyCollided()
    {
        dashTime = 0;
    }

}
