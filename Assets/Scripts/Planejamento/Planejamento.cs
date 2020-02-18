using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuestGuest))]
public class Planejamento : MonoBehaviour {

    private Canvas canvas;
    private FadeEffect backgroundPreto;

	//Use this for initialization
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

        // Fazer com que o jogador consiga arrastar os items do inventário
        // durante e para o planejamento
        var items = GetComponentsInChildren<ItemInUserInterface>();
        foreach (var item in items)
        {
            if (!item.GetComponent<DragDrop>())
                item.gameObject.AddComponent<DragDrop>();
        }
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
        ManagerQuest.QuestTakeStep(GetComponent<QuestGuest>().index);
        CancelarPlanejamento();
    }
}
