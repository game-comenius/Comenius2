using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemName ItemName { get; private set; }
    public string FriendlyName { get; private set; }
    public Sprite Sprite { get; private set; }
    public string Description { get; private set; }


    public Item(ItemName itemName)
    {
        ItemName = itemName;
        Sprite = ItemSpriteDatabase.GetSpriteOf(ItemName);
        switch (itemName)
        {
            case ItemName.Livro:
                FriendlyName = "Livro";
                Description = "Descrição do Livro...";
                break;
            case ItemName.Cartazes:
                FriendlyName = "Cartazes";
                Description = "Descrição de Cartazes...";
                break;
            case ItemName.SemNome:
                FriendlyName = "Sem nome";
                Description = "Sem descrição";
                break;
            case ItemName.TV:
                FriendlyName = "TV";
                Description = "Uma TV";
                break;
        }
    }
}

[Serializable] public class Item2
{
    public ItemName ItemName;
}
