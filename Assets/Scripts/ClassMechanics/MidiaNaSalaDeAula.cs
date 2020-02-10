using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiaNaSalaDeAula : MonoBehaviour {

    private LocalParaColocarItem mesaDoProfessor;
    private LocalParaColocarItem[] mesasDosAlunos;

	// Use this for initialization
	void Start () {
        mesaDoProfessor = FindObjectOfType<MesaDoProfessor>();
        mesasDosAlunos = FindObjectsOfType<MesaDoAluno>();
	}

    public void ApresentarMidia(ItemName midia)
    {
        switch (midia)
        {
            case ItemName.QuadroNegro:
                break;
            case ItemName.TV:
            case ItemName.ReprodutorAudio:
                if (mesaDoProfessor) mesaDoProfessor.ColocarItem(midia);
                break;
            default:
                foreach (var mesaDoAluno in mesasDosAlunos)
                    mesaDoAluno.ColocarItem(midia);
                break;
        }
    }

    public void EsconderMidiaAtual()
    {
        if (mesaDoProfessor) mesaDoProfessor.RemoverItem();
    }
}
