using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MediaAnimation : MonoBehaviour {

    public Animator animator;

    public void exitAnimation()
    {        
        animator.SetBool("Exit", true);
    }

    public void endFanfare()
    {
        // Fadeout em todos os fades ativos da cena
        var fades = FindObjectsOfType<FadeEffect>();
        foreach (var fade in fades)
        {
            var image = fade.GetComponent<Image>();
            var sp = fade.GetComponent<SpriteRenderer>();
            if (image && image.color.a > 0) fade.Fadeout();
            else if (sp && sp.color.a > 0) fade.Fadeout();
        }

        GameObject.Find("MediaAnimationCanvas").SetActive(false);
    }


    public void UINaoSendoUsada()
    {
        GameManager.UINaoSendoUsada();
    }
}
