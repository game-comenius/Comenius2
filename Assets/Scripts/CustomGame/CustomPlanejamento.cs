using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanejamento : MonoBehaviour {

    void Start () {
        AnularQuestsParaIniciarPlanejamento();
        ZerarMidiasPadraoDoProfessor();

        var s = CustomGameSettings.ReadCustomGameSettingsFromDisk();
        DefinirProcedimentos(s.Procedimento1, s.Procedimento2, s.Procedimento3);
	}

    private void AnularQuestsParaIniciarPlanejamento()
    {
        var objetoQueIniciaPlanejamento = GameObject.Find("IniciaPlanejamento");
        var quest = objetoQueIniciaPlanejamento.GetComponent<QuestScript>();
        var c = objetoQueIniciaPlanejamento.GetComponent<PolygonCollider2D>();
        quest.dependenciasNaoFeitas.AddListener(() => c.enabled = true);
        quest.enabled = false;
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

    private void DefinirProcedimentos(Procedimento p1, Procedimento p2, Procedimento p3)
    {
        Debug.Log("Procedimentos escolhidos: " + p1 + ", " + p2 + " e " + p3);
    }
}
