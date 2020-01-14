using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AjudaComeniusJanelaMissoes : MonoBehaviour {

    // A ajuda do comenius vai aparecer logo depois do diálogo com o Jean
    // por isso precisamos de uma referência ao Jean neste caso
    [SerializeField]
    private NpcDialogo jean;

    private TextMeshProUGUI componenteTexto;

    [SerializeField]
    private JanelaMissoes janelaMissoes;

    private readonly string[] falas =
    {
        "Muito bem Lurdinha! Agora que você falou com o professor Jean e já sabe sua missão, você pode consultá-la sempre que quiser aqui.",
        "No momento você têm uma missão principal e pequenas tarefas que podem te ajudar na missão em questão. Clique no título da missão para ver seus objetivos específicos!",
        "Ótimo! Agora você pode ir até a sala multimeios onde te explicarei mais coisas. Te encontro lá!"
    };

    private bool permiteFechar;

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;
    private FadeEffect focoBotaoDaJanela;

    // Use this for initialization
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        var fadeEffects = GetComponentsInChildren<FadeEffect>();
        backgroundFadeEffect = fadeEffects[0];
        focoBotaoDaJanela = fadeEffects[1];

        componenteTexto = GetComponentInChildren<TextMeshProUGUI>();

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        jean.OnEndDialogueEvent += Mostrar;
    }

    private void Mostrar()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        GameManager.UISendoUsada();

        // Alpha do background escuro para aumentar o foco do jogador
        var alpha = 0.6f;

        // Fade in do background escuro
        backgroundFadeEffect.MaxAlpha = alpha;
        StartCoroutine(backgroundFadeEffect.Fade());

        canvas.enabled = true;

        TocarAudio();

        componenteTexto.text = falas[0];

        yield return new WaitForSeconds(1.5f);

        // Posicionar o canvas da janela de missões sobre o este canvas
        var mySortingOrder = GetComponentInChildren<Canvas>().sortingOrder;
        janelaMissoes.GetComponentInChildren<Canvas>().sortingOrder = mySortingOrder + 1;

        janelaMissoes.Ativar();

        // Focar no botão da janela de missões
        focoBotaoDaJanela.MaxAlpha = alpha;
        StartCoroutine(focoBotaoDaJanela.Fade());

        // Esperar o jogador abrir a janela de missões
        yield return new WaitUntil(() => janelaMissoes.Aberta);

        StartCoroutine(focoBotaoDaJanela.Fade());

        componenteTexto.text = falas[1];

        var coroutine = PermitirFecharApos(8);
        StartCoroutine(coroutine);
    }

    private IEnumerator Fechar()
    {
        yield return StartCoroutine(backgroundFadeEffect.Fade());
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();
    }

    private IEnumerator PermitirFecharApos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        permiteFechar = true;
    }

    private void Update()
    {
        if (permiteFechar && Input.anyKeyDown)
            StartCoroutine(Fechar());
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
