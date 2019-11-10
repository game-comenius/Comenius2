using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItemSlotUI : MonoBehaviour, IPointerClickHandler {

    private static Image activeItemBorder;

    [SerializeField]
    private Image myItemBorder;
    [SerializeField]
    private Sprite selectedItemBorder;
    [SerializeField]
    private Sprite neutralItemBorder;

    [SerializeField]
    private GameObject previousItemButton;
    [SerializeField]
    private GameObject nextItemButton;

    public GameObject DescriptionSlot { get; set; }

    [SerializeField]
    private Image imageComponent;


    private LinkedList<Item> myItems;
    private LinkedListNode<Item> myCurrentItem;


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
        imageComponent.sprite = item.Sprite;

        // Colocar o nome do item no slot.
        var slotText = GetComponentInChildren<Text>();
        slotText.text = item.FriendlyName;
    }

    public void AddBaseItem(Item item)
    {
        if (myItems.Count == 0)
            myCurrentItem = myItems.AddLast(item);

        previousItemButton.SetActive(false);
        nextItemButton.SetActive(false);

        DisplayInfo();
    }

    public void AddUpgrade(Item item)
    {
        if (!myItems.Contains(item)) myItems.AddLast(item);

        previousItemButton.SetActive(true);
        nextItemButton.SetActive(true);
    }

    public void DisplayPreviousItem()
    {
        myCurrentItem = myCurrentItem.Previous ?? myItems.Last;
        DisplayInfo();
        HandleClick();
    }

    public void DisplayNextItem()
    {
        myCurrentItem = myCurrentItem.Next ?? myItems.First;
        DisplayInfo();
        HandleClick();
    }

    

    private void HandleClick()
    {
        // Desabilitar o último destaque de slot de item
        if (activeItemBorder) activeItemBorder.sprite = neutralItemBorder;
        // Habilitar o destaque para este item
        myItemBorder.sprite = selectedItemBorder;
        // A borda ativa/selecionada agora é a borda deste item
        activeItemBorder = myItemBorder;

        var item = myCurrentItem.Value;
        // Colocar a descrição na caixa de descrição
        DescriptionSlot.GetComponent<Text>().text = item.Description;

        // Caso esta folha de inventário esteja sendo usada em um planejamento
        GameObject selectedMoment = null;
        var planManager = FindObjectOfType<PlanManager>();
        if (planManager)
            selectedMoment = planManager.getSelectedMoment();
        if (selectedMoment)
            selectedMoment.GetComponent<MidiaMomento>().AddItem(item.ItemName);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        HandleClick();
    }
}
