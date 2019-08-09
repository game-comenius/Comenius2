using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueImage : MonoBehaviour {

    public Sprite image;
  //  DialogueSystem dialogueSystem = new DialogueSystem();
    public float alphaLevel = .5f;

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update ()
    {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
	}
}
