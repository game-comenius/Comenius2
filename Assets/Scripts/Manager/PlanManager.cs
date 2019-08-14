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

    public void setSelectedMoment(GameObject newMoment) {
        selectedMoment = newMoment;
    }

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
