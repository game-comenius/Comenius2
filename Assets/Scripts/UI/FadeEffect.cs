using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Classe FadeEffect como componente oferece fade in e out suave para imagens
public class FadeEffect : MonoBehaviour
{
    public static FadeEffect instance;

    // Por exemplo, fade in vai levar o alpha da imagem do 0 até MaxAlpha
    [HideInInspector]
    private float _maxAlpha;

    // Esta classe pode ser usada tanto como componente para GameObjects 2D
    // quanto para GameObjects de UI
    private SpriteRenderer spriteRenderer;
    private Image image;

    // Use this for initialization
    private void Awake()
    {
        //if (gameObject.tag == "fadeMenu") DontDestroyOnLoad(gameObject);

        instance = this;
    }

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        // Definir o campo Color e MaxAlpha e
        // deixar a imagem completamente transparente
        if (spriteRenderer != null)
        {
            var color = spriteRenderer.color;
            _maxAlpha = color.a;
            color.a = 0;
            spriteRenderer.color = color;
        }
        else if (image != null)
        {
            var color = image.color;
            _maxAlpha = color.a;
            color.a = 0;
            image.color = color;
        }

        // Se a imagem ja é completamente transparente, usar um valor padrão
        // para o MaxAlpha do efeito de fade
        _maxAlpha = (_maxAlpha > 0) ? _maxAlpha : 0.7f;
	}
	
    public IEnumerator Fade(float maxAlpha)
    {
        if (spriteRenderer != null)
        {
            var color = spriteRenderer.color;
            // Se Alpha > 0, fazer fade out, senão fazer fade in
            bool fadeOut = (color.a > 0);

            if (fadeOut)
            {
                for (float i = color.a; i >= 0; i -= Time.deltaTime)
                {
                    color.a = i;
                    spriteRenderer.color = color;
                    yield return null;
                }
                color.a = 0;
                spriteRenderer.color = color;
            }
            else
            {
                for (float i = color.a; i <= maxAlpha; i += Time.deltaTime)
                {
                    color.a = i;
                    spriteRenderer.color = color;
                    yield return null;
                }
                color.a = maxAlpha;
                spriteRenderer.color = color;
            }
        }
        else if (image != null)
        {
            var color = image.color;
            // Se Alpha > 0, fazer fade out, senão fazer fade in
            bool fadeOut = (color.a > 0);

            if (fadeOut)
            {
                for (float i = color.a; i >= 0; i -= Time.deltaTime)
                {
                    color.a = i;
                    image.color = color;
                    yield return null;
                }
                color.a = 0;
                image.color = color;
            }
            else
            {
                for (float i = color.a; i <= maxAlpha; i += Time.deltaTime)
                {
                    color.a = i;
                    image.color = color;
                    yield return null;
                }
                color.a = maxAlpha;
                image.color = color;
            }
        }
    }

    public void Fadeout() {
        StartCoroutine(Fade(_maxAlpha));
    }

    public void Fadeout(float maxAlpha) {
        StartCoroutine(Fade(maxAlpha));
    }

    public void Fadein() {
        StartCoroutine(Fade(_maxAlpha));
    }

    public void Fadein(float maxAlpha) {
        StartCoroutine(Fade(maxAlpha));
    }
}
