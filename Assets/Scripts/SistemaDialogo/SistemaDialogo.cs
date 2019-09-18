using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameComenius.Dialogo;

public class SistemaDialogo : MonoBehaviour
{
    static public SistemaDialogo sistemaDialogo;

    #region Referências para os GameObject na UI
    [SerializeField] private GameObject sistemaDialogoUI;

    [SerializeField] private Text textoDialogo;

    [SerializeField] private Text npcNome;

    [SerializeField] private Button botao;

    [SerializeField] private Dropdown dropdown;

    [Tooltip("O primeiro rosto tem que ser SEMPRE o da Lurdinha.")]
    public Image[] personagemRosto = new Image[2];
    #endregion

    #region Animações
    public Opacidade opacidade = new Opacidade();

    #region Escrita
    [Header("Escrita")] [SerializeField] private float velocidade = 20f;

    private float tempo;

    private bool escrevendo = false;

    private Coroutine corrotina;
    #endregion

    #endregion

    #region Variaveis
    public Dialogo dialogo; //alterar para se passar um nódulo de cada vez

    private NpcDialogo npcDialogo = null;

    private Falador faladorAtual = new Falador();

    private Fala falaAtual = new Fala();

    private int proximaFala = 0;

    private int nodulo = 0;

    private int dropdownIndex;
    #endregion

    private void Awake()
    {
        sistemaDialogo = this;
    }

    private void Start()
    {
        botao.onClick.AddListener(() => { ComecarProximaFala(); });
    }

    public void ComecarDialogo(Dialogo _dialogo, NpcDialogo _npcDialogoQA)
    {
        if (npcDialogo == null) 
        {
            GameManager.uiSendoUsada = true;

            dialogo = _dialogo;
            npcDialogo = _npcDialogoQA;

            InicializarDialogo();
        }
    }

    private void InicializarDialogo()
    {
        sistemaDialogoUI.SetActive(true);

        proximaFala = 0;

        nodulo = 0;

        escrevendo = false;

        personagemRosto[0].sprite = Falador.BuscarPolaroideNosAssets(Personagens.Lurdinha, Expressao.Serio).personagem;

        for (int i = 0; i < dialogo.nodulos[nodulo].falas.Length; i++)
        {

            Personagens _personagem = dialogo.nodulos[nodulo].falas[i].personagem;

            if (_personagem != Personagens.Lurdinha)
            {
                personagemRosto[1].sprite = Falador.BuscarPolaroideNosAssets(_personagem, Expressao.Serio).personagem;

                i = dialogo.nodulos[nodulo].falas.Length;
            }
            else if (i + 1 == dialogo.nodulos[nodulo].falas.Length) 
            {
                Debug.Log("Monologo da Lurdinha");

                personagemRosto[1].color = new Color(0f, 0f, 0f, 0f);
            }
        }

        ProximaFala();
    }    
    
    public void ProximaFala()
    {
        foreach (Image image in personagemRosto)
        {
            image.color = opacidade.Desligar();
        }

        if (dialogo.nodulos[nodulo].falas.Length > proximaFala)
        {
            falaAtual = dialogo.nodulos[nodulo].falas[proximaFala];
        }

        faladorAtual = Falador.BuscarPolaroideNosAssets(falaAtual.personagem, falaAtual.emocao);

        npcNome.text = faladorAtual.nome;

        if (falaAtual.personagem == Personagens.Lurdinha)
        {
            personagemRosto[0].color = opacidade.Ligar();
            personagemRosto[0].sprite = faladorAtual.personagem;
        }
        else
        {
            personagemRosto[1].color = opacidade.Ligar();
            personagemRosto[1].sprite = faladorAtual.personagem;
        }

        if (falaAtual.fala == "")
        {
            proximaFala++;

            Analise();

            botao.onClick.Invoke();
        }
        else
        {
            corrotina = StartCoroutine(Escrever());
        }
    }

    public void ComecarProximaFala()
    {
        if (escrevendo)
        {
            TerminarEscrita();
        }
        else
        {
            ProximaFala();
        }
    }

