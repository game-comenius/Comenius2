using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogue : MonoBehaviour
{
    public Dialogue[] dialogue;
    private int DialogueCount = 0;
    public Player player;
    

    private void Start()
    {

    }

    //diálogo por movimentação
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player" && player.dialogue == null)
    //    {
    //        Debug.Log("GG");
    //        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue[DialogueCount]);
    //        if (dialogue[DialogueCount++] != null) {
    //            DialogueCount++;
    //        }
            
    //    }
    //}

    //diálogo por clique
    public void OnMouseUp()
    {
        Debug.Log(DialogueCount);
        if (DialogueCount < dialogue.Length){
            FindObjectOfType<DialogueSystem>().StartDialogue(dialogue[DialogueCount]);
            DialogueCount++;
        }
        else
        {
            FindObjectOfType<DialogueSystem>().StartDialogue(dialogue[DialogueCount - 1]);
        }
    }
}
