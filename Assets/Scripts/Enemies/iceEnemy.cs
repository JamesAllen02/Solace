using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceEnemy : MonoBehaviour
{
    [SerializeField] private Vector2 positions = new Vector2(-3, 3);
    private Vector2 patrolPositions;

    [SerializeField] private float moveSpeed = 3;
    private int cDir = 1;
    private int direction = 1;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float checkDistance = 5;
    private Transform player;
    private bool playerClose = false;

    public int enemyBehaviour = 1;

    [SerializeField] private Animator anim;


    void Start()
    {
        patrolPositions = new Vector2(positions.x + this.transform.position.x, positions.y + this.transform.position.x);
    }


    void Update()
    {
        this.transform.localScale = new Vector3(cDir, 1, 1);
        switch (enemyBehaviour)
        {
            case 1:
                patrol();
                break;
            case 2:
                follow();
                break;
            case 3:
                // Shoot player
                break;
            case 4:
                // 
                break;
            default:
                break;
        }
    }

    // Patrol script
    private void patrol()
    {
        // Moves the enemy between patrol points
        if (direction == 1)
        {
            moveEnemy(patrolPositions.x);
            if (transform.position.x == patrolPositions.x)
            {
                cDir = -1;
                direction = -1;
            };
        }
        else if (direction == -1)
        {
            moveEnemy(patrolPositions.y);
            if (transform.position.x == patrolPositions.y)
            {
                cDir = 1;
                direction = 1;
            };
        }

        // If the enemy is outside of the patrol points, it looks towards the patrol points
        if(transform.position.x < patrolPositions.x)
        {
            // print("Too far left");
            cDir = -1;
        }
        if(patrolPositions.y < transform.position.x)
        {
            // print("Too far right");
            cDir = 1;
        }
        
    }

    // Simple function that moves it in a direction.
    void moveEnemy(float targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos, transform.position.y, 0), Time.deltaTime * moveSpeed);
    }

    // Follow and attack player
    private void follow()
    {
        // Decides the direction based on player
        if (player.transform.position.x > transform.position.x)
        {
            cDir = -1;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            cDir = 1;
        }

        // Checks if the player is in front
        Debug.DrawRay(transform.position, Vector2.right * -cDir * 1.75f, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * -cDir, 1.75f, playerLayer);
        if (hit.collider != null)
        {
            // Attacks
            anim.SetBool("attacking", true);

        } else
        {
            // Stops attacking because the player left
            anim.SetBool("attacking", false);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("iceClownRun"))
            {
                moveEnemy(player.transform.position.x);
            }
        }
    }

    private void FixedUpdate()
    {
        // Checks for player inside area
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), checkDistance, playerLayer);
        if (enemyColliders.Length > 0)
        {
            playerClose = true;
            player = enemyColliders[0].transform;

            // Creates a raycast to check if player is in enemy sight
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * -cDir, checkDistance, playerLayer);
            if (hit.collider != null)
            {
                // Attacks
                enemyBehaviour = 2;
            }
        }
        else
        {
            playerClose = false;
            enemyBehaviour = 1;
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.75f, 1, 0.5f);
        Gizmos.DrawCube(new Vector3(patrolPositions.x, this.transform.position.y, 0), new Vector3(1, 1, 1));
        Gizmos.DrawCube(new Vector3(patrolPositions.y, this.transform.position.y, 0), new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(new Vector3(positions.x + transform.position.x, this.transform.position.y, 0), new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(new Vector3(positions.y + transform.position.x, this.transform.position.y, 0), new Vector3(1, 1, 1));
        Gizmos.DrawWireSphere(transform.position, checkDistance);
    }

}