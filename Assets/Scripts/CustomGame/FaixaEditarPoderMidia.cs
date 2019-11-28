using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaixaEditarPoderMidia : MonoBehaviour {

    [HideInInspector]
    public ItemName Midia { get; private set; }

    public Poder Poder { get; private set; }

    [SerializeField]
    private Image midiaImage;
    [SerializeField]
    private TextMeshProUGUI placeholder;
    [SerializeField]
    private TextMeshProUGUI feedback;

    public void SetMidia(ItemName midia)
    {
        Midia = midia;
        midiaImage.sprite = ItemSpriteDatabase.GetSpriteOf(Midia);
        midiaImage.preserveAspect = true;   
    }

    public string Feedback()
    {
        if (!placeholder.enabled)
            return feedback.text;
        else
            return placeholder.text;
    }

    internal void RefreshFeedbackPlaceholder(Poder novoPoder)
    {
        Poder = novoPoder;
        string f = Midia.ToString() + " é uma mídia ";
        //string f = "É uma mídia ";
        switch (Poder)
        {
            case Poder.Fraca:
                f += "fraca";
                break;
            case Poder.Boa:
                f += "boa";
                break;
            case Poder.MuitoBoa:
                f += "muito boa";
                break;
            case Poder.Melhor:
                f += "excelente";
                break;
        }
        f += " para este momento da aula!";
        placeholder.SetText(f);
    }

    private void Start()
    {
        RefreshFeedbackPlaceholder(Poder.Fraca);
    }
}
