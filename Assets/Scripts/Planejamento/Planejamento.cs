using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuestGuest))]
public class Planejamento : MonoBehaviour {

    private Canvas canvas;
    private FadeEffect backgroundPreto;

    public bool Disponivel { get; set; }

	//Use this for initialization
	void Start () {
        canvas = GetComponentInChildren<Canvas>();
        backgroundPreto = GetComponentInChildren<FadeEffect>();
        canvas.enabled = false;

        // Para ajudar no desenvolvimento, o planejamento sempre ta disponível
        #if UNITY_EDITOR
        this.Disponivel = true;
        #endif
    }

    public void AbrirPlanejamento()
    {
        if (!Disponivel) return;

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
