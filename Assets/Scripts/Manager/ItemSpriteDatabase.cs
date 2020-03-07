using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ItemSpriteDatabase : MonoBehaviour {

    //   [Serializable]
    //   public struct ItemNameAndItsSprite
    //   {
    //       public ItemName ItemName;
    //       public Sprite Sprite;
    //   }

    //   // O banco de dados vai ter uma coleção de structs
    //public List<ItemNameAndItsSprite> itemNameSpriteList;

    public Dictionary<ItemName, Sprite> dictionary;

    public ItemName MyItemName;
    public Sprite MySprite;
    public ItemName MyItemName2;
    public Sprite MySprite2;

    public Sprite[] spriteArray;
    public ItemName[] itemNameArray;


    private static ItemSpriteDatabase Instance;


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

        dictionary = new Dictionary<ItemName, Sprite>();
        for (var i = 0; i < itemNameArray.Length; i++)
        {
            Debug.Log("Adicionando " + itemNameArray[i] + " - " + spriteArray[i]);
            try
            {
                dictionary.Add(itemNameArray[i], spriteArray[i]);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
        foreach (var e in dictionary)
            Debug.Log(e.Key + " - " + e.Value);
    }

    private void Start()
    {
    }


    public static Sprite GetSpriteOf(ItemName itemName)
    {
        //var list = Instance.itemNameSpriteList;
        //var element = list.Find(x => x.ItemName == itemName);
        //return element.Sprite;
        Sprite sprite;
        var success = Instance.dictionary.TryGetValue(itemName, out sprite);
        return (success) ? sprite : null;
    }

    public static ItemName GetItemNameOf(Sprite sprite)
	{
        //var list = Instance.itemNameSpriteList;
        //var element = list.Find(x => x.Sprite == sprite);
        //return element.ItemName;
        return ItemName.TVComVHS;
	}

    public void Add(ItemName itemName, Sprite sprite)
    {
        //itemNameSpriteList.Add(new ItemNameAndItsSprite(itemName, sprite));
    }
}
