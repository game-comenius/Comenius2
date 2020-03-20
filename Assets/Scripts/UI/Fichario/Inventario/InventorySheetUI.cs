using System;
using System.Linq;
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
    [SerializeField]
    private Animator animatorDestaqueDescricao;

    private Inventory inventory;

    private List<InventoryItemSlotUI> listOfSlots;

    private void Awake()
    {
        listOfSlots = new List<InventoryItemSlotUI>();
    }

    protected virtual IEnumerator Start ()
    {
        yield return new WaitUntil(() => Player.Instance);
        var player = Player.Instance;
        inventory = player.Inventory;
        DisplayItems(inventory.Items());
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

    public void HighlightDescription()
    {
        animatorDestaqueDescricao.SetTrigger("PulsarDestaque");
    }

    public virtual void ShowDescription(Item item)
    {
        var currentMissionID = Player.Instance.missionID;
        ItemDescriptionsInOneMission descriptions;
        switch (currentMissionID)
        {
            default:
                descriptions = item.DescriptionsInMission1;
                break;
            case 1:
                descriptions = item.DescriptionsInMission2;
                break;
            case 2:
                descriptions = item.DescriptionsInMission3;
                break;
        }
        descriptionBox.text = descriptions.StandardDescription;
    }

    private InventoryItemSlotUI FindItemSlot(ItemName itemName)
    {
        var slot = listOfSlots.Where((s) => s.Contains(itemName));
        return slot.FirstOrDefault();
    }

    public void DisplayItems(ICollection<Item> items)
    {
        // Apagar os itens atuais
        foreach (var slot in listOfSlots) Destroy(slot.gameObject);
        listOfSlots.Clear();

        // Mostrar os itens novamente
        // Primeiro, mostrar os itens base
        var baseItems = items.Where((i) => !i.IsAnUpgrade());
        foreach (var item in baseItems)
        {
            // Criar um slot para comportar o item
            GameObject slot = Instantiate(itemSlotPrefab, itemSlotList.transform);
            // Colocar informações do item no slot.
            var itemSlot = slot.GetComponent<InventoryItemSlotUI>();
            itemSlot.AddBaseItem(item);
            listOfSlots.Add(itemSlot);
        }

        // Segundo, mostrar os itens que são upgrades de itens base
        var upgrades = items.Where((i) => i.IsAnUpgrade());
        foreach (var upgrade in upgrades)
        {
            foreach (var baseItem in upgrade.UpgradeFrom)
            {
                var baseItemSlot = FindItemSlot(baseItem);
                if (baseItemSlot) baseItemSlot.AddUpgrade(upgrade);
            }
        }
    }
}
