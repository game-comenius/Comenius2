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
    }
}
