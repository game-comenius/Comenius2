using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolhaInventarioNoPlanejamento : InventorySheetUI
{
    private Planejamento planejamento;

    protected override IEnumerator Start()
    {
        yield return base.Start();

        planejamento = GetComponentInParent<Planejamento>();
    }

    public void PermitirDragAndDrop()
    {
        // Fazer com que o jogador consiga arrastar os items do inventário
        // durante e para o planejamento
        var items = GetComponentsInChildren<ItemInUserInterface>();
        foreach (var item in items)
        {
            if (!item.GetComponent<DragDrop>())
                item.gameObject.AddComponent<DragDrop>();
        }
    }

    // Substitui o método ShowDescription do pai InventorySheetUI
    public override void ShowDescription(Item item)
    {
        ItemDescriptionsInOneMission descriptions;
        switch (Player.Instance.missionID)
        {
            
            case 1:
                descriptions = item.DescriptionsInMission2;
                break;
            case 2:
                descriptions = item.DescriptionsInMission3;
                break;
            default:
                descriptions = item.DescriptionsInMission1;
                break;
        }

        if (planejamento.Momento2Confirmado)
            descriptionBox.text = descriptions.ThirdMomentDescription;
        else if (planejamento.Momento1Confirmado)
            descriptionBox.text = descriptions.SecondMomentDescription;
        else
            descriptionBox.text = descriptions.FirstMomentDescription;

    }
}
