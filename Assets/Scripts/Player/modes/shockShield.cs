using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        InvokeRepeating("reduceEnergy", 2, 2);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && shieldOn == false)
        {
            shieldOn = true;
        } else if (Input.GetKeyDown(KeyCode.Mouse1) && shieldOn == true)
        {
            shieldOn = false;
        }

        if(FindObjectOfType<energyController>().energy > 0 && shieldOn == true)
        {
            cc.enabled = true;
            sr.enabled = true;
        } else
        {
            cc.enabled = false;
            sr.enabled = false;
            shieldOn = false;
        }

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
                    fiend.GetComponent<EnemyDamageTaken>().recieveDamage();
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
