using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script para ser utilizado com objetos simples da UI que são apenas a
// representação visual de um item do jogo, e.g., mídia
public class ItemInUserInterface : MonoBehaviour {

    [SerializeField]
    private ItemName itemName;
    public ItemName ItemName
    {
        get { return itemName; }
        set
        {
            itemName = value;
            UpdateSprite();
        }
    }

    private Image image;


    private void Awake ()
    {
        image = GetComponent<Image>();
        image.preserveAspect = true;
	}

    private void Start()
    {
        // Se o item for definido através do inspector, já atualizar o sprite
        if (itemName != ItemName.SemNome) UpdateSprite();
    }

    private void UpdateSprite()
    {
        image.sprite = ItemSpriteDatabase.GetSpriteOf(itemName);
    }
}
