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
        GameObject.Find("LocalFade").GetComponent<FadeEffect>().Fadein();
        GameObject.Find("MediaAnimationCanvas").SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
