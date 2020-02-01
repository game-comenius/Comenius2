using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjudaComeniusDiretorPatio : MonoBehaviour {

    [SerializeField]
    private NpcDialogo diretor;

    private bool permiteFechar;

    private Canvas canvas;
    //private FadeEffect backgroundFadeEffect;

    // Use this for initialization
    private void Start () {
        canvas = GetComponentInChildren<Canvas>();

        //backgroundFadeEffect = GetComponentInChildren<FadeEffect>();
        
        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        diretor.OnEndDialogueEvent += Mostrar;
	}

    private void Mostrar()
    {
        GameManager.UISendoUsada();

        canvas.enabled = true;
        //backgroundFadeEffect.MaxAlpha = 0.6f;
        StartCoroutine(FadeEffect.instance.Fade(0.6f));

        TocarAudio();

        var coroutine = PermitirFecharApos(8);
        StartCoroutine(coroutine);
    }

    private IEnumerator Fechar()
    {
        yield return StartCoroutine(FadeEffect.instance.Fade(0.6f));
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
        {
            StartCoroutine(Fechar());
            permiteFechar = false;
        }
    }

    private void TocarAudio()
    {
        var source = GetComponent<AudioSource>();
        source.Play();
    }
}
