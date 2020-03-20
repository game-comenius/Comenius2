using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSpriteDatabase : MonoBehaviour {

    [Serializable]
    public struct PlaceSprite
    {
        public SalaDeAula sala;
        public Sprite sprite;
    }

    [SerializeField]
    public List<PlaceSprite> SalaSprites;

    private static readonly object simpleLock = new object();
    private static PlaceSpriteDatabase instance;
    public static PlaceSpriteDatabase Instance
    {
        get
        {
            lock (simpleLock)
            {
                if (instance == null)
                {
                    // Search for existing instance
                    instance = FindObjectOfType<PlaceSpriteDatabase>();

                    if (instance == null)
                        Debug.Log("Não há " + typeof(PlaceSpriteDatabase) + " nesta cena!");
                    else
                        DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Sprite SpriteOf(SalaDeAula sala)
    {
        var list = Instance.SalaSprites;
        var element = list.Find(x => x.sala == sala);
        return element.sprite;
    }
}
