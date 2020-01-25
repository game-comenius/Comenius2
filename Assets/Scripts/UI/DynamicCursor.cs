using System.Collections;
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
    protected virtual void OnMouseEnter()
    {
        if (!GameManager.uiSendoUsada)
        {
            CursorInfos.SetCursorInterativo();
        }
    }

    protected virtual void OnMouseExit()
    {
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
