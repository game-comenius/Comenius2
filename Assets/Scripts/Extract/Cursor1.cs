using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor1: MonoBehaviour {
    public Texture2D select;
    public Texture2D cursorImage;
    public CursorMode curmode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;


	void Start ()
    {
		Cursor.SetCursor  (cursorImage,Vector2.zero,CursorMode.Auto);
	}			

    void OnMouseEnter()
    {
        if (gameObject.tag == "usavel")
        {
            Cursor.SetCursor(select, hotspot, curmode);
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorImage, hotspot, curmode);
    }
}
