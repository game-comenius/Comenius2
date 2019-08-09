using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpriteDatabase : MonoBehaviour {

	// O banco de dados vai ter uma coleção de structs
	[SerializeField]
	private List<ItemNameAndItsSprite> itemNameSpriteList;

	[Serializable]
    private struct ItemNameAndItsSprite
    {
        public ItemName ItemName;
        public Sprite Sprite;

        ItemNameAndItsSprite(ItemName itemName, Sprite sprite)
        {
            ItemName = itemName;
            Sprite = sprite;
        }
	}

    public static ItemSpriteDatabase Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static Sprite GetSpriteOf(ItemName itemName)
    {
        var list = Instance.itemNameSpriteList;
        var element = list.Find(x => x.ItemName == itemName);
        return element.Sprite;
    }

    public static ItemName GetItemNameOf(Sprite sprite)
	{
        var list = Instance.itemNameSpriteList;
        var element = list.Find(x => x.Sprite == sprite);
        return element.ItemName;
	}
}
