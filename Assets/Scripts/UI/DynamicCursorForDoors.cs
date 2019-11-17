using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicCursorForDoors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Texture2D cursorImage;
    private Texture2D select;
    private CursorMode curmode;
    private Vector2 hotspot;

    private static Color selectedColor = new Color(1, 1, 1, 1);
    private static Color unselectedColor = new Color(1, 1, 1, 0.4f);
    private SpriteRenderer spriteRenderer;

    public void OnMouseEnter()
    {        
        Cursor.SetCursor(select, hotspot, curmode);
        if (spriteRenderer) spriteRenderer.color = selectedColor;
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorImage, hotspot, curmode);
        if (spriteRenderer) spriteRenderer.color = unselectedColor;
    }

    // Use this for initialization
    void Start ()
    {
        cursorImage = GameObject.FindObjectOfType<Cursor1>().cursorImage;
        select = GameObject.FindObjectOfType<Cursor1>().select;
        curmode = GameObject.FindObjectOfType<Cursor1>().curmode;
        hotspot = GameObject.FindObjectOfType<Cursor1>().hotspot;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer) spriteRenderer.color = unselectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(select, hotspot, curmode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorImage, hotspot, curmode);
    }
}
