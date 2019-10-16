using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<ItemName, Item> items;

    public Inventory() {
        items = new Dictionary<ItemName, Item>();

        // Itens iniciais
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
        if (!this.Contains(itemName))
        {
            var item = new Item(itemName);

            // Se é um upgrade mas o jogador não possui o item base para
            // realizar o upgrade, aborta esta adição de item ao inventário
            if (item.IsUpgrade())
            {
                var validUpgrade = false;
                foreach (var baseItem in item.UpgradeFrom)
                    validUpgrade = this.Contains(baseItem);

                if (!validUpgrade) return;
            }

            items.Add(itemName, item);

            DisplayItems();
        }

        if (QuestScript.questList.Count != 0)
        {
            QuestScript.questList[0].ReavaliarTodasQuests();
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


    // Para testes usando o console
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