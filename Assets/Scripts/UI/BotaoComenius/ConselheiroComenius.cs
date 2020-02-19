using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConselheiroComenius : MonoBehaviour
{
    private bool visivel;
    public bool Visivel
    {
        get { return visivel; }

        set
        {
            visivel = value;
            canvas.enabled = visivel;
        }
    }

    [HideInInspector]
	public JanelaMissoes janelaMissoes;

    private Animator animator;
    private static readonly string janelaMissoesAbertaAnimatorParameter = "JanelaMissoesAberta";

    private Canvas canvas;

    void Start()
	{
		// Este objeto Comenius estará disponível em todas as cenas seguintes
		// Não é necessário adicionar o GameObject/Prefab em outras cenas
		DontDestroyOnLoad(this.gameObject);

		janelaMissoes = GetComponentInChildren<JanelaMissoes>();

        animator = GetComponent<Animator>();

        canvas = GetComponentInChildren<Canvas>();

        // Começa invisível, aparece no tutorial da sala dos professores
        Visivel = false;
	}

    // Provisório, depois poderemos escolher entre apresentar a janela de
    // missões e a janela de tutoriais
    public void HandlePointerClick()
    {
        if (janelaMissoes && janelaMissoes.Ativa && !janelaMissoes.Aberta)
            AbrirJanelaMissoes();
        else
            FecharJanelaMissoes();
    }

    public void AbrirJanelaMissoes()
	{
        if (janelaMissoes && janelaMissoes.Ativa && !janelaMissoes.Aberta)
        {
            janelaMissoes.Aberta = true;
            animator.SetBool(janelaMissoesAbertaAnimatorParameter, janelaMissoes.Aberta);
        }
	}

    public void FecharJanelaMissoes()
    {
        if (janelaMissoes && janelaMissoes.Ativa && janelaMissoes.Aberta)
        {
            janelaMissoes.Aberta = false;
            animator.SetBool(janelaMissoesAbertaAnimatorParameter, janelaMissoes.Aberta);
        }
    }
}