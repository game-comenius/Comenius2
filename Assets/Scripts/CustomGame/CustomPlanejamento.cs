using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        DefinirFotoDoProfessor(s.Professor);
        DefinirDescricaoDosMomentos();
        DefinirPoderDasMidias(s);
        DefinirProcedimentos(s.Procedimento1, s.Procedimento2, s.Procedimento3);
        DefinirAgrupamentos(s.Agrupamento1, s.Agrupamento2, s.Agrupamento3);
    }

    private void DefinirFotoDoProfessor(CharacterName professor)
    {
        var objRetrato = GameObject.Find("ImageRetratoProfessor");
        if (!objRetrato) return;
        var imageRetrato = objRetrato.GetComponent<Image>();
        if (!imageRetrato) return;
        imageRetrato.sprite = CharacterSpriteDatabase.Foto(professor);
        imageRetrato.preserveAspect = true;
    }

    private void DefinirDescricaoDosMomentos()
    {
        planejamento.descricaoMomento1 = "Escolha uma das mídias para o momento 1";
        planejamento.descricaoMomento2 = "Escolha uma das mídias para o momento 2";
        planejamento.descricaoMomento3 = "Escolha uma das mídias para o momento 3";
    }

    private void DefinirPoderDasMidias(CustomGameSettings s)
    {
        CreateCustomGamePanel.MidiaPoderFeedback[][] arraysMPF =
        {
            s.ArrayMidiaPoderFeedbackMomento1,
            s.ArrayMidiaPoderFeedbackMomento2,
            s.ArrayMidiaPoderFeedbackMomento3,
        };
        foreach (var arrayMPF in arraysMPF) if (arrayMPF == null) return;

        MidiaMomento[] midiasMomento =
        {
            GameObject.Find("Midia1").GetComponent<MidiaMomento>(),
            GameObject.Find("Midia2").GetComponent<MidiaMomento>(),
            GameObject.Find("Midia3").GetComponent<MidiaMomento>(),
        };
        foreach (var mm in midiasMomento) if (mm == null) return;

        for (var i = 0; i < 3; i++)
        {
            var midiaMomento = midiasMomento[i];
            foreach (var mpf in arraysMPF[i])
                midiaMomento.SetPoints(mpf.Midia, (int)mpf.Poder);
        }
    }

    private void ZerarMidiasPadraoDoProfessor()
    {
        var momentos = planejamento.GetComponentsInChildren<MidiaMomento>();
        foreach (var momento in momentos)
        {
            momento.initialItem = ItemName.SemNome;
            momento.AddItem(ItemName.SemNome);
        }
    }

    private void DefinirProcedimentos(Procedimento p1, Procedimento p2, Procedimento p3)
    {
        Debug.Log("Procedimentos: " + p1 + ", " + p2 + " e " + p3);

        planejamento.DefinirProcedimentoMomento1(p1);
        planejamento.DefinirProcedimentoMomento2(p2);
        planejamento.DefinirProcedimentoMomento3(p3);
    }

    void DefinirAgrupamentos(Agrupamento a1, Agrupamento a2, Agrupamento a3)
    {
        Debug.Log("Agrupamentos: " + a1 + ", " + a2 + " e " + a3);

        planejamento.DefinirAgrupamentoMomento1(a1);
        planejamento.DefinirAgrupamentoMomento2(a2);
        planejamento.DefinirAgrupamentoMomento3(a3);
    }
}
