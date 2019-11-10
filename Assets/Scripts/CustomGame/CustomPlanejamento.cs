using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanejamento : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AnularQuestsParaIniciarPlanejamento();

        ZerarMidiasPadraoDoProfessor();
	}

    private void AnularQuestsParaIniciarPlanejamento()
    {
        var objetoQueIniciaPlanejamento = FindObjectOfType<Plan>();
        var quest = objetoQueIniciaPlanejamento.GetComponent<QuestScript>();
        quest.enabled = false;
        var c = objetoQueIniciaPlanejamento.GetComponent<PolygonCollider2D>();
        c.enabled = true;
    }

    private void ZerarMidiasPadraoDoProfessor()
    {
        var planejamento = GameObject.Find("PlanejamentoGameObject");

        var momentos = planejamento.GetComponentsInChildren<MidiaMomento>(true);
        foreach (var momento in momentos)
        {
            momento.initialItem = ItemName.SemNome;
            momento.AddItem(ItemName.SemNome);
        }
    }
}
