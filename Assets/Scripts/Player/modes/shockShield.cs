using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shockShield : MonoBehaviour
{
    public GameObject shieldCircle;
    RaycastHit2D enemiesCheck;
    public float circleDistance = 2f;
    public LayerMask enemyLayer;
    public Transform enemy01;
    public bool enemyClose = false;

    private CircleCollider2D cc;
    private SpriteRenderer sr;
    public bool shieldOn = false;

    

    private void Awake()
    {
        cc = shieldCircle.GetComponent<CircleCollider2D>();
        sr = shieldCircle.GetComponent<SpriteRenderer>();

        InvokeRepeating("reduceEnergy", 1, 1);

    }

    public void shock(InputAction.CallbackContext context)
    {
        if (context.started && GetComponent<shockShield>().enabled == true)
        {

            if (FindObjectOfType<energyController>().energy >= 3)
            {
                // Checks for nearby enemies
                Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleDistance, enemyLayer);
                if (enemyColliders.Length > 0)
                {
                    enemyClose = true;
                    enemy01 = enemyColliders[0].transform;
                }
                else
                {
                    enemyClose = false;
                }

                FindObjectOfType<energyController>().reduceEnergy(3);
                foreach (Collider2D fiend in enemyColliders)
                {
                    if (fiend.GetComponent<EnemyDamageTaken>() != null)
                    {
                        fiend.GetComponent<EnemyDamageTaken>().recieveDamage(2);
                        if (this.transform.position.x < fiend.transform.position.x)
                        {
                            fiend.GetComponent<EnemyDamageTaken>().hDir = 1;
                        }
                        else if (this.transform.position.x > fiend.transform.position.x)
                        {
                            fiend.GetComponent<EnemyDamageTaken>().hDir = -1;
                        }
                    }
                }
            }
        }
    }

    public void shieldToggle(InputAction.CallbackContext context)
    {
        if (context.started && GetComponent<shockShield>().enabled == true)
        {

            if (shieldOn == false)
            {
                // FindObjectOfType<damagePlayer>().shieldOn = false;
                shieldOn = true;
                this.GetComponent<dashMove>().enabled = false;
                FindObjectOfType<character>().speed = FindObjectOfType<modeSelector>().characterSpeed / 2;
            }
            else if (shieldOn == true)
            {
                // FindObjectOfType<damagePlayer>().isMortal = true;
                shieldOn = false;
                this.GetComponent<dashMove>().enabled = true;
                FindObjectOfType<character>().speed = FindObjectOfType<modeSelector>().characterSpeed;
            }
        }
    }

    void Update()
    {
        FindObjectOfType<damagePlayer>().shieldOn = shieldOn;
        /*
        if (Input.GetKeyDown(KeyCode.Mouse1) && shieldOn == false)
        {
            // FindObjectOfType<damagePlayer>().shieldOn = false;
            shieldOn = true;
            this.GetComponent<dashMove>().enabled = false;
            FindObjectOfType<character>().speed = FindObjectOfType<modeSelector>().characterSpeed / 2;
        } else if (Input.GetKeyDown(KeyCode.Mouse1) && shieldOn == true)
        {
            // FindObjectOfType<damagePlayer>().isMortal = true;
            shieldOn = false;
            this.GetComponent<dashMove>().enabled = true;
            FindObjectOfType<character>().speed = FindObjectOfType<modeSelector>().characterSpeed;
        }*/

        if(FindObjectOfType<energyController>().energy > 0 && shieldOn == true)
        {
            cc.enabled = true;
            sr.enabled = true;
        } else
        {
            cc.enabled = false;
            sr.enabled = false;
            shieldOn = false;
            // FindObjectOfType<damagePlayer>().isMortal = true;
        }

        /*
        // Checks for nearby enemies
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), circleDistance, enemyLayer);
        if (enemyColliders.Length > 0)
        {
            enemyClose = true;
            enemy01 = enemyColliders[0].transform;
        }
        else
        {
            enemyClose = false;
        }

        // hits nearby enemies
        if (Input.GetKeyDown(KeyCode.Mouse0) && FindObjectOfType<energyController>().energy >= 3)
        {
            FindObjectOfType<energyController>().reduceEnergy(3);
            foreach (Collider2D fiend in enemyColliders)
            {
                if (fiend.GetComponent<EnemyDamageTaken>() != null)
                {
                    fiend.GetComponent<EnemyDamageTaken>().recieveDamage(2);
                    if (this.transform.position.x < fiend.transform.position.x)
                    {
                        fiend.GetComponent<EnemyDamageTaken>().hDir = 1;
                    }
                    else if (this.transform.position.x > fiend.transform.position.x)
                    {
                        fiend.GetComponent<EnemyDamageTaken>().hDir = -1;
                    }
                }
            }
        }
        */
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, circleDistance);
    }

    void reduceEnergy()
    {
        if(shieldOn == true)
        {
            FindObjectOfType<energyController>().reduceEnergy(0.5f);
        }
    }

}
