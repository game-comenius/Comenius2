using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlanejamento : MonoBehaviour {

    private Planejamento planejamento;


    void Start()
    {
        planejamento = FindObjectOfType<Planejamento>();
        planejamento.Disponivel = true;

        ZerarMidiasPadraoDoProfessor();

        ConfigurarPlanejamento(CustomGameSettings.CurrentSettings);
    }

    private void ConfigurarPlanejamento(CustomGameSettings s)
    {
        DefinirDescricaoDosMomentos();
        DefinirProcedimentos(s.Procedimento1, s.Procedimento2, s.Procedimento3);
        DefinirAgrupamentos(s.Agrupamento1, s.Agrupamento2, s.Agrupamento3);
    }

    private void DefinirDescricaoDosMomentos()
    {
        planejamento.descricaoMomento1 = "Escolha uma das mídias para o momento 1";
        planejamento.descricaoMomento2 = "Escolha uma das mídias para o momento 2";
        planejamento.descricaoMomento3 = "Escolha uma das mídias para o momento 3";
    }

    private void ZerarMidiasPadraoDoProfessor()
    {
        var momentos = planejamento.GetComponentsInChildren<MidiaMomento>(true);
        foreach (var momento in momentos)
        {
            momento.initialItem = ItemName.SemNome;
            momento.AddItem(ItemName.SemNome);
        }
    }

    void DefinirProcedimentos(Procedimento p1, Procedimento p2, Procedimento p3)
    {
        Debug.Log("Procedimentos: " + p1 + ", " + p2 + " e " + p3);

        var s = planejamento.GetComponentsInChildren<SlotProcedimento>(true);
        s[0].Procedimento = p1;
        s[1].Procedimento = p2;
        s[2].Procedimento = p3;
    }

    void DefinirAgrupamentos(Agrupamento a1, Agrupamento a2, Agrupamento a3)
    {
        Debug.Log("Agrupamentos: " + a1 + ", " + a2 + " e " + a3);

        var s = planejamento.GetComponentsInChildren<SlotAgrupamento>(true);
        s[0].Agrupamento = a1;
        s[1].Agrupamento = a2;
        s[2].Agrupamento = a3;
    }
}
