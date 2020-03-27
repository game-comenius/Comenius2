using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLurdinha : MonoBehaviour {

	private void Start () {
        StartCoroutine(UpdateInventory());
	}

    // Estabelecer novamente quais são os itens no inventário da Lurdinha
    // Os items do jogo tradicional serão deletados e os items escolhidos
    // pelo jogador no momento de criar o custom serão adicionados
    private IEnumerator UpdateInventory()
    {
        yield return new WaitUntil(() => Player.Instance != null);
        var player = Player.Instance;

        var settings = CustomGameSettings.CurrentSettings;

        var inventory = player.Inventory;
        if (inventory != null)
        {
            player.Inventory.Clear();
            var customItems = settings.MidiasDisponiveis();
            foreach (var item in customItems)
                player.Inventory.Add(item);
        }

        // Os itens não terão descrição específica de um momento no custom
        foreach (var item in player.Inventory.Items())
        {
            item.DescriptionsInMission1.FirstMomentDescription = item.DescriptionsInMission3.StandardDescription;
            item.DescriptionsInMission1.SecondMomentDescription = item.DescriptionsInMission3.StandardDescription;
            item.DescriptionsInMission1.ThirdMomentDescription = item.DescriptionsInMission3.StandardDescription;
        }
    }
}
