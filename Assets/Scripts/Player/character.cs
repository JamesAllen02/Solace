using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public float speed = 0.1f;
    [SerializeField] private float m_JumpForce = 400f;

    const float k_GroundedRadius = .01f;
    private Rigidbody2D rb;

    public float groundedHeight = 0.51f;
    public float checkRate = 1.0f;
    public bool grounded = false;
    public bool onWall = false;
    public LayerMask groundLayer;
    public float heightOffset = 0.25f;
    public float looking = 1;

    private float startHeight = 10000;
    public float jumpHeight = 4;
    
    public SpriteRenderer stickRender;

    private IEnumerator coroutine;

    private void Start()
    {
        // - After 0 seconds, prints "Starting 0.0"
        // - After 0 seconds, prints "Before WaitAndPrint Finishes 0.0"
        // - After 2 seconds, prints "WaitAndPrint 2.0"
        //print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine.

        coroutine = WaitAndPrint(2.0f);
        StartCoroutine(coroutine);

        //print("Before WaitAndPrint Finishes " + Time.time);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //print("WaitAndPrint " + Time.time);
        }
    }

    void Update()
    {
        GroundCheck();
        wallCheck();

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            startHeight = this.transform.position.y;
            rb.velocity = new Vector2(rb.velocity.x, m_JumpForce);

        }

        // Moving left
        if (Input.GetAxis("Horizontal") < 0)
        {
            stickRender.flipX = true;
            looking = -1;
        }
        // Moving right
        if (Input.GetAxis("Horizontal") > 0)
        {
            stickRender.flipX = false;
            looking = 1;
        }

        // Wall jump
        if (onWall && !grounded)
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0);

            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.AddForce(new Vector2(300*-looking, 600));
                StartCoroutine(waitWall());
            }

        } else if (!onWall)
        {
        }

        if (this.transform.position.y > startHeight + jumpHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            startHeight = 10000;
        }

    }

    IEnumerator waitWall()
    {
        yield return new WaitForSeconds(0.02f);

        //After we have waited 5 seconds print the time again.
        //rb.velocity = new Vector2(rb.velocity.x, 10);
    }

    void wallCheck()
    {
        if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector3.right, 0.325f, groundLayer) || Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right, 0.325f, groundLayer))
        {
            onWall = true;
        }
        else
        {
            onWall = false;
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector3.right * 0.325f, Color.yellow);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right * 0.325f, Color.yellow);
        }
    }
    

    void GroundCheck()
    {
        if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z), Vector3.down, groundedHeight + heightOffset, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * speed;
    }
}