using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLurdinha : MonoBehaviour {

    CustomGameSettings settings;

	// Use this for initialization
	private IEnumerator Start () {
        settings = CustomGameSettings.LoadCustomGameSettings();

        yield return StartCoroutine(UpdateInventory());
	}

    // Estabelecer novamente quais são os itens no inventário da Lurdinha
    // Os items do jogo tradicional serão deletados e os items escolhidos
    // pelo jogador no momento de criar o custom serão adicionados
    private IEnumerator UpdateInventory()
    {
        // Esperar um tempo para que o script Player inicialize o inventário
        var seconds = 0.05f;
        yield return new WaitForSeconds(seconds);

        var updated = false;
        try
        {
            var inventory = Player.Instance.Inventory;
            if (inventory != null)
            {
                inventory.Clear();
                var customItems = settings.MidiasDisponiveis();
                foreach (var item in customItems)
                    inventory.Add(item);
                updated = true;
            }
        }
        catch (System.Exception) {}
        if (!updated) StartCoroutine(UpdateInventory());
    }
}
