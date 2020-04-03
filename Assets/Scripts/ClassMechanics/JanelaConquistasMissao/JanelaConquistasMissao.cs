using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaConquistasMissao : MonoBehaviour {

    public bool Aberta { get; set; }

    [SerializeField] private CanvasGroup conteudo;
    private bool ConteudoVisivel
    {
        set
        {
            conteudo.alpha = value ? 1 : 0;
            conteudo.blocksRaycasts = value;
        }
    }

	// Use this for initialization
	void Start () {
        ConteudoVisivel = false;

        // Fazer com que a janela abra antes da Lurdinha sair da sala de aula
        // A troca de cena irá esperar a Coroutine Abrir acabar para mudar cena
        var trocaDeCena = FindObjectOfType<GoToScene>();
        trocaDeCena.AntesDeIrParaCenaEvent += Apresentar;
	}

    public IEnumerator Apresentar()
    {
        Aberta = true;

        var fadeEffect = GetComponentInChildren<FadeEffect>();
        if (fadeEffect) yield return StartCoroutine(fadeEffect.Fade(0.7f));

        ConteudoVisivel = true;

        var fichaDesempenhoAula = GetComponentInChildren<FichaDesempenhoAula>();
        fichaDesempenhoAula.Mostrar();

        // Deixar a janela à mostra enquanto a propriedade Aberta == true
        yield return new WaitWhile(() => Aberta);
        // Quando alguém definir a propriedade Aberta=false, esconder a janela
        ConteudoVisivel = false;
    }
}
