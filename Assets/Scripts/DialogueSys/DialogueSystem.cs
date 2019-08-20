using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public float needSpeed;
    private Queue<DialogueSentence> dialogueSentences;
    private DialogueSentence currentSentence;

    public Animator animator;
    public float timeleaft;
    public bool Actvate;
    public bool x2;

    public Sprite Diretor;
    public Sprite Lurdinha;
    public Sprite Drica;
    public Sprite Vazio;

    public int texto_numero;
    

	// Use this for initialization
	void Start ()
    {
        //names = new Queue<string>();
        //sentences = new Queue<string>();
        dialogueSentences = new Queue<DialogueSentence>();
        timeleaft = 0.5f;
        GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Vazio;
        texto_numero = 0;

    }

    //------------------------------------------------------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------//

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);

        //nameText.text = dialogue.name;
        //names.Clear();
        //sentences.Clear();
        dialogueSentences.Clear();
        Actvate = true;

        foreach(DialogueSentence x in dialogue.sentences)
        {
            dialogueSentences.Enqueue(x);
        }
        DisplayNextSentence();
    }

    //-----------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------------------------------------------------------------------------//

    public void DisplayNextSentence()
    {
        texto_numero += 1;
        if (SceneManager.GetActiveScene().name == "Multimeios")
        {
            if (texto_numero == 6)
            {
                GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Lurdinha;
            }   
            else
            {
                GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Drica;
            }
        }

        if (SceneManager.GetActiveScene().name == "Patio")
        {
            if (texto_numero == 2)
            {
                GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Lurdinha;
            }
            else
            {
                GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Diretor;
            }
        }

        if (dialogueSentences.Count == 0)
        {
            EndDialogue();  //Manda um Sinal para a função de finalização do dialogo;
            Time.timeScale = 1f; //Function 
            return;
        }
        currentSentence = dialogueSentences.Dequeue();
        nameText.text = currentSentence.characterName;
        string sentence = currentSentence.sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation.");
        animator.SetBool("IsOpen", false);
        Actvate = false;
        dialogueText.text = "";
        GameObject.Find("Personagem Rosto").GetComponent<Image>().sprite = Vazio;
    }

    private void Update()
    {
        timeleaft -= Time.deltaTime;

        if (Actvate == true)
        {
            if (timeleaft < 0 )
            {

                Time.timeScale = 0f; //function for pause the game

            }
        }

        if(Actvate == false)
        {

            timeleaft = 0.5f;

        }
    }
}
