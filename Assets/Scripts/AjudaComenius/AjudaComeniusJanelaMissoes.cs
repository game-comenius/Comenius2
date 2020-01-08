using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AjudaComeniusJanelaMissoes : MonoBehaviour {

    // A ajuda do comenius vai aparecer logo depois do diálogo com o Jean
    // por isso precisamos de uma referência ao Jean neste caso
    [SerializeField]
    private NpcDialogo jean;

    [SerializeField]
    private TextMeshProUGUI texto;

    [SerializeField]
    private JanelaMissoes janelaMissoes;

    private readonly string[] falas =
    {
        "Muito bem Lurdinha! Agora que você falou com o professor Jean e já sabe sua missão, você pode consultá-la sempre que quiser aqui.",
        "No momento você têm uma missão principal e pequenas tarefas que podem te ajudar na missão em questão. Clique nos tópicos para ver seus objetivos!",
        "Ótimo! Agora você pode ir até a sala multimeios onde te explicarei mais coisas. Te encontro lá!"
    };

    private bool permiteFechar;

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;

    // Use this for initialization
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        backgroundFadeEffect = GetComponentInChildren<FadeEffect>();

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        jean.OnEndDialogueEvent += Mostrar;
    }

    private void Mostrar()
    {
        GameManager.UISendoUsada();

        texto.text = falas[0];
        canvas.enabled = true;

        backgroundFadeEffect.MaxAlpha = 0.6f;
        StartCoroutine(backgroundFadeEffect.Fade());

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

}
