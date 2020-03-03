using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySheetUI : MonoBehaviour {

    [SerializeField]
    private GameObject itemSlotList;
    [SerializeField]
    private GameObject itemSlotPrefab;
    [SerializeField]
    protected TextMeshProUGUI descriptionBox;

    private List<GameObject> gameObjectSlots = new List<GameObject>();

    private Inventory inventory;


    protected virtual IEnumerator Start ()
    {
        yield return new WaitUntil(() => Player.Instance);
        var player = Player.Instance;
        inventory = player.Inventory;

        if (inventory.Items().Count > 0)
            DisplayItems(inventory.Items());
    }

    private InventoryItemSlotUI FindItemSlot(ItemName itemName)
    {
        foreach (var gameObjectSlot in gameObjectSlots)
        {
            var itemSlot = gameObjectSlot.GetComponent<InventoryItemSlotUI>();
            if (itemSlot.Contains(itemName))
                return itemSlot;
        }
        return null;
    }

    public void UnselectAllItems()
    {
        var selectedItemSlot = InventoryItemSlotUI.SelectedItemSlot;
        if (!selectedItemSlot) return;

        selectedItemSlot.Unselect();
        EmptyDescription();
    }

    public void EmptyDescription()
    {
        descriptionBox.text = "";
    }

    public virtual void ShowDescription(Item item)
    {
        descriptionBox.text = item.DescriptionsInMission1.StandardDescription;
    }

    public void DisplayItems(ICollection<Item> items)
    {
        var itemsEnumerator = items.GetEnumerator();
        var availableSlots = gameObjectSlots.Count;
        int i = 0;

        while (itemsEnumerator.MoveNext())
        {
            var item = itemsEnumerator.Current;

            // Se a mídia é uma mídia com upgrade, achar o slot das mídias
            // base e adicionar este upgrade a eles
            if (item.IsAnUpgrade())
            {
                foreach (var baseItem in item.UpgradeFrom)
                {
                    var baseItemSlot = FindItemSlot(baseItem);

                    if (baseItemSlot) baseItemSlot.AddUpgrade(item);
                }
                // Passe para o próximo item do while
                continue;
            }

            GameObject slot;

            // Se houver um slot vivo, usar ele. Se não houver, criar um.
            if (i < availableSlots)
            {
                slot = gameObjectSlots[i];
                i++;
            }
            else
            {
                slot = Instantiate(itemSlotPrefab, itemSlotList.transform);
                gameObjectSlots.Add(slot);
            }

            // Colocar informações do item no slot.
            var itemSlot = slot.GetComponent<InventoryItemSlotUI>();
            itemSlot.AddBaseItem(item);
        }
    }
}
