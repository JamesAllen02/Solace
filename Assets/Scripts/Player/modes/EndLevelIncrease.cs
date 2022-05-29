using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelIncrease : MonoBehaviour
{
    [SerializeField] private Animator fade;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(delayNextLevel());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.name == "player")
        {
            StartCoroutine(delayNextLevel());
        }
    }

    IEnumerator delayNextLevel()
    {
        fade = FindObjectOfType<thisIsSoICanFindIt>().gameObject.GetComponent<Animator>();
        fade.SetTrigger("fade");
        // Animate screen dark
        yield return new WaitForSeconds(1f);
        FindObjectOfType<character>().gameObject.transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<ModeUI>().currentAbilities = SceneManager.GetActiveScene().buildIndex;

    }

}
