using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planejamento : MonoBehaviour {

    public bool Disponivel { get; set; }

    public bool Momento1Confirmado { get; set; }
    public bool Momento2Confirmado { get; set; }
    // Momento 3 é confirmado quando o jogador confirma o planejamento

    [SerializeField] [TextArea]
    public string descricaoMomento1;
    [SerializeField] [TextArea]
    public string descricaoMomento2;
    [SerializeField] [TextArea]
    public string descricaoMomento3;

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

        inventario.PermitirDragAndDrop();

        coroutineExecutarPlanejamento = StartCoroutine(ExecutarPlanejamento());
    }

    private IEnumerator ExecutarPlanejamento()
    {
        Momento1Confirmado = false;
        Momento2Confirmado = false;

        inventario.UnselectAllItems();
        planejamentoUI.DesbloquearMomento1();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento1);
        planejamentoUI.DefinirCallbackConfirmacaoMomento1(() => Momento1Confirmado = true);

        // Esperar o jogador confirmar o momento 1
        yield return new WaitUntil(() => Momento1Confirmado);

        inventario.UnselectAllItems();
        planejamentoUI.DesbloquearMomento2();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento2);
        planejamentoUI.DefinirCallbackConfirmacaoMomento2(() => Momento2Confirmado = true);

        // Esperar o jogador confirmar o momento 2
        yield return new WaitUntil(() => Momento2Confirmado);

        inventario.UnselectAllItems();
        planejamentoUI.DesbloquearMomento3();
        planejamentoUI.AlterarDescricaoMomento(descricaoMomento3);
        // A confirmação do momento 3 do planejamento acontecerá quando o
        // método ConfirmarPlanejamento for chamado, ele só faz alguma coisa
        // quando Momento2Confirmado == true
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
        if (!Momento2Confirmado) return;
        // Só continua se o jogador confirmou o momento 2
        GetComponentInChildren<PlanManager>().ConfirmPlan();

        var questGuest = GetComponent<QuestGuest>();
        if (questGuest) ManagerQuest.QuestTakeStep(questGuest.index);

        CancelarPlanejamento();
    }
}
