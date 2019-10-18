using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour {

    bool faded = false;
    Color tmp;

    // Use this for initialization
    void Start () {
        tmp = this.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        this.GetComponent<SpriteRenderer>().color = tmp;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (faded) {
            while (tmp.a < 0.7f) {
                tmp.a += 0.001f;
                this.GetComponent<SpriteRenderer>().color = tmp;
                //Debug.Log("alpha = " + tmp.a);
            }
            tmp.a = 0.7f;
        }
        else {
            while (tmp.a > 0f)
            {
                tmp.a -= 0.001f;
                this.GetComponent<SpriteRenderer>().color = tmp;
                //Debug.Log("alpha = " + tmp.a);
            }
            tmp.a = 0f;
        }
	}

    public void Fadeout() {
        //this.GetComponent<SpriteRenderer>().enabled = true;
        faded = true;
    }

    public void Fadein() {
        //this.GetComponent<SpriteRenderer>().enabled = false;
        faded = false;
    }
}
