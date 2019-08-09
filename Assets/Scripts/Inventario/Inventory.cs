using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<ItemName, Item> items;

    public Inventory() {
        items = new Dictionary<ItemName, Item>();
    }

    public ICollection<Item> Items()
    {
        return items.Values;
    }

    public void Add(ItemName itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            items.Add(itemName, new Item(itemName));

            var inventoryUI = Object.FindObjectOfType<InventorySheetUI>();
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