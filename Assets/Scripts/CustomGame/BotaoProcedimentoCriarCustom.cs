using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotaoProcedimentoCriarCustom : MonoBehaviour, IPointerClickHandler
{
    private Procedimento procedimento;
    public Procedimento Procedimento
    {
        get
        {
            return procedimento;
        }

        set
        {
            procedimento = value;
            UpdateSprite();
        }
    }

    private MomentoUICriarCustom meuMomento;

    // UI fields
    private Image image;
    private static Color selectedColor = new Color(1, 1, 1, 1);
    private static Color unselectedColor = new Color(.5f, .5f, .5f, .6f);

    void Awake () {
        image = GetComponent<Image>();
        // Começa não selecionado
        UpdateColor(false);
	}

    private void UpdateSprite()
    {
        image.sprite = ProcedimentoSpriteDatabase.SpriteOf(procedimento);
        image.preserveAspect = true;
    }

    public void UpdateColor(bool selected)
    {
        image.color = selected ? selectedColor : unselectedColor;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        meuMomento.SelecionarProcedimento(this);
    }

    public void CadastrarMeuMomento(MomentoUICriarCustom meuMomento)
    {
        this.meuMomento = meuMomento;
    }
}
