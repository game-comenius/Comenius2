using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedimentoSpriteDatabase : MonoBehaviour {

    [System.Serializable]
    private struct ProcedimentoSprite
    {
        public Procedimento procedimento;
        public Sprite sprite;
    }

    [SerializeField]
    private List<ProcedimentoSprite> procedimentosSprites;

    private static ProcedimentoSpriteDatabase Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Sprite SpriteOf(Procedimento procedimento)
    {
        var list = Instance.procedimentosSprites;
        var element = list.Find(x => x.procedimento == procedimento);
        return element.sprite;
    }
}