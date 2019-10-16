using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySheetUI : MonoBehaviour {

    [SerializeField]
    private GameObject itemSlotList;
    [SerializeField]
    private GameObject itemSlotPrefab;

    private List<GameObject> gameObjectSlots;

    private Inventory inventory;

    public void Start ()
    {
        gameObjectSlots = new List<GameObject>();

        StartCoroutine(FindPlayer());
    }

    //private void OnEnable()
    //{
    //    inventory = Player.Instance.Inventory;

    //    if (inventory != null && inventory.Items().Count > 0)
    //        DisplayItems(inventory.Items());
    //}

    private IEnumerator FindPlayer()
    {
        yield return new WaitWhile(() => Player.Instance == null);

        inventory = Player.Instance.Inventory;

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
            if (item.IsUpgrade())
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
            itemSlot.DescriptionSlot = transform.GetChild(0).gameObject;

            itemSlot.AddBaseItem(item);
        }
    }
}
