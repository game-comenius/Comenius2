using System;
using UnityEngine;

public class CursorInfos: MonoBehaviour
{
    [Serializable]
    private class CursorClass
    {
        public Texture2D texture = null;
        public Vector2 offset = Vector2.zero;
        public CursorMode mode = CursorMode.Auto;
    }

    private static CursorInfos instance;

    [SerializeField] private CursorClass cursorBase = new CursorClass();

    [SerializeField] private CursorClass cursorInterativo = new CursorClass();

    //[Header("Cursor Base")]
    //public Texture2D cursorImage;
    //[SerializeField] Vector2 offset = Vector2.zero;

    //[Header("Cursor Iterativo")]
    //public Texture2D select;
    //public CursorMode curmode = CursorMode.ForceSoftware;
    //public Vector2 hotspot = Vector2.zero;

    private void Awake()
    {
#if UNITY_EDITOR
        if (instance != null)
        {
            Debug.Log("Tem mais de um CursorInfos");
        }
#endif 

        instance = this;
    }

	private void Start ()
    {
        SetCursorBase();
    }

    public static void SetCursorBase()
    {
        Cursor.SetCursor(instance.cursorBase.texture, instance.cursorBase.offset, instance.cursorBase.mode);
    }

    public static void SetCursorInterativo()
    {
        Cursor.SetCursor(instance.cursorInterativo.texture, instance.cursorInterativo.offset, instance.cursorInterativo.mode);
    }
}
