using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<ItemName, Item> items;

    public Inventory() {
        items = new Dictionary<ItemName, Item>();

        items.Add(ItemName.Cartazes, new Item(ItemName.Cartazes));
        items.Add(ItemName.Caderno, new Item(ItemName.Caderno));
        items.Add(ItemName.Jornais, new Item(ItemName.Jornais));
    }

    public ICollection<Item> Items()
    {
        return items.Values;
    }

    public bool Contains(ItemName itemName)
    {
        return items.ContainsKey(itemName);
    }

    public void Add(ItemName itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            items.Add(itemName, new Item(itemName));

            DisplayItems();
        }
    }

    private void DisplayItems()
    {
        var inventoryUIArray = Object.FindObjectsOfType<InventorySheetUI>();
        foreach (var inventoryUI in inventoryUIArray)
        {
            inventoryUI.DisplayItems(Items());
        }
    }


    public override string ToString()
    {
        var returnString = "";
        foreach (var item in items)
        {
            returnString += string.Format("[{0}, {1}], ", item.Value.FriendlyName, item.Value.Description);
        }
        return returnString.TrimEnd(' ', ',');
    }
}