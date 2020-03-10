using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    public static InventoryItemSlotUI SelectedItemSlot;

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
    [SerializeField]
    private TextMeshProUGUI ItemNameTextBox;

    // GameObject that can render an item on the UI
    private ItemInUserInterface itemInUserInterface;

    //Parent
    private InventorySheetUI inventoryUI;


    // Data
    private LinkedList<Item> myItems;
    private LinkedListNode<Item> myCurrentItem;
    

    private void Awake()
    {
        myItems = new LinkedList<Item>();

        itemInUserInterface = GetComponentInChildren<ItemInUserInterface>();

        inventoryUI = GetComponentInParent<InventorySheetUI>();
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
        itemInUserInterface.ItemName = item.ItemName;

        // Colocar o nome do item no slot.
        ItemNameTextBox.text = item.FriendlyName;
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
        HandlePointerDown();
    }

    public void DisplayNextItem()
    {
        myCurrentItem = myCurrentItem.Next ?? myItems.First;
        DisplayInfo();
        HandlePointerDown();
    }

    public void Unselect()
    {
        if (SelectedItemSlot != this) return;

        SelectedItemSlot.myItemBorder.sprite = neutralItemBorder;
    }
    

    private void HandlePointerDown()
    {
        // Desabilitar o último destaque de slot de item
        if (SelectedItemSlot)
            SelectedItemSlot.myItemBorder.sprite = neutralItemBorder;
        // Habilitar o destaque para este item
        myItemBorder.sprite = selectedItemBorder;
        // O slot com a borda ativa/selecionada agora é este item
        SelectedItemSlot = this;

        var myItem = myCurrentItem.Value;
        // Colocar a descrição na caixa de descrição
        // O tab no início da descrição é para "escapar" do clip
        // do canto da folha do inventário
        inventoryUI.ShowDescription(myItem);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HandlePointerDown();
    }
}
