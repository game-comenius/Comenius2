using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConselheiroComenius : MonoBehaviour
{
    [HideInInspector]
	public JanelaMissoes janelaMissoes;

    private Animator animator;
    private static readonly string janelaMissoesAbertaAnimatorParameter = "JanelaMissoesAberta";

    void Start()
	{
		// Este objeto Comenius estará disponível em todas as cenas seguintes
		// Não é necessário adicionar o GameObject/Prefab em outras cenas
		DontDestroyOnLoad(this.gameObject);

		janelaMissoes = GetComponentInChildren<JanelaMissoes>();
        janelaMissoes.Ativa = true;

        animator = GetComponent<Animator>();
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