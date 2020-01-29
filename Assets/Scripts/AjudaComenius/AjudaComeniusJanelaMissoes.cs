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
        "Muito bem Lurdinha! Agora que você falou com o professor Jean e já sabe sua missão, você pode consultá-la sempre que quiser neste botão.",
        "No momento você têm uma missão principal e pequenas tarefas que podem te ajudar na missão em questão. Aperte no botão azul com o título de uma missão para ver seus objetivos específicos!",
        "Ótimo! Agora você pode ir até a sala multimeios onde te explicarei mais coisas.\nTe encontro lá!"
    };

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;
    private FadeEffect focoBotaoDaJanela;

    // Use this for initialization
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        var fadeEffects = GetComponentsInChildren<FadeEffect>();
        focoBotaoDaJanela = fadeEffects[0];
        backgroundFadeEffect = fadeEffects[1];

        componenteTexto = GetComponentInChildren<TextMeshProUGUI>();

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        jean.OnEndDialogueEvent += AdicionarMissoesNaJanelaMissoes;
        jean.OnEndDialogueEvent += Mostrar;
    }

    private void AdicionarMissoesNaJanelaMissoes()
    {
        var tituloMissao1 = "Coletar mídias";
        string[] ordensMissao1 = { "Colete no mínimo 3 mídias" };
        janelaMissoes.AdicionarMissao(tituloMissao1, ordensMissao1);

        var tituloMissao2 = "Fazer planejamento";
        string[] ordensMissao2 = { "Pegue a prancheta sobre a mesa" };
        janelaMissoes.AdicionarMissao(tituloMissao2, ordensMissao2);
    }

    private void Mostrar()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        GameManager.UISendoUsada();

        // Alpha do background escuro para aumentar o foco do jogador
        var alpha = 0.8f;

        // Fade in do background escuro
        //backgroundFadeEffect.MaxAlpha = alpha;
        StartCoroutine(backgroundFadeEffect.Fade(alpha));

        canvas.enabled = true;

        TocarAudio();

        componenteTexto.text = falas[0];

        yield return new WaitForSeconds(2);

        // Posicionar o canvas da janela de missões sobre o este canvas
        var mySortingOrder = GetComponentInChildren<Canvas>().sortingOrder;
        janelaMissoes.GetComponentInChildren<Canvas>().sortingOrder = mySortingOrder + 1;

        janelaMissoes.Ativar();

        // Focar no botão da janela de missões
        backgroundFadeEffect.GetComponent<Image>().enabled = false;
        //focoBotaoDaJanela.MaxAlpha = alpha;
        focoBotaoDaJanela.GetComponent<Image>().color = new Color(0, 0, 0, alpha);


        // Esperar o jogador abrir a janela de missões
        yield return new WaitUntil(() => janelaMissoes.Aberta);
        focoBotaoDaJanela.GetComponent<Image>().enabled = false;
        backgroundFadeEffect.GetComponent<Image>().enabled = true;
        componenteTexto.text = falas[1];

        // Esperar o jogador ler todas as missões que estão na janela
        yield return new WaitUntil(() => janelaMissoes.CompletamenteExplorada());
        componenteTexto.text = falas[2];

        var coroutine = PermitirFecharApos(4);
        StartCoroutine(coroutine);
    }

    private IEnumerator Fechar()
    {
        yield return StartCoroutine(backgroundFadeEffect.Fade(0.8f));
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();
    }

    private IEnumerator PermitirFecharApos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        yield return new WaitUntil(() => Input.anyKeyDown);
        StartCoroutine(Fechar());
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
