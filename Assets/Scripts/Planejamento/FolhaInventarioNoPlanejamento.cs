using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhaInventarioNoPlanejamento : InventorySheetUI
{
    protected override IEnumerator Start()
    {
        yield return base.Start();

        // Fazer com que o jogador consiga arrastar os items do inventário
        // durante e para o planejamento
        var items = GetComponentsInChildren<ItemInUserInterface>();
        foreach (var item in items)
        {
            if (!item.GetComponent<DragDrop>())
                item.gameObject.AddComponent<DragDrop>();
        }
    }
}
