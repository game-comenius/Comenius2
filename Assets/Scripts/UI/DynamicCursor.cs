﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Texture2D cursorImage;
    private Texture2D select;
    private CursorMode curmode;
    private Vector2 hotspot;

    private bool pointerIn = false;

    public void OnMouseEnter()
    {        
        Cursor.SetCursor(select, hotspot, curmode);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorImage, hotspot, curmode);
    }

    // Use this for initialization
    void Start ()
    {
        cursorImage = GameObject.FindObjectOfType<Cursor1>().cursorImage;
        select = GameObject.FindObjectOfType<Cursor1>().select;
        curmode = GameObject.FindObjectOfType<Cursor1>().curmode;
        hotspot = GameObject.FindObjectOfType<Cursor1>().hotspot;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerIn = true;

        Cursor.SetCursor(select, hotspot, curmode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIn = false;
        Cursor.SetCursor(cursorImage, hotspot, curmode);
    }

    public void OnDisable()
    {
        if (pointerIn)
        {
            Cursor.SetCursor(GameObject.FindObjectOfType<Cursor1>().cursorImage, GameObject.FindObjectOfType<Cursor1>().hotspot, GameObject.FindObjectOfType<Cursor1>().curmode);
        }
    }
}
