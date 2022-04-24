using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class damagePlayer : MonoBehaviour
{
    public float hp = 10;
    public TextMeshProUGUI text;
    private float maxHp;
    private Rigidbody2D rb;
    public GameObject hitEffect;

    public Animator deathAnim;
    public bool isDead = false;
    public bool isMortal = true;

    public healthBar hpBar;
    private dashMove dashScript;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;
        rb = this.GetComponent<Rigidbody2D>();
        dashScript = FindObjectOfType<dashMove>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.barSlider.value = hp;

        text.text = "HP: " + hp + "/" + maxHp;

        if (isDead && Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(FindObjectOfType<dontDestroy>().gameObject);
            SceneManager.LoadScene(1);
            isDead = false;
        }

        

    }

    public void recieveDamage()
    {
        if (hp > 0 && isMortal == true)
        {

            hp--;
            isMortal = false;
            Invoke("becomeMortal", 1f);
            FindObjectOfType<CameraScript>().gotHit();

            var prefab = Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, -5), transform.rotation);
            Destroy(prefab, 4);

            if(dashScript.dashTime < dashScript.startDashTime)
            {
                dashScript.enemyCollided();
            }

            if (hp == 0)
            {
                deathAnim.SetBool("isDead", true);
                isDead = true;
            }
        }


    }
    public void recieveHealth()
    {
        hp++;
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 14 || collision.gameObject.layer == 11)
        {
            recieveDamage();
            if (this.transform.position.x < collision.transform.position.x)
            {
                rb.velocity = new Vector2(-6, 6);
            }
            else if (this.transform.position.x > collision.transform.position.x)
            {
                rb.velocity = new Vector2(6, 6);
            }
        }
    }

    void becomeMortal()
    {
        isMortal = true;
    }
}
