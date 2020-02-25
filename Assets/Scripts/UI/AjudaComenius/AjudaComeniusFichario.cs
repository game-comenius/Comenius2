using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Essa ajuda não fica observando algo para então executar sozinha
// Essa ajuda é chamada ativamente através do public void Mostrar()
public class AjudaComeniusFichario : MonoBehaviour
{
    [SerializeField]
    private Button BotaoQueAtivaEstaAjuda;

    private BotaoAbrirFichario botaoFichario;

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;

    private CanvasGroup conteudo;

    private CanvasGroup balaoDireita;
    private GameObject botaoEntendi1;

    private CanvasGroup baloesEsquerda;

    private Image focoBotaoDaJanela;

    private Transform botaoPularT;


    // Use this for initialization
    private void Start()
    {
        botaoFichario = FindObjectOfType<BotaoAbrirFichario>();

        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;

        var fadeEffect = GetComponentInChildren<FadeEffect>();
        backgroundFadeEffect = fadeEffect;

        var canvasGroups = canvas.GetComponentsInChildren<CanvasGroup>();
        conteudo = canvasGroups[0];
        conteudo.alpha = 0;
        
        balaoDireita = canvasGroups[1];
        balaoDireita.alpha = 0;

        botaoEntendi1 = balaoDireita.GetComponentInChildren<Button>().gameObject;

        baloesEsquerda = canvasGroups[2];
        baloesEsquerda.alpha = 0;
        baloesEsquerda.blocksRaycasts = false;

        focoBotaoDaJanela = canvas.transform.GetChild(0).GetComponent<Image>();
        focoBotaoDaJanela.color = Color.clear;

        // Botão para pular tutorial aparecerá no início e desaparecerá no fim
        botaoPularT = conteudo.transform.GetChild(conteudo.transform.childCount - 1);

        BotaoQueAtivaEstaAjuda.onClick.AddListener(Mostrar);
    }

    // Este método será chamado quando o BotaoQueAtivaEstaAjuda for apertado
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

        // Apresentar o Comenius e o botão de pular tutorial
        yield return new WaitForSeconds(0.4f);
        conteudo.alpha = 1;
        TocarAudio();

        // Pegar o animator do ImageComeniusFantasma
        conteudo.GetComponentInChildren<Animator>().Play("Flutuar");

        // Apresentar o primeiro conjunto de balões
        yield return new WaitForSeconds(0.6f);
        balaoDireita.alpha = 1;

        // Espera o jogador apertar no botão entendi e confirmar deseja passar
        var entendiPrimeiraParte = false;
        botaoEntendi1.GetComponent<Button>().onClick.AddListener
        (
            () => { entendiPrimeiraParte = true; }
        );
        yield return new WaitUntil( () => entendiPrimeiraParte );

        yield return new WaitForSeconds(0.4f);
        botaoEntendi1.SetActive(false);

        // Apresentar o segundo conjunto de balões

        // O último balão vai aparecer alguns segundos depois, desativado agora
        var ultimoBalao = baloesEsquerda.transform.GetChild(1).gameObject;
        ultimoBalao.SetActive(false);

        baloesEsquerda.alpha = 1;
        baloesEsquerda.blocksRaycasts = true;

        yield return new WaitForSeconds(1.2f);
        // Mostrar o botão para abrir o fichário
        botaoFichario.Visivel = true;
        // Destacar o botão artificialmente
        botaoFichario.GetComponent<Image>().color = Color.white;
        // Focar no botão para abrir o fichário
        backgroundFadeEffect.GetComponent<Image>().enabled = false;
        focoBotaoDaJanela.color = new Color(0, 0, 0, alpha);

        // Apresentar o último balão
        yield return new WaitForSeconds(1.5f);
        ultimoBalao.SetActive(true);
        botaoPularT.gameObject.SetActive(false);
    }

    public void Fechar()
    {
        StartCoroutine(FecharCoroutine());
    }

    private IEnumerator FecharCoroutine()
    {
        balaoDireita.alpha = 0;
        baloesEsquerda.alpha = 0;
        conteudo.alpha = 0;
        yield return new WaitForSeconds(0.4f);
        focoBotaoDaJanela.enabled = false;
        backgroundFadeEffect.GetComponent<Image>().enabled = true;
        yield return StartCoroutine(backgroundFadeEffect.Fade(0));
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();

        // Esta ajuda será vista apenas 1 vez
        BotaoQueAtivaEstaAjuda.onClick.RemoveListener(Mostrar);
    }

    public void PularAjuda()
    {
        // Pausar ajuda
        StopAllCoroutines();
        // Fazer o efeito que esta ajuda faria no jogo
        botaoFichario.Visivel = true;
        // Fechar ajuda normalmente
        Fechar();
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
