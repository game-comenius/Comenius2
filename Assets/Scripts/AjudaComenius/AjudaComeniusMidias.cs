using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AjudaComeniusMidias : MonoBehaviour {

    // A ajuda do comenius vai aparecer logo depois do diálogo com a Drica
    // por isso precisamos de uma referência ao Drica neste caso
    [SerializeField]
    private NpcDialogo drica;


    private Canvas canvas;
    private FadeEffect backgroundFadeEffect;
    private Image focoBotaoDaJanela;

    // Use this for initialization
    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();

        var fadeEffect = GetComponentInChildren<FadeEffect>();
        backgroundFadeEffect = fadeEffect;

        focoBotaoDaJanela = canvas.transform.GetChild(0).GetComponent<Image>();
        focoBotaoDaJanela.color = Color.clear;

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        drica.OnEndDialogueEvent += Mostrar;
    }

    private void Mostrar()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        GameManager.UISendoUsada();

        Debug.Log("Oi!");

        yield return new WaitForSeconds(2);

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
