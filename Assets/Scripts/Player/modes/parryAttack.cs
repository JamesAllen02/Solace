using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryAttack : MonoBehaviour
{
    public GameObject shield;

    [SerializeField] private float cdTime = 0.5f;
    [SerializeField] private float upTime = 0.5f;

    private float coolDown;

    private bool cooldown = false;

    public Vector2 attackArea = new Vector2(0.6f, -0.15f);
    public float attackSize;

    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = cdTime + upTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(this.transform.position.x, this.transform.position.y) + attackArea, attackSize, enemyLayer);

        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldown == false)
        {
            cooldown = true;
            shield.SetActive(true);
            Invoke("closeShield", upTime);
            Invoke("endCool", coolDown);

            foreach (Collider2D fiend in enemyColliders)
            {
                if(fiend.GetComponent<EnemyDamageTaken>() != null)
                {
                    fiend.GetComponent<EnemyDamageTaken>().recieveDamage();
                    FindObjectOfType<energyController>().recieveEnergy();
                    if(this.transform.position.x < fiend.transform.position.x)
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

        if(this.transform.GetComponent<character>().looking == -1)
        {
            shield.transform.localPosition = new Vector2(-0.25f, 0);
            attackArea = new Vector2(-0.6f, -0.15f);
        } 
        else
        {
            shield.transform.localPosition = new Vector2(0.25f, 0);
            attackArea = new Vector2(0.6f, -0.15f);
        }

    }
    public void closeShield()
    {
        shield.SetActive(false);
    }

    public void endCool()
    {
        cooldown = false;
    }
}
