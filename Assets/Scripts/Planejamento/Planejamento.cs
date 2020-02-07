using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planejamento : MonoBehaviour {

    private Canvas canvas;
    private FadeEffect backgroundPreto;

	// Use this for initialization
	void Start () {
        canvas = GetComponentInChildren<Canvas>();
        backgroundPreto = GetComponentInChildren<FadeEffect>();
        canvas.enabled = false;
	}

    public void AbrirPlanejamento()
    {
        canvas.enabled = true;
        backgroundPreto.Fadein();
        GameManager.UISendoUsada();
    }

    public void CancelarPlanejamento()
    {
        canvas.enabled = false;
        backgroundPreto.Fadeout();
        GameManager.UINaoSendoUsada();
    }

    public void ConfirmarPlanejamento()
    {
        GetComponentInChildren<PlanManager>().ConfirmPlan();
        GetComponent<QuestScript>().CompletarQuest();
        CancelarPlanejamento();
    }
}
