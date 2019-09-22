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

    private List<GameObject> itemSlots;

    private Inventory inventory;

    public void Start ()
    {
        itemSlots = new List<GameObject>();

        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        yield return new WaitWhile(() => Player.Instance == null);

        inventory = Player.Instance.Inventory;

        if (inventory.Items().Count > 0)
            DisplayItems(inventory.Items());
    }

    public void DisplayItems(ICollection<Item> items)
    {
        var itemsEnumerator = items.GetEnumerator();
        var availableSlots = itemSlots.Count;
        int i = 0;

        while (itemsEnumerator.MoveNext())
        {
            GameObject slot;

            // Se houver um slot vivo, usar ele. Se não houver, criar um.
            if (i < availableSlots)
            {
                slot = itemSlots[i];
                i++;
            }
            else
            {
                slot = Instantiate(itemSlotPrefab, itemSlotList.transform);
                itemSlots.Add(slot);
            }

            // Colocar informações do item no slot.
            var itemSlotScript = slot.GetComponent<InventoryItemSlotUI>();
            itemSlotScript.DescriptionSlot = transform.GetChild(0).gameObject;
            var item = itemsEnumerator.Current;
            itemSlotScript.SetItemInfoInSlot(item);
        }
    }
}
