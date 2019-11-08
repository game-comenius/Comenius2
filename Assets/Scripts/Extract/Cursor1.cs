using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor1: MonoBehaviour
{
    [Header("Cursor Base")]
    public Texture2D cursorImage;
    [SerializeField] Vector2 offset = Vector2.zero;

    [Header("Cursor 2")]
    public Texture2D select;
    public CursorMode curmode = CursorMode.ForceSoftware;
    public Vector2 hotspot = Vector2.zero;


	void Start ()
    {
        Cursor.SetCursor(cursorImage, offset, CursorMode.ForceSoftware);
	}			

}
