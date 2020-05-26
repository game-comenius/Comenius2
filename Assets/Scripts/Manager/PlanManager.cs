using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : MonoBehaviour {

    private AgrupamentosEmSala[] agrupamentos = new AgrupamentosEmSala[3];
    private ItemName[] chosenMedia = new ItemName[3];
    private int[] points = new int[3];
    private int totalMissionPoints = 0;

    public void ConfirmPlan()
    {
        chosenMedia[0] = GameObject.Find("Midia1").GetComponent<MidiaMomento>().getItem();
        chosenMedia[1] = GameObject.Find("Midia2").GetComponent<MidiaMomento>().getItem();
        chosenMedia[2] = GameObject.Find("Midia3").GetComponent<MidiaMomento>().getItem();

        points[0] = GameObject.Find("Midia1").GetComponent<MidiaMomento>().Points();
        points[1] = GameObject.Find("Midia2").GetComponent<MidiaMomento>().Points();
        points[2] = GameObject.Find("Midia3").GetComponent<MidiaMomento>().Points();

        agrupamentos[0] = GameObject.Find("Midia1").GetComponent<MidiaMomento>().agrupamento;
        agrupamentos[1] = GameObject.Find("Midia2").GetComponent<MidiaMomento>().agrupamento;
        agrupamentos[2] = GameObject.Find("Midia3").GetComponent<MidiaMomento>().agrupamento;

        totalMissionPoints = points[0] + points[1] + points[2];

        Debug.Log("pontuação da missão: " + totalMissionPoints);

        //Player.Instance.chosenMedia = chosenMedia;
        //Player.Instance.points = points;
        //Player.Instance.totalMissionPoints = totalMissionPoints;

        Player.Instance.MissionHistory[Player.Instance.missionID] = new ChosenMediaPoints(agrupamentos, chosenMedia, points, totalMissionPoints);

        //GameManager.UINaoSendoUsada();
    }

    public int getTotalMissionPoints() {
        return totalMissionPoints;
    }

    //então, esses aqui estão hardcoded pra pegar específicamente a melhor/pior mídia da missão 1 apenas, poisé.
    //eu sei, não é bonito, mas é tudo que a gente precisa agora.
    //para deixar mais genérico pra depois, que tal multiplicar todos os índices por uma variável igual ao número da missão?
    //só uma ideia, só dizendo, pode funcionar.
    public ItemName getBestMedia() {
        if (points[1] > points[2] && points[1] > points[3])
            return chosenMedia[1];
        else if (points[2] > points[1] && points[2] > points[3])
            return chosenMedia[2];
        else if (points[3] > points[1] && points[3] > points[2])
            return chosenMedia[3];
        else
            return chosenMedia[1];
    }

    public ItemName getWorstMedia() {
        if (points[1] < points[2] && points[1] < points[3])
            return chosenMedia[1];
        else if (points[2] < points[1] && points[2] < points[3])
            return chosenMedia[2];
        else if (points[3] < points[1] && points[3] < points[2])
            return chosenMedia[3];
        else
            return chosenMedia[1];
    }
}
