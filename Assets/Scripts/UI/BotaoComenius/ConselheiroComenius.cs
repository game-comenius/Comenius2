using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConselheiroComenius : MonoBehaviour
{
    // Conselheiro Comenius é singleton, só pode existir um Conselheiro
    // Este objeto Comenius estará disponível em todas as cenas seguintes
    // Não é necessário adicionar o GameObject/Prefab em outras cenas
    private static readonly object simpleLock = new object();
    private static ConselheiroComenius instance;
    private static ConselheiroComenius Instance
    {
        get
        {
            lock (simpleLock)
            {
                if (instance == null)
                {
                    // Search for existing instance
                    instance = FindObjectOfType<ConselheiroComenius>();

                    if (instance == null)
                        Debug.Log("Não há " + typeof(ConselheiroComenius) + " nesta cena!");
                    else
                        DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }

    private static bool visivel;
    public static bool Visivel
    {
        get { return visivel; }

        set
        {
            visivel = value;
            Instance.canvas.enabled = visivel;
        }
    }

    private JanelaMissoes janelaMissoes;
    public static JanelaMissoes JanelaMissoes { get { return Instance.janelaMissoes; } }

    private Animator animator;
    private static readonly string janelaMissoesAbertaAnimatorParameter = "JanelaMissoesAberta";

    private Canvas canvas;
    public static Canvas Canvas { get { return Instance.canvas; } }

    private void Awake()
    {
        if (Instance != this) Destroy(this.gameObject);

        janelaMissoes = GetComponentInChildren<JanelaMissoes>();

        animator = GetComponent<Animator>();

        canvas = GetComponentInChildren<Canvas>();

        // Começa invisível, aparece no tutorial da sala dos professores
        Visivel = false;

        // Para ajudar no desenvolvimento, este objeto sempre começará visível
        #if UNITY_EDITOR
        Visivel = true;
        #endif
    }

    // Provisório, depois poderemos escolher entre apresentar a janela de
    // missões e a janela de tutoriais
    public static void HandlePointerClick()
    {
        if (JanelaMissoes && JanelaMissoes.Ativa && !JanelaMissoes.Aberta)
            AbrirJanelaMissoes();
        else
            FecharJanelaMissoes();
    }

    public static void AbrirJanelaMissoes()
	{
        if (JanelaMissoes && JanelaMissoes.Ativa && !JanelaMissoes.Aberta)
        {
            JanelaMissoes.Aberta = true;
            Instance.animator.SetBool(janelaMissoesAbertaAnimatorParameter, JanelaMissoes.Aberta);
        }
	}

    public static void FecharJanelaMissoes()
    {
        if (JanelaMissoes && JanelaMissoes.Ativa && JanelaMissoes.Aberta)
        {
            JanelaMissoes.Aberta = false;
            Instance.animator.SetBool(janelaMissoesAbertaAnimatorParameter, JanelaMissoes.Aberta);
        }
    }
}