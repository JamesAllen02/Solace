using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashMove : MonoBehaviour
{

    private Rigidbody2D rb;

    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    private bool canDash = true;
    float timer = 0;
    [SerializeField] private float coolDownTime = 0.5f;
    public SpriteRenderer dashMeter;

    private int direction;
    private float playerLooking;
    private float playerEnergy;

    [SerializeField] private GameObject dashParticle;

    public Animator guardianAnim;
    public Animator dashAnim;
    public bool isDashing = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        timer = coolDownTime;
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

        // print(timer + " " + coolDownTime);

        if(timer > coolDownTime)
        {
            // Can dash now
            dashAnim.SetBool("dash", false);
        }

        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.F) && canDash && timer > coolDownTime && FindObjectOfType<PauseMenu>().paused == false && !FindObjectOfType<character>().isSitting || Input.GetKeyDown(KeyCode.LeftShift) && canDash && timer > coolDownTime && FindObjectOfType<PauseMenu>().paused == false && !FindObjectOfType<character>().isSitting)
            {
                // start Dash
                var prefab = Instantiate(dashParticle, this.transform.position, Quaternion.identity);
                Destroy(prefab, 1f);
                direction = 1;
                // FindObjectOfType<energyController>().reduceEnergy(1f);
                canDash = false;
                timer = 0;
                guardianAnim.SetBool("isDashing", true);
                dashAnim.SetBool("dash", true);
                isDashing = true;
            }
        } else
        {
            if(dashTime <= 0)
            {
                // stop Dash
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                guardianAnim.SetBool("isDashing", false);
                isDashing = false;
            } else
            {
                // During dash?
                dashTime -= Time.deltaTime;
            }
            if (direction == 1)
            {
                rb.velocity = Vector2.right * dashSpeed * playerLooking;
            }
        }

        timer += Time.deltaTime;

        // calculate time left

        dashMeter.material.SetFloat("_Health", timer / coolDownTime);



    }

    public void enemyCollided()
    {
        dashTime = 0;
        rb.velocity = new Vector2(0, 0);
    }

}
