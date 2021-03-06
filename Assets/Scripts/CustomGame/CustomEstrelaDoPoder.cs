﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomEstrelaDoPoder : MonoBehaviour, IPointerClickHandler
{
    public bool Selected { get; private set; }

    private Poder poder;

    [SerializeField]
    private Sprite spriteEstrelaVazia;
    [SerializeField]
    private Sprite spriteEstrelaCheia;

    private Image image;
    private CustomEstrelaDoPoder[] estrelas;
    private CustomEstrelaDoPoder[] estrelasAntesDeMim;

    private FaixaEditarPoderMidia MinhaFaixa;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        MinhaFaixa = GetComponentInParent<FaixaEditarPoderMidia>();

        var p = transform.parent;
        estrelas = new CustomEstrelaDoPoder[p.childCount];
        var posicaoEntreEstrelas = transform.GetSiblingIndex();
        estrelasAntesDeMim = new CustomEstrelaDoPoder[posicaoEntreEstrelas];
        for (int i = 0; i < estrelas.Length; i++)
        {
            var estrela = p.GetChild(i).GetComponent<CustomEstrelaDoPoder>();
            if (i < posicaoEntreEstrelas)
                estrelasAntesDeMim[i] = estrela;

            estrelas[i] = estrela;
        }

        poder = (Poder) posicaoEntreEstrelas;

        // Se for a primeira estrela, já começa selecionada
        if (posicaoEntreEstrelas == 0)
            Select(true);
        else
            Select(false);
	}

    public void Select(bool value)
    {
        Selected = value;
        image.sprite = Selected ? spriteEstrelaCheia : spriteEstrelaVazia;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var estrela in estrelas)
            estrela.Select(false);
        foreach (var estrela in estrelasAntesDeMim)
            estrela.Select(true);

        Select(!Selected);

        MinhaFaixa.RefreshFeedbackPlaceholder(poder);
    }
}
