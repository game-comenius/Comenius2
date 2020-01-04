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

    private static PlaceSpriteDatabase Instance;

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

    public static Sprite SpriteOf(SalaDeAula sala)
    {
        var list = Instance.SalaSprites;
        var element = list.Find(x => x.sala == sala);
        return element.sprite;
    }
}
