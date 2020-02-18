using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjudaComeniusDiretorPatio : MonoBehaviour {

    [SerializeField]
    private NpcDialogo dialogoDoDiretor;

    private Image imageBotaoFechar;

    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;
    private CanvasGroup conteudoDaAjuda;

    // Use this for initialization
    private void Start () {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;

        imageBotaoFechar = GetComponentInChildren<Button>().GetComponent<Image>();
        imageBotaoFechar.enabled = false;

        backgroundFadeEffect = GetComponentInChildren<FadeEffect>();
        conteudoDaAjuda = GetComponentInChildren<CanvasGroup>();
        conteudoDaAjuda.alpha = 0;

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        dialogoDoDiretor.OnEndDialogueEvent += Mostrar;
	}

    private void Mostrar()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        GameManager.UISendoUsada();

        canvas.enabled = true;
        yield return StartCoroutine(backgroundFadeEffect.Fade(0.6f));
        yield return new WaitForSeconds(0.4f);
        conteudoDaAjuda.alpha = 1;
        TocarAudio();

        StartCoroutine(PermitirFecharApos(2.5f));
    }

    private IEnumerator PermitirFecharApos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        imageBotaoFechar.enabled = true;
    }

    // Método para ser chamado por um botão na interface
    public void Fechar()
    {
        StartCoroutine(FecharCoroutine());
    }

    private IEnumerator FecharCoroutine()
    {
        conteudoDaAjuda.alpha = 0;
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(backgroundFadeEffect.Fade(0f));
        canvas.enabled = false;
        GameManager.UINaoSendoUsada();
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
