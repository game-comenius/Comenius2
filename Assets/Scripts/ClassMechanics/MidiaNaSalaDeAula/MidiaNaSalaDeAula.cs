using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiaNaSalaDeAula : MonoBehaviour {

    private LocalParaColocarItem quadroNegro;
    private LocalParaColocarItem mesaDoProfessor;
    private LocalParaColocarItem[] mesasDosAlunos;

	// Use this for initialization
	void Start () {
        quadroNegro = FindObjectOfType<QuadroNegro>();
        mesaDoProfessor = FindObjectOfType<MesaDoProfessor>();
        mesasDosAlunos = FindObjectsOfType<MesaDoAluno>();
	}

    public void ApresentarMidia(ItemName midia)
    {
        switch (midia)
        {
            case ItemName.QuadroNegro:
                break;
            case ItemName.Cartazes:
            case ItemName.CartazComColecaoDePenas:
                if (quadroNegro) quadroNegro.ColocarItem(midia);
                break;
            case ItemName.TVComVHS:
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
        if (quadroNegro) quadroNegro.RemoverItem();
        if (mesaDoProfessor) mesaDoProfessor.RemoverItem();
        foreach (var mesaDoAluno in mesasDosAlunos)
            mesaDoAluno.RemoverItem();
    }
}
