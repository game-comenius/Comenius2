using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparadorDaProximaMissao : MonoBehaviour {

	public void LimparMissaoAtual()
    {
        var indiceMissaoAtual = Player.Instance.missionID;

        RemoverMidiasExclusivasDaMissao(indiceMissaoAtual);
    }

    private void RemoverMidiasExclusivasDaMissao(int indiceMissao)
    {
        var inventario = Player.Instance.Inventory;

        ItemName[] midiasExclusivas = { };
        // Missão 1 = 0; Missão 2 = 1; ...
        switch (indiceMissao)
        {
            case 0:
                midiasExclusivas = GameManager.MidiasExclusivasDaMissao1; break;
            case 1:
                midiasExclusivas = GameManager.MidiasExclusivasDaMissao2; break;
            case 2:
                midiasExclusivas = GameManager.MidiasExclusivasDaMissao3; break;
        }

        foreach (var midia in midiasExclusivas) inventario.Remove(midia);
    }
}
