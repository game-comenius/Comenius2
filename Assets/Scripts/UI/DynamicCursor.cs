﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private bool pointerIn = false;

    protected virtual void OnEnable()
    {
        GameManager.uiSendoUsadaEvent += OnMouseExit;
    }

    //Para o mundo do jogo.
    private void OnMouseEnter()
    {
        if (!GameManager.uiSendoUsada)
        {
            MouseIn();
        }
    }

    protected virtual void MouseIn()
    {
        pointerIn = true;

        CursorInfos.SetCursorInterativo();
    }

    private void OnMouseExit()
    {
        if (!GameManager.uiSendoUsada)
        {
            MouseOut();
        }
    }

    protected virtual void MouseOut()
    {
        pointerIn = false;

        CursorInfos.SetCursorBase();
    }


    //Para UI.
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        pointerIn = true;

        CursorInfos.SetCursorInterativo();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        pointerIn = false;

        CursorInfos.SetCursorBase();
    }

    protected virtual void OnDisable()
    {
        GameManager.uiSendoUsadaEvent -= OnMouseExit;

        if (pointerIn)
        {
            CursorInfos.SetCursorBase();
        }
    }
}
