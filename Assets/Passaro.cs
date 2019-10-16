using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passaro : MonoBehaviour {

    private bool a;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!a)
        {
            Player.Instance.Inventory.Add(ItemName.Gravador);
            Player.Instance.Inventory.Add(ItemName.GravacaoPassaro);
            a = true;
        }
	}
}
