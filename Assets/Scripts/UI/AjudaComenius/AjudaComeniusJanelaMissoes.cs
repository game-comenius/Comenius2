using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AjudaComeniusJanelaMissoes : MonoBehaviour {

    // A ajuda do comenius vai aparecer logo depois do diálogo com o Jean
    // por isso precisamos de uma referência ao Jean neste caso
    [SerializeField]
    private NpcDialogo dialogoDoJean;

    private ConselheiroComenius conselheiroComenius;
    private JanelaMissoes janelaMissoes;

    private readonly string[] falas =
    {
        "Muito bem Lurdinha! Agora que você falou com o professor Jean e já sabe sua missão, você pode consultá-la sempre que quiser neste botão.",
        "No momento você têm uma missão principal e pequenas tarefas que podem te ajudar na missão em questão. Aperte no botão azul com o título de uma missão para ver seus objetivos específicos!",
        "Ótimo! Agora você pode ir até a sala multimeios onde te explicarei mais coisas.\nTe encontro lá!"
    };

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;
    private CanvasGroup conteudo;
    private TextMeshProUGUI componenteTexto;
    private Image focoBotaoDaJanela;
    private GameObject botaoEntendi;
    private GameObject botaoPularAjuda;

    // Use this for initialization
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;

        var fadeEffect = GetComponentInChildren<FadeEffect>();
        backgroundFadeEffect = fadeEffect;

        conteudo = canvas.GetComponentInChildren<CanvasGroup>();
        conteudo.alpha = 0;

        componenteTexto = conteudo.GetComponentInChildren<TextMeshProUGUI>();

        focoBotaoDaJanela = canvas.transform.GetChild(0).GetComponent<Image>();
        focoBotaoDaJanela.color = Color.clear;

        var botoes = conteudo.GetComponentsInChildren<Button>();
        // Botão Entendi só vai aparecer no final deste tutorial
        botaoEntendi = botoes[0].gameObject;
        botaoEntendi.SetActive(false);

        // Botão para pular tutorial aparecerá no início e desaparecerá no fim
        botaoPularAjuda = botoes[1].gameObject;

        conselheiroComenius = FindObjectOfType<ConselheiroComenius>();

        if (conselheiroComenius)
        {
            janelaMissoes = conselheiroComenius.janelaMissoes;
            // Cadastrar função para ser invocada quando o diretor fechar o diálogo
            dialogoDoJean.OnEndDialogueEvent += AdicionarMissaoNaJanelaMissoes;
            dialogoDoJean.OnEndDialogueEvent += Mostrar;
        }
    }

    private void AdicionarMissaoNaJanelaMissoes()
    {
        var tituloMissao = "Coletar mídias";
        string[] ordensMissao = { "Colete pelo menos 3 mídias" };
        janelaMissoes.AdicionarMissao(tituloMissao, ordensMissao);
    }

    private void Mostrar()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        GameManager.UISendoUsada();

        canvas.enabled = true;

        var alpha = 0.8f;
        // Fade in do background escuro
        yield return StartCoroutine(backgroundFadeEffect.Fade(alpha));

        yield return new WaitForSeconds(0.4f);
        conteudo.alpha = 1;
        componenteTexto.text = falas[0];
        TocarAudio();

        yield return new WaitForSeconds(1.2f);
        // Posicionar o canvas da janela de missões sobre o este canvas
        var mySortingOrder = GetComponentInChildren<Canvas>().sortingOrder;
        conselheiroComenius.GetComponentInChildren<Canvas>().sortingOrder = mySortingOrder + 1;

        conselheiroComenius.Visivel = true;
        janelaMissoes.Ativa = true;

        // Focar no botão da janela de missões
        backgroundFadeEffect.GetComponent<Image>().enabled = false;
        focoBotaoDaJanela.color = new Color(0, 0, 0, alpha);

        // Esperar o jogador abrir a janela de missões
        yield return new WaitUntil(() => janelaMissoes.Aberta);
        focoBotaoDaJanela.enabled = false;
        backgroundFadeEffect.GetComponent<Image>().enabled = true;
        componenteTexto.text = falas[1];

        // Esperar o jogador ler todas as missões que estão na janela
        yield return new WaitUntil(() => janelaMissoes.CompletamenteExplorada());
        componenteTexto.text = falas[2];
        botaoEntendi.SetActive(true);
        botaoPularAjuda.SetActive(false);
    }

    public void Fechar()
    {
        StartCoroutine(FecharCoroutine());
    }

    private IEnumerator FecharCoroutine()
    {
        conselheiroComenius.FecharJanelaMissoes();

        conteudo.alpha = 0;
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(backgroundFadeEffect.Fade(0));
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();

        // Esta ajuda será vista apenas 1 vez
        dialogoDoJean.OnEndDialogueEvent -= AdicionarMissaoNaJanelaMissoes;
        dialogoDoJean.OnEndDialogueEvent -= Mostrar;
    }

    public void PularAjuda()
    {
        StopAllCoroutines();
        // Fazer o que esta ajuda faria no jogo
        janelaMissoes.Ativa = true;
        Fechar();
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