    public void TerminarEscrita()
    {
        StopCoroutine(corrotina);

        textoDialogo.text = falaAtual.fala;

        proximaFala += 1;

        escrevendo = false;

        Analise();
    }

    private IEnumerator Escrever()
    {
        escrevendo = true;

        tempo = 0;

        textoDialogo.text = "";

        while (textoDialogo.text.Length < falaAtual.fala.Length)
        {
            textoDialogo.text = falaAtual.fala.Substring(0, Mathf.FloorToInt(tempo * velocidade));

            yield return null;

            tempo += Time.deltaTime;
        }

        proximaFala += 1;

        escrevendo = false;

        Analise();
    }

    private void Analise()
    {
        if (proximaFala >= dialogo.nodulos[nodulo].falas.Length)
        {
            botao.onClick.RemoveAllListeners();

            switch (dialogo.nodulos[nodulo].respostas.Length)
            {
                case 0:
                    botao.onClick.AddListener(() => 
                    {
                        AcabarConversa();
                        ResetarBotao();
                    });                    
                    break;
                case 1:
                    nodulo = dialogo.nodulos[nodulo].respostas[0].conexao;
                    proximaFala = 0;
                    botao.onClick.AddListener(() => 
                    {
                        ProximaFala();
                        ResetarBotao();
                    });
                    break;
                default:
                    dropdown.gameObject.SetActive(true);
                    ColocarOpcoes();
                    botao.onClick.AddListener(() => 
                    {
                        ConfirmarEscolha();
                        ResetarBotao();
                    });
                    botao.interactable = false;
                    break;
            }
        }
    }

    private void ColocarOpcoes()
    {
        List<string> opcoes = new List<string>();

        for (int i = 0; i < dialogo.nodulos[nodulo].respostas.Length; i++)
        {
            opcoes.Add(dialogo.nodulos[nodulo].respostas[i].resumo);            
        }

        opcoes.Add("(Escolha uma opção)");

        dropdown.AddOptions(opcoes);

        dropdown.value = opcoes.Count - 1;

        //for (int i = 0; i < dropdown.options.Count; i++)
        //{
        //    dropdown.itemText.color = Color.blue;
        //    dropdown.itemImage.color = Color.red;
        //}
    }

    public void RespostaEscolhida(int escolha)
    {
        if (escolha == dropdown.options.Count - 1)
        {
            botao.interactable = false;
        }
        else
        {
            dropdownIndex = escolha;
            botao.interactable = true;
        }
    }

    private void ConfirmarEscolha()
    {
        falaAtual.personagem = dialogo.nodulos[nodulo].respostas[dropdownIndex].personagem;
        falaAtual.emocao = dialogo.nodulos[nodulo].respostas[dropdownIndex].emocao;
        falaAtual.fala = dialogo.nodulos[nodulo].respostas[dropdownIndex].fala;

        faladorAtual = Falador.BuscarPolaroideNosAssets(falaAtual.personagem, falaAtual.emocao);
        npcNome.text = faladorAtual.nome;

        nodulo = dialogo.nodulos[nodulo].respostas[dropdownIndex].conexao;
        proximaFala = -1;
        dropdown.options.Clear();
        dropdown.gameObject.SetActive(false);

        corrotina = StartCoroutine(Escrever());

        personagemRosto[0].color = opacidade.Ligar();
        personagemRosto[1].color = opacidade.Desligar();

        personagemRosto[0].sprite = faladorAtual.personagem;
    }

    private void ResetarBotao()
    {
        botao.onClick.RemoveAllListeners();
        botao.onClick.AddListener(() => { ComecarProximaFala(); });
    }

    public void AcabarConversa()
    {
        npcDialogo.SetQuestControl();
        npcDialogo = null;
        personagemRosto[1].sprite = null;
        escrevendo = false;

        nodulo = 0;
        proximaFala = 0;

        sistemaDialogoUI.SetActive(false);

        GameManager.uiSendoUsada = false;
    }
}
