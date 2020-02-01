using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaAnimation : MonoBehaviour {

    public Animator animator;

    public void exitAnimation()
    {        
        animator.SetBool("Exit", true);
    }

    public void endFanfare()
    {
        GameObject.Find("Fade").GetComponent<FadeEffect>().Fadein();
        GameObject.Find("MediaAnimationCanvas").SetActive(false);
    }


    public void UINaoSendoUsada()
    {
        GameManager.UINaoSendoUsada();
    }
}
