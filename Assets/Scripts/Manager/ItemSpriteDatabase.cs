using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ItemSpriteDatabase : MonoBehaviour {

    private Dictionary<ItemName, Sprite> dictionary;

    // 2 campos que auxiliam a classe ItemSpriteDatabaseEditor, custom editor
    // desta classe, a adicionar as duplas (ItemName, Sprite) ao dicionário,
    // portanto... Por favor, não excluir estes 2, eles são necessários
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

        // Pegar os sprites vinculados aos ItemName's pelo Inspector de
        // ItemSpriteDatabase e adicionar eles no dicionário desta classe
        dictionary = new Dictionary<ItemName, Sprite>();
        for (var i = 0; i < itemNameArray.Length; i++)
        {
            try { dictionary.Add(itemNameArray[i], spriteArray[i]); }
            catch (Exception ex) { /* Aceitar a Exception */ }
        }
    }


    public static Sprite GetSpriteOf(ItemName itemName)
    {
        Sprite sprite;
        var success = Instance.dictionary.TryGetValue(itemName, out sprite);
        if (!success)
        {
            Debug.LogError("Não existe sprite vinculado ao ItemName " + itemName.ToString());
            return null;
        }

        return sprite;
    }
}
