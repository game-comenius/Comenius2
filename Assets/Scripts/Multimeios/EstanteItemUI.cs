﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EstanteItemUI : MonoBehaviour, IPointerClickHandler {

    private Image imageComponent;

    public Estante Estante { get; set; }

    [SerializeField]
    private int[] questsIndex = new int[0];

    private ItemName item;
    public ItemName Item
    {
        get { return item; }

        set
        {
            item = value;
            imageComponent.sprite = ItemSpriteDatabase.GetSpriteOf(item);
            
            gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        imageComponent = gameObject.GetComponent<Image>();
        imageComponent.preserveAspect = true;
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var clickedItem = Item;
        Estante.Remove(clickedItem);
        Player.Instance.Inventory.Add(clickedItem);

        foreach (int index in questsIndex)
        {
            ManagerQuest.QuestTakeStep(index);
        }

        //if (QuestScript.questList.Count != 0)
        //{
        //    QuestScript.questList[0].ReavaliarTodasQuests();
        //}
    }
}
