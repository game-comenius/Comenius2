using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlanejamentoOnDropHandler : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        var draggedObject = eventData.pointerDrag;
        if (draggedObject)
        {
            var itemInUI = draggedObject.GetComponent<ItemInUserInterface>();
            if (itemInUI)
                GetComponentInChildren<MidiaMomento>().AddItem(itemInUI.ItemName);
        }
    }
}
