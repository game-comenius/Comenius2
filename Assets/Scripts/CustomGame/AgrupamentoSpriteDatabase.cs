using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgrupamentoSpriteDatabase : MonoBehaviour
{

    [System.Serializable]
    private struct AgrupamentoSprite
    {
        public Agrupamento agrupamento;
        public Sprite sprite;
    }

    [SerializeField]
    private List<AgrupamentoSprite> procedimentosSprites;

    private static AgrupamentoSpriteDatabase Instance;

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

    public static Sprite SpriteOf(Agrupamento agrupamento)
    {
        var list = Instance.procedimentosSprites;
        var element = list.Find(x => x.agrupamento == agrupamento);
        return element.sprite;
    }
}