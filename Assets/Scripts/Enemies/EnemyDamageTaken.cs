using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaken : MonoBehaviour
{
    public Renderer hpRenderer;
    private float startingHealth;

    public float hp = 10;

    public bool isMortal = true;
    public bool isDead = false;

    public int hDir;
    public Rigidbody2D rb;

    public GameObject healingHeart;

    //public Animator deathAnim;

    public GameObject enemyToDie;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = hp;
    }

    // Update is called once per frame
    void Update()
    {
        hpRenderer.material.SetFloat("_Health", hp / startingHealth);
    }

    public void recieveDamage()
    {
        if (hp > 0)
        {
            hp--;
            rb.velocity = new Vector2(3 * hDir, 3);

            if (hp == 0)
            {
                //deathAnim.SetBool("isDead", true);
                var prefab = Instantiate(healingHeart, this.transform.position, this.transform.rotation);
                isDead = true;
                Destroy(enemyToDie.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            recieveDamage();
        }
    }

}
