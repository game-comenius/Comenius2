using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiaNaSalaDeAula : MonoBehaviour {

    private LocalParaColocarItem mesaDoProfessor;
    private QuadroNegro quadroNegro;
    private LocalParaColocarItem[] mesasDosAlunos;
    

	// Use this for initialization
	void Start () {
        mesaDoProfessor = FindObjectOfType<MesaDoProfessor>();
        quadroNegro = FindObjectOfType<QuadroNegro>();
        mesasDosAlunos = FindObjectsOfType<MesaDoAluno>();
	}

    public void ApresentarMidia(ItemName midia)
    {
        switch (midia)
        {
            case ItemName.QuadroNegro:
                break;
            case ItemName.Cartazes:
                if (quadroNegro) quadroNegro.ColocarItem(midia);
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
        foreach (var mesaDoAluno in mesasDosAlunos)
            mesaDoAluno.RemoverItem();
    }
}
