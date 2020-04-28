using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPortaSalaProfessores : MonoBehaviour {

    private Planejamento planejamento;

	private IEnumerator Start () {
        // Truque para que a Lurdinha comece no lugar certo na sala de aula
        // porque este script vai enganar o jogo e a Lurdinha vai ignorar
        // o pátio quando sair da sala dos professores
        yield return new WaitUntil(() => Player.Instance != null);
        Player.Instance.sceneName = "M1_Patio1_Dia";

        var doors = FindObjectsOfType<DoorTransition>();
        foreach (var door in doors)
        {
            // Trocar o destino de todas as portas na sala dos professores para
            // a sala de aula selecionada pelo criador do jogo custom
            door.sceneName = "CustomSalaDeAula";

            // Desabilitar a porta até que o jogador confirme o planejamento
            door.GetComponent<PolygonCollider2D>().enabled = false;
            FindObjectOfType<Planejamento>().QuandoConfirmarPlanejamentoEvent += () =>
            {
                door.GetComponent<PolygonCollider2D>().enabled = true;
            };
        }
	}
}
