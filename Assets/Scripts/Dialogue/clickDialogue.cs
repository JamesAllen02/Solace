using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueSymbol;

    private bool isWithin = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isWithin && !FindObjectOfType<DialogueManager>().isUp)
        {
            dialogueSymbol.SetActive(false);
            TriggerDialogue();
            //this.gameObject.SetActive(false);
            isWithin = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueSymbol.SetActive(true);
        isWithin = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueSymbol.SetActive(false);
        isWithin = false;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
