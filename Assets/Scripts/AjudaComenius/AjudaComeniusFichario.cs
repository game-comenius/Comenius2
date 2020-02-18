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

    private CanvasGroup baloes1;
    private GameObject botaoEntendi1;

    private CanvasGroup baloes2;
    private Image focoBotaoDaJanela;
    private GameObject botaoEntendi2;
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

        baloes1 = canvasGroups[1];
        baloes1.alpha = 0;

        botaoEntendi1 = baloes1.GetComponentInChildren<Button>().gameObject;
        botaoEntendi1.SetActive(false);

        baloes2 = canvasGroups[2];
        baloes2.alpha = 0;
        baloes2.blocksRaycasts = false;

        focoBotaoDaJanela = canvas.transform.GetChild(0).GetComponent<Image>();
        focoBotaoDaJanela.color = Color.clear;

        botaoEntendi2 = baloes2.GetComponentInChildren<Button>().gameObject;
        botaoEntendi2.SetActive(false);

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

        // Apresentar o primeiro conjunto de balões
        yield return new WaitForSeconds(0.6f);
        baloes1.alpha = 1;

        var tempoParaMostrarBotaoEntendi = 1.5f;

        yield return new WaitForSeconds(tempoParaMostrarBotaoEntendi);
        botaoEntendi1.gameObject.SetActive(true);

        // Espera o jogador apertar no botão entendi e confirmar deseja passar
        var entendiPrimeiraParte = false;
        botaoEntendi1.GetComponent<Button>().onClick.AddListener
        (
            () => { entendiPrimeiraParte = true; }
        );
        yield return new WaitUntil( () => entendiPrimeiraParte );

        // Apresentar o segundo conjunto de balões
        yield return new WaitForSeconds(0.4f);
        botaoEntendi1.SetActive(false);
        baloes2.alpha = 1;
        baloes2.blocksRaycasts = true;

        yield return new WaitForSeconds(1.2f);
        // Mostrar o botão para abrir o fichário
        botaoFichario.Ativo = true;
        // Focar no botão para abrir o fichário
        backgroundFadeEffect.GetComponent<Image>().enabled = false;
        focoBotaoDaJanela.color = new Color(0, 0, 0, alpha);

        StartCoroutine(PermitirFecharApos(tempoParaMostrarBotaoEntendi));
    }

    public void Fechar()
    {
        StartCoroutine(FecharCoroutine());
    }

    private IEnumerator FecharCoroutine()
    {
        baloes1.alpha = 0;
        baloes2.alpha = 0;
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

    private IEnumerator PermitirFecharApos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        botaoPularT.gameObject.SetActive(false);
        botaoEntendi2.gameObject.SetActive(true);
    }

    public void PularAjuda()
    {
        // Pausar ajuda
        StopAllCoroutines();
        // Fazer o efeito que esta ajuda faria no jogo
        botaoFichario.Ativo = true;
        // Fechar ajuda normalmente
        Fechar();
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
