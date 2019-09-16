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
    public CursorMode curmode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;


	void Start ()
    {
        Cursor.SetCursor(cursorImage, offset, CursorMode.Auto);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition) + 10 * Vector3.forward, 0.1f);
    }
}
