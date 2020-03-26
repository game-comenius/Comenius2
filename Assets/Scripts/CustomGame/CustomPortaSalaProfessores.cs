using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPortaSalaProfessores : MonoBehaviour {

	private IEnumerator Start () {
        // Truque para que a Lurdinha comece no lugar certo na sala de aula
        // porque este script vai enganar o jogo e a Lurdinha vai ignorar
        // o pátio quando sair da sala dos professores
        yield return new WaitUntil(() => Player.Instance != null);
        Player.Instance.sceneName = "M1_Patio1_Dia";

        // Trocar o destino de todas as portas na sala dos professores para
        // a sala de aula selecionada pelo criador do jogo custom
        var doors = FindObjectsOfType<DoorTransition>();
        foreach (var door in doors)
            door.sceneName = "CustomSalaDeAula";
	}
}
