using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanManager : MonoBehaviour {

    private ItemName[] chosenMedia = new ItemName[3];
    private double[] points = new double[3];
    private double totalMissionPoints = 0;

    //contém a referência ao momento que está selecionado para receber uma mídia
    private GameObject selectedMoment = null;

    public GameObject getSelectedMoment()
    {
        if (selectedMoment != null)
            return selectedMoment;
        else {
            Debug.Log("nenhum momento selecionado");
            return null;
        }
    }

    //MidiaMomento usa essa função para dizer que momento vai ser alterado
    public void setSelectedMoment(GameObject newMoment) {
        selectedMoment = newMoment;
    }

    //inventário usa isso para deselecionar o momento alterado depois de fazer as mudanças
    public void unselectMoment() {
        selectedMoment = null;
    }

    public void ConfirmPlan() {
        chosenMedia[0] = GameObject.Find("Midia1").GetComponent<MidiaMomento>().getItem();
        chosenMedia[1] = GameObject.Find("Midia2").GetComponent<MidiaMomento>().getItem();
        chosenMedia[2] = GameObject.Find("Midia3").GetComponent<MidiaMomento>().getItem();

        points[0] = GameObject.Find("Midia1").GetComponent<MidiaMomento>().Points();
        points[1] = GameObject.Find("Midia2").GetComponent<MidiaMomento>().Points();
        points[2] = GameObject.Find("Midia3").GetComponent<MidiaMomento>().Points();
        totalMissionPoints = points[0] + points[2] + points[3];
    }

    public double getTotalMissionPoints() {
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
