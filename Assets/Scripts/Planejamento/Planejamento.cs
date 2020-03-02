using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuestGuest))]
public class Planejamento : MonoBehaviour {

    public bool Disponivel { get; set; }

    public bool Momento1Confirmado { get; set; }
    public bool Momento2Confirmado { get; set; }
    // Momento 3 é confirmado quando o jogador confirma o planejamento

    [SerializeField] [TextArea]
    private string descricaoMomento1;
    [SerializeField] [TextArea]
    private string descricaoMomento2;
    [SerializeField] [TextArea]
    private string descricaoMomento3;

    private Canvas canvas;
    private FadeEffect backgroundPreto;

    private Coroutine coroutineExecutarPlanejamento;

    private FolhaInventarioNoPlanejamento inventario;
    private PlanejamentoUI planejamentoUI;

	//Use this for initialization
	void Start () {
        inventario = GetComponentInChildren<FolhaInventarioNoPlanejamento>();
        planejamentoUI = GetComponentInChildren<PlanejamentoUI>();

        canvas = GetComponentInChildren<Canvas>();
        backgroundPreto = GetComponentInChildren<FadeEffect>();
        canvas.enabled = false;
    }

    public void AbrirPlanejamento()
    {
        if (!Disponivel) return;

        canvas.enabled = true;
        backgroundPreto.Fadein();
        GameManager.UISendoUsada();

        coroutineExecutarPlanejamento = StartCoroutine(ExecutarPlanejamento());
    }

    private IEnumerator ExecutarPlanejamento()
    {
        Momento1Confirmado = false;
        Momento2Confirmado = false;

        planejamentoUI.DesbloquearMomento1();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento1);
        planejamentoUI.DefinirCallbackConfirmacaoMomento1(() => Momento1Confirmado = true);

        // Esperar o jogador confirmar o momento 1
        yield return new WaitUntil(() => Momento1Confirmado);

        planejamentoUI.DesbloquearMomento2();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento2);
        planejamentoUI.DefinirCallbackConfirmacaoMomento2(() => Momento2Confirmado = true);

        // Esperar o jogador confirmar o momento 2
        yield return new WaitUntil(() => Momento2Confirmado);

        planejamentoUI.DesbloquearMomento3();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento3);
    }

    public void RecomecarPlanejamento()
    {
        planejamentoUI.ColocarItensIniciais();
        // Parar a coroutine se ela existir
        StopCoroutine(coroutineExecutarPlanejamento);
        // Começar a coroutine novamente
        coroutineExecutarPlanejamento = StartCoroutine(ExecutarPlanejamento());
    }

    public void CancelarPlanejamento()
    {
        // Parar a coroutine se ela existir
        StopCoroutine(coroutineExecutarPlanejamento);

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
