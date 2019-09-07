using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemSlotUI : MonoBehaviour, IPointerClickHandler {

    private static Image enabledItemBorder;

    protected Item myItem;

    public GameObject DescriptionSlot { get; set; }

    public void SetItemInfoInSlot(Item item)
    {
        myItem = item;
        // Colocar o sprite do item no slot.
        // Referência para o GameObject que representa a imagem de um item
        var imageObject = transform.GetChild(0).GetChild(0);
        // Referência para o componente Image do item no inventário
        var image = imageObject.GetComponent<Image>();
        image.sprite = item.Sprite;

        // Colocar o nome do item no slot.
        var slotText = GetComponentInChildren<Text>();
        slotText.text = item.FriendlyName;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Desabilitar o último destaque de slot de item
        if (enabledItemBorder) enabledItemBorder.enabled = false;
        // Habilitar o destaque para este item
        enabledItemBorder = transform.GetChild(0).GetComponent<Image>();
        enabledItemBorder.enabled = true;

        // Colocar a descrição na caixa de descrição
        DescriptionSlot.GetComponent<Text>().text = myItem.Description;

        // Caso esta folha de inventário esteja sendo usada em um planejamento
        GameObject selectedMoment = null;
        var planManager = FindObjectOfType<PlanManager>();
        if (planManager)
            selectedMoment = planManager.getSelectedMoment();
        if (selectedMoment)
            selectedMoment.GetComponent<MidiaMomento>().AddItem(this.myItem.ItemName);
    }
}
