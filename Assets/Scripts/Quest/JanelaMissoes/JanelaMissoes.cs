﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaMissoes : MonoBehaviour {


    public bool Aberta { get; set; }
    private RectTransform transformJanela;
    private Vector2 posicaoFechada;
    private Vector2 posicaoAberta;

	// Use this for initialization
	void Start () {
        transformJanela = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        posicaoFechada = transformJanela.anchoredPosition;
        posicaoAberta = new Vector2(32, 0);
    }

    public void Toggle()
    {
        transformJanela.anchoredPosition = (Aberta) ? posicaoFechada : posicaoAberta;
        Aberta = !Aberta;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
