using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory
{
    private Dictionary<ItemName, Item> items;

    private TextMeshProUGUI uiMidiaCounter;

    public Inventory() {
        items = new Dictionary<ItemName, Item>();

        // Itens iniciais
        items.Add(ItemName.Cartazes, new Item(ItemName.Cartazes));
        items.Add(ItemName.Jornais, new Item(ItemName.Jornais));
        items.Add(ItemName.LivroDidatico, new Item(ItemName.LivroDidatico));
        items.Add(ItemName.Caderno, new Item(ItemName.Caderno));
        items.Add(ItemName.QuadroNegro, new Item(ItemName.QuadroNegro));
    }

    public ICollection<Item> Items()
    {
        return items.Values;
    }

    public int Count { get { return items.Count; } }

    public void Clear()
    {
        items.Clear();

        uiMidiaCounter = GameObject.Find("ContadorMidias").GetComponent<TextMeshProUGUI>();
        uiMidiaCounter.SetText("Mídias Obtidas: " + Count + "/13");
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
            if (item.IsAnUpgrade())
            {
                var validUpgrade = false;
                foreach (var baseItem in item.UpgradeFrom)
                    validUpgrade = this.Contains(baseItem);

                if (!validUpgrade) return;
            }

            items.Add(itemName, item);

            uiMidiaCounter = GameObject.Find("ContadorMidias").GetComponent<TextMeshProUGUI>();
            uiMidiaCounter.SetText("Mídias Obtidas: " + Count + "/13");

            DisplayItems();
        }

    }

    public void Remove(ItemName itemName)
    {
        items.Remove(itemName);

        uiMidiaCounter = GameObject.Find("ContadorMidias").GetComponent<TextMeshProUGUI>();
        uiMidiaCounter.SetText("Mídias Obtidas: " + Count + "/13");
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
            returnString += string.Format("[{0}], ", item.Value.FriendlyName);
        }
        return returnString.TrimEnd(' ', ',');
    }
}