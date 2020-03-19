using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameComenius.Dialogo;
using TMPro;

public class SistemaDialogo : MonoBehaviour
{
    static public SistemaDialogo sistemaDialogo;

    #region Referências para os GameObject na UI
    [SerializeField] private GameObject sistemaDialogoUI;

    [SerializeField] private GameObject painel;

    [SerializeField] private TMP_Text textoDialogo;

    [SerializeField] private TMP_Text npcNome;

    [SerializeField] private Button botao;
    [SerializeField] private Button botaoRetornar;

    [SerializeField] private TMP_Dropdown dropdown;

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

    private List<int> questsIndex = new List<int>();

    // Os balões de fala da Lurdinha tem uma cor diferente
    // Esta aqui é um verde parecido com o verde do Whatsapp
    // private static Color corDialogoLurdinha = new Color(0.7411765f, 0.945098f, 0.8196079f);
    // Esta aqui é um azul
    private static Color corDialogoLurdinha = new Color(0.8705882f, 0.9568627f, 0.9647059f);

    private string[] richText = new string[]
    {
        "<b>",
        "</b>",
        "<i>",
        "</i>",
        "<s>",
        "</s>",
        "<u>",
        "</u>"
    };
    #endregion

    private void Awake()
    {
        sistemaDialogo = this;
    }

    private void Start()
    {
        botaoRetornar.onClick.AddListener(() => RetornarParaFalaAnterior());
        botaoRetornar.gameObject.SetActive(false);

        botao.onClick.AddListener(() => { ComecarProximaFala(); });
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            ResetarBotao();
            AcabarConversa();
        }
    }

#endif

    #region Logica do Sistema de Fala
    public void ComecarDialogo(Dialogo _dialogo, NpcDialogo _npcDialogoQA)
    {
        if (npcDialogo == null && _dialogo != null) 
        {
            GameManager.UISendoUsada();

            dialogo = _dialogo;

            for (int i = 0; i < dialogo.nodulos.Length; i++)
            {
                for (int j = 0; j < dialogo.nodulos[i].respostas.Count; j++)
                {
                    if (!ManagerQuest.VerifyQuestIsAvailable(dialogo.nodulos[i].respostas[j].questIndex)) 
                    {
                        dialogo.nodulos[i].respostas.RemoveAt(j);
                        j--;
                    }
                }
            }

            npcDialogo = _npcDialogoQA;

            if (npcDialogo) questsIndex.Add(npcDialogo.questIndex);

            InicializarDialogo();
        }
    }

    private void InicializarDialogo()
    {
        sistemaDialogoUI.SetActive(true);

        proximaFala = 0;

        nodulo = 0;

        escrevendo = false;

        for (int i = 0; i < dialogo.nodulos[nodulo].falas.Length; i++) 
        {
            Personagens _personagem = dialogo.nodulos[nodulo].falas[i].personagem;

            if (_personagem == Personagens.Lurdinha)
            {
                personagemRosto[0].sprite = Falador.BuscarPolaroideNosAssets(_personagem, Expressao.Serio).personagem;

                i = dialogo.nodulos[nodulo].falas.Length;
            }
            else if (i + 1 == dialogo.nodulos[nodulo].falas.Length)
            {
                personagemRosto[0].sprite = null;

                personagemRosto[0].color = new Color(0f, 0f, 0f, 0f);

                //personagemRosto[0].transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }

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
                personagemRosto[1].sprite = null;

                personagemRosto[1].color = new Color(0f, 0f, 0f, 0f);

                //personagemRosto[1].transform.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }

        ProximaFala();
    }    
    
    public void ProximaFala()
    {
        foreach (Image image in personagemRosto)
        {
            if (image.sprite != null)
            {
                image.color = opacidade.Desligar();
            }
        }

        if (dialogo.nodulos[nodulo].falas.Length > proximaFala)
        {
            falaAtual = dialogo.nodulos[nodulo].falas[proximaFala];

            for (int i = 0; i < falaAtual.fala.ToCharArray().Length; i++)
            {
                if ((int)falaAtual.fala.ToCharArray()[i] == 13)
                {
                    char[] m = new char[falaAtual.fala.ToCharArray().Length - 1];

                    for (int j = 0; j < m.Length; j++)
                    {
                        if (j < i)
                        {
                            m[j] = falaAtual.fala.ToCharArray()[j];
                        }
                        else
                        {
                            m[j] = falaAtual.fala.ToCharArray()[j + 1];
                        }
                    }

                    falaAtual.fala = m.ArrayToString();
                }
            }
        }

        faladorAtual = Falador.BuscarPolaroideNosAssets(falaAtual.personagem, falaAtual.emocao);

        npcNome.text = faladorAtual.nome;

        var balaoDeFala = painel.transform.GetChild(0).GetComponent<Image>();
        if (falaAtual.personagem == Personagens.Lurdinha)
        {
            // Trocar a cor do balão de fala para a cor que a Lurdinha usa
            balaoDeFala.color = corDialogoLurdinha;

            personagemRosto[0].color = opacidade.Ligar();
            personagemRosto[0].sprite = faladorAtual.personagem;
            painel.transform.rotation = Quaternion.identity;

        }
        else
        {
            // Trocar a cor do balão de fala para a original novamente
            balaoDeFala.color = new Color(1, 1, 1);

            personagemRosto[1].color = opacidade.Ligar();
            personagemRosto[1].sprite = faladorAtual.personagem;
            painel.transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        // Apresentar o botão de retornar para a fala anterior
        var indiceFalaAtual = proximaFala;
        if (indiceFalaAtual > 0 && dialogo.nodulos[nodulo].falas[indiceFalaAtual - 1].fala != "")
            botaoRetornar.gameObject.SetActive(true);
        else
            botaoRetornar.gameObject.SetActive(false);

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

    // Só é chamado através do OnClick() do botão de próxima fala
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

    public void RetornarParaFalaAnterior()
    {
        if (escrevendo)
        {
            TerminarEscrita();
        }
        else
        {
            // Resetar botão de avançar se estiver na função "terminar diálogo"
            ResetarBotao();
            // Retornar para a fala anterior e executar ela
            proximaFala -= 2;
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

        while (Mathf.FloorToInt(tempo * velocidade) < falaAtual.fala.Length)
        {
            if (falaAtual.fala[Mathf.FloorToInt(tempo * velocidade)] == '<')
            {
                string text = falaAtual.fala.Substring(Mathf.FloorToInt(tempo * velocidade));

                text = text.Substring(0, text.IndexOf('>') + 1);

                foreach (string i in richText)
                {
                    if (i == text)
                    {
                        tempo += ((float)i.Length) / velocidade;

                        break;
                    }
                }
            }
            else if (falaAtual.fala[Mathf.FloorToInt(tempo * velocidade)] == ' ')
            {
                tempo += 1 / velocidade;
            }

            textoDialogo.text = falaAtual.fala.Substring(0, Mathf.FloorToInt(tempo * velocidade));

            yield return null;

            tempo += Time.deltaTime;
        }

        textoDialogo.text = falaAtual.fala.Substring(0, Mathf.FloorToInt(tempo * velocidade));

        proximaFala += 1;

        escrevendo = false;

        Analise();
    }

    private void Analise()
    {
        if (proximaFala >= dialogo.nodulos[nodulo].falas.Length)
        {
            botao.onClick.RemoveAllListeners();

            switch (dialogo.nodulos[nodulo].respostas.Count)
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

        for (int i = 0; i < dialogo.nodulos[nodulo].respostas.Count; i++)
        {
            if (ManagerQuest.VerifyQuestIsAvailable(dialogo.nodulos[nodulo].respostas[i].questIndex))
            {
                opcoes.Add(dialogo.nodulos[nodulo].respostas[i].resumo);
            }
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

    // Este método é chamado pelo OnValueChanged do Dropdown que é filho do
    // objeto Dialogue em ManagerScene
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
        // Se precisar fazer algo de acordo com a resposta escolhida pelo
        // jogador, invocar as funções cadastradas no evento
        // QuandoEscolherRespostaEvent se ele possuir funções dentro dele
        if (npcDialogo != null) npcDialogo.QuandoEscolherResposta(dropdownIndex);

        falaAtual.personagem = dialogo.nodulos[nodulo].respostas[dropdownIndex].personagem;
        falaAtual.emocao = dialogo.nodulos[nodulo].respostas[dropdownIndex].emocao;
        falaAtual.fala = dialogo.nodulos[nodulo].respostas[dropdownIndex].fala;

        if (dialogo.nodulos[nodulo].respostas[dropdownIndex].questIndex != 0)
        {
            questsIndex.Add(dialogo.nodulos[nodulo].respostas[dropdownIndex].questIndex);
        }

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
        GameManager.UINaoSendoUsada();

        if (npcDialogo != null) npcDialogo.OnEndDialogue();
        npcDialogo = null;
        personagemRosto[1].sprite = null;
        escrevendo = false;

        nodulo = 0;
        proximaFala = 0;

        sistemaDialogoUI.SetActive(false);

        foreach (int questIndex in questsIndex)
        {
            ManagerQuest.QuestTakeStep(questIndex);
        }

        questsIndex.Clear();
    }
    #endregion

    #region FontSize
    public void IncreaseFontSize()
    {
        textoDialogo.fontSize = Mathf.Min(textoDialogo.fontSize + 4f, 26);
        npcNome.fontSize = Mathf.Min(npcNome.fontSize + 4f, 32);
    }

    public void DecreaseFontSize()
    {
        textoDialogo.fontSize = Mathf.Max(textoDialogo.fontSize - 4f, 14);
        npcNome.fontSize = Mathf.Max(npcNome.fontSize - 4f, 20);
    }
    #endregion 
}
