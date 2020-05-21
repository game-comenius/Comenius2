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
            case ItemName.Gravador:
            case ItemName.ReprodutorAudio:
            case ItemName.Mapa:
            case ItemName.Retroprojetor:
            case ItemName.RetroprojetorSlideCicloTrabalho:
            case ItemName.RetroprojetorSlideLinhaTempo:
            case ItemName.RetroprojetorSlideMapa:
            case ItemName.CameraPolaroid:
                if (mesaDoProfessor) mesaDoProfessor.ColocarItem(midia);
                break;
            case ItemName.GravacaoPassaro:
                if (mesaDoProfessor) mesaDoProfessor.ColocarItem(ItemName.Gravador);
                break;
            case ItemName.TVComVHS:
            case ItemName.VHS:
            case ItemName.TVComVHSPassaros:
            case ItemName.TVComVHSRevolucaoIndustrial:
            case ItemName.TVComVHSRevolucaoIndustrialEditado:
            case ItemName.VhsEditado:
            case ItemName.VHSregionalismo:
            case ItemName.VHSregionalismoEditado:
                if (mesaDoProfessor) mesaDoProfessor.ColocarItem(ItemName.TVComVHS);
                break;
            case ItemName.Cd:
            case ItemName.ReprodutorAudioComCDPassaros:
            case ItemName.ReprodutorAudioComCDRevolucaoIndustrial:
            case ItemName.CDsotaques:
                if (mesaDoProfessor) mesaDoProfessor.ColocarItem(ItemName.ReprodutorAudio);
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
