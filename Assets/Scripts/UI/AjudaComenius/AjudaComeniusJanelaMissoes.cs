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

    private JanelaMissoes janelaMissoes;

    private readonly string[] falas =
    {
        "Muito bem Lurdinha! Agora que você falou com o professor Jean e já sabe sua missão, você pode consultá-la sempre que quiser neste botão.",
        "No momento, podemos ver que você tem 2 missões! Aperte nos botões azuis para ver objetivos específicos e dicas.",
        "Ótimo! Sempre que quiser fechar a janela de missões, basta usar o mesmo botão que usou para abri-la.",
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

        janelaMissoes = ConselheiroComenius.JanelaMissoes;
        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        dialogoDoJean.OnEndDialogueEvent += ManagerQuest.SetupQuestLog;
        dialogoDoJean.OnEndDialogueEvent += Mostrar;
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

        // Pegar o animator do ImageComeniusFantasma
        conteudo.GetComponentInChildren<Animator>().Play("Flutuar");

        yield return new WaitForSeconds(1.2f);
        // Posicionar o canvas da janela de missões sobre o este canvas
        var mySortingOrder = GetComponentInChildren<Canvas>().sortingOrder;
        ConselheiroComenius.Canvas.sortingOrder = mySortingOrder + 1;

        ConselheiroComenius.Visivel = true;
        janelaMissoes.Ativa = true;

        // Pegar o animator do BackgroundTranslucidoComBuraco
        canvas.GetComponentInChildren<Animator>().Play("Focar");

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
        ConselheiroComenius.FecharJanelaMissoes();

        conteudo.alpha = 0;
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(backgroundFadeEffect.Fade(0));
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();

        // Esta ajuda será vista apenas 1 vez
        dialogoDoJean.OnEndDialogueEvent -= ManagerQuest.SetupQuestLog;
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
