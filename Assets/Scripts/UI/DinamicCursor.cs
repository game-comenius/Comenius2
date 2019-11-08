using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DinamicCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private Texture2D cursorImage;
    private Vector2 offset = Vector2.zero;
    private Texture2D select;
    private CursorMode curmode;
    private Vector2 hotspot;

    public void OnMouseEnter()
    {
        if (gameObject.tag == "usavel")
        {
            Cursor.SetCursor(select, hotspot, curmode);
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorImage, hotspot, curmode);
    }

    // Use this for initialization
    void Start () {
        cursorImage = GameObject.FindObjectOfType<Cursor1>().cursorImage;
        select = GameObject.FindObjectOfType<Cursor1>().select;
        curmode = GameObject.FindObjectOfType<Cursor1>().curmode;
        hotspot = GameObject.FindObjectOfType<Cursor1>().hotspot;
    }
	
	// Update is called once per frame
	void Update () {
		
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
