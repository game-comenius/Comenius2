using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItemSlotUI : MonoBehaviour, IPointerClickHandler {

    //private static Image enabledItemBorder;

    private LinkedList<Item> myItems;
    private LinkedListNode<Item> myCurrentItem;

    public GameObject DescriptionSlot { get; set; }
    [SerializeField]
    private Image imageComponent;

    private void Awake()
    {
        myItems = new LinkedList<Item>();
    }


    public bool Contains(ItemName itemName)
    {
        foreach (var myItem in myItems)
            if (myItem.ItemName == itemName) return true;

        return false;
    }

    private void DisplayInfo()
    {
        var item = myCurrentItem.Value;

        // Colocar o sprite do item no slot.
        // Referência para o GameObject que representa a imagem de um item
        // var image = transform.GetChild(0);
        // Referência para o componente Image do item no inventário
        //var image = imageObject.GetComponent<Image>();
        imageComponent.sprite = item.Sprite;

        // Colocar o nome do item no slot.
        var slotText = GetComponentInChildren<Text>();
        slotText.text = item.FriendlyName;
    }

    public void AddBaseItem(Item item)
    {
        if (myItems.Count == 0)
            myCurrentItem = myItems.AddLast(item);

        DisplayInfo();
    }

    public void AddUpgrade(Item item)
    {
        if (!myItems.Contains(item)) myItems.AddLast(item);
    }

    public void DisplayPreviousItem()
    {
        myCurrentItem = myCurrentItem.Previous ?? myItems.Last;
        DisplayInfo();
        DisplayDescription();
    }

    public void DisplayNextItem()
    {
        myCurrentItem = myCurrentItem.Next ?? myItems.First;
        DisplayInfo();
        DisplayDescription();
    }

    public void DisplayDescription()
    {
        var item = myCurrentItem.Value;
        // Colocar a descrição na caixa de descrição
        DescriptionSlot.GetComponent<Text>().text = item.Description;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Desabilitar o último destaque de slot de item
        //if (enabledItemBorder) enabledItemBorder.enabled = false;
        // Habilitar o destaque para este item
        //enabledItemBorder = transform.GetChild(0).GetComponent<Image>();
        //enabledItemBorder.enabled = true;
        DisplayDescription();

        // Caso esta folha de inventário esteja sendo usada em um planejamento
        var item = myCurrentItem.Value;
        GameObject selectedMoment = null;
        var planManager = FindObjectOfType<PlanManager>();
        if (planManager)
            selectedMoment = planManager.getSelectedMoment();
        if (selectedMoment)
            selectedMoment.GetComponent<MidiaMomento>().AddItem(item.ItemName);
    }
}
