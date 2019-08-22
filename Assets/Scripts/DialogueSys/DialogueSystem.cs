using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    static public DialogueSystem dialogue;

    public GameObject sistemaDialogo;
    public Text textoDialogo;
    public Text npcNome;

    [Tooltip("O primeiro rosto tem que ser SEMPRE o da Lurdinha.")]
    public Image[] personagemRosto = new Image[2];

    public Opacidade opacidade = new Opacidade();

    [HideInInspector]
    public Dialogue dialogo = new Dialogue();

    private npcDialogue npcDialogo = null;

    private int proximaFala = 0;

    [Header("Escrita")] public float velocidade = 5;

    private float tempo;

    private bool escrevendo = false;

    private Coroutine corrotina;

    private void Awake()
    {
        if (dialogue != null)
        {
            Debug.LogError("Mais que um DialogueSystem");
        }

        dialogue = this;
    }

    public void IniciarConversa(npcDialogue _dialogo)
    {
        npcDialogo = _dialogo;

        sistemaDialogo.SetActive(true);

        personagemRosto[0].sprite = dialogo.personagens[0].personagem;

        int i = 0;

        while (i < dialogo.personagens.Length && dialogo.sentences[i].personagem == 0) 
        {
            i += 1;
        }

        if (i == 0) 
        {
            Debug.LogWarning("Lurdinha está falando sozinha.");
        }
        else
        {
            personagemRosto[1].sprite = dialogo.personagens[dialogo.sentences[i].personagem].personagem;
        }

        ProximaFala();
    }

    public void AcabarConversa()
    {
        npcDialogo.jaFalou = true;
        npcDialogo = null;

        sistemaDialogo.SetActive(false);
    }

    public void ProximaFala()
    {
        if (!escrevendo)
        {
            ComeçarProximaFala();
        }
        else
        {
            TerminarFala();
        }
    }

    public void ComeçarProximaFala()
    {
        for (int i = 0; i < personagemRosto.Length; i++)
        {
            personagemRosto[i].color = opacidade.Desligar();
        }

        if (proximaFala < dialogo.sentences.Length)
        {
            corrotina = StartCoroutine(Escrever());

            //Acessa o personagem que tem a próxima fala, que está registrado em dialogo.sentences, e 
            //retorna o nome do mesmo, que está registrado em dialogo.personagens
            npcNome.text = dialogo.personagens[dialogo.sentences[proximaFala].personagem].nome;

            if (dialogo.sentences[proximaFala].personagem == 0)
            {
                personagemRosto[0].color = opacidade.Ligar();

            }
            else
            {
                //Acessa o personagem que tem a próxima fala, que está registrado em dialogo.sentences, e 
                //retorna a imagem do mesmo, que está registrado em dialogo.personagens
                personagemRosto[1].sprite =
                    dialogo.personagens[dialogo.sentences[proximaFala].personagem].personagem;
                personagemRosto[1].color = opacidade.Ligar();
            }
        }
        else
        {
            proximaFala = 0;

            dialogo = null;

            AcabarConversa();
        }
    }

    public void TerminarFala()
    {
        StopCoroutine(corrotina);

        escrevendo = false;

        textoDialogo.text = dialogo.sentences[proximaFala].sentence;

        proximaFala += 1;
    }

    private IEnumerator Escrever()
    {
        escrevendo = true;

        tempo = 0;

        textoDialogo.text = "";

        while (textoDialogo.text.Length < dialogo.sentences[proximaFala].sentence.Length) 
        {
            textoDialogo.text = dialogo.sentences[proximaFala].sentence.Substring(0, Mathf.FloorToInt(tempo * velocidade));

            yield return null;

            tempo += Time.deltaTime;
        }

        proximaFala += 1;

        escrevendo = false;
    }
}