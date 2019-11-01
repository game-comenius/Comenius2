using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{

    Color tmp;

    // Use this for initialization
    private void Awake()
    {
        if (gameObject.tag == "fadeMenu")
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        tmp = this.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        this.GetComponent<SpriteRenderer>().color = tmp;
        
	}
	
    IEnumerator FadeBackground(bool faded)
    {
        if (faded) {
            for (float i = 0.7f; i >= 0; i -= Time.deltaTime) {
                tmp.a = i;
                this.GetComponent<SpriteRenderer>().color = tmp;
                yield return null;
            }
        } else {
            for (float i = 0; i <= 0.7f; i += Time.deltaTime) {
                tmp.a = i;
                this.GetComponent<SpriteRenderer>().color = tmp;
                yield return null;
            }
        }
    }

    public void Fadeout() {
        StartCoroutine(FadeBackground(false));
    }

    public void Fadein() {
        StartCoroutine(FadeBackground(true));
    }
}
