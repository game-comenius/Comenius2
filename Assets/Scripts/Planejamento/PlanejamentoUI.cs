using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class PlanejamentoUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI descricaoMomento;
    [SerializeField]
    private Image bloqueioMomento1;
    [SerializeField]
    private Image bloqueioMomento2;
    [SerializeField]
    private Image bloqueioMomento3;
    [SerializeField]
    private Button botaoConfirmarMomento;
    [SerializeField]
    private Button botaoConfirmarPlanejamento;

    private MidiaMomento midiaMomento1;
    private MidiaMomento midiaMomento2;
    private MidiaMomento midiaMomento3;

    private Coroutine coroutineLiberarConfirmarPlanejamento;

    private void Start()
    {
        botaoConfirmarPlanejamento.image.enabled = false;

        var mms = GetComponentsInChildren<MidiaMomento>();
        midiaMomento1 = mms[0];
        midiaMomento2 = mms[1];
        midiaMomento3 = mms[2];
    }

    public void BloquearTodosOsMomentos()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento2.enabled = true;
        bloqueioMomento3.enabled = true;
    }

    private bool MidiaDefinidaParaMomento1
    {
        get { return midiaMomento1.getItem() != ItemName.SemNome; }
    }

    public void DesbloquearMomento1()
    {
        bloqueioMomento2.enabled = true;
        bloqueioMomento3.enabled = true;

        bloqueioMomento1.enabled = false;

        // Fazer o botão confirmar planejamento sumir
        if (coroutineLiberarConfirmarPlanejamento != null)
            StopCoroutine(coroutineLiberarConfirmarPlanejamento);
        botaoConfirmarPlanejamento.image.enabled = false;

        botaoConfirmarMomento.image.enabled = true;
        botaoConfirmarMomento.animator.SetBool("IrParaMomento2", false);
    }

    public void DefinirCallbackConfirmacaoMomento1(UnityAction action)
    {
        UnityAction actionApenasAposDefinicaoDaMidia = () => { if (MidiaDefinidaParaMomento1) action(); };
        botaoConfirmarMomento.onClick.RemoveAllListeners();
        botaoConfirmarMomento.onClick.AddListener(actionApenasAposDefinicaoDaMidia);
    }

    private bool MidiaDefinidaParaMomento2
    {
        get { return midiaMomento2.getItem() != ItemName.SemNome; }
    }

    public void DesbloquearMomento2()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento3.enabled = true;

        bloqueioMomento2.enabled = false;

        // Fazer o botão confirmar planejamento sumir
        if (coroutineLiberarConfirmarPlanejamento != null)
            StopCoroutine(coroutineLiberarConfirmarPlanejamento);
        botaoConfirmarPlanejamento.image.enabled = false;

        botaoConfirmarMomento.image.enabled = true;
        botaoConfirmarMomento.animator.SetBool("IrParaMomento2", true);
    }

    public void DefinirCallbackConfirmacaoMomento2(UnityAction action)
    {
        UnityAction actionApenasAposDefinicaoDaMidia = () => { if (MidiaDefinidaParaMomento2) action(); };
        botaoConfirmarMomento.onClick.RemoveAllListeners();
        botaoConfirmarMomento.onClick.AddListener(actionApenasAposDefinicaoDaMidia);
    }

    private bool MidiaDefinidaParaMomento3
    {
        get { return midiaMomento3.getItem() != ItemName.SemNome; }
    }

    public void DesbloquearMomento3()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento2.enabled = true;

        bloqueioMomento3.enabled = false;

        botaoConfirmarMomento.image.enabled = false;
        coroutineLiberarConfirmarPlanejamento = StartCoroutine(LiberarConfirmarPlanejamento());
    }

    private IEnumerator LiberarConfirmarPlanejamento()
    {
        yield return new WaitUntil(() => MidiaDefinidaParaMomento3);
        yield return new WaitForSeconds(0.4f);
        botaoConfirmarPlanejamento.image.enabled = true;
    }

    public void AlterarDescricaoMomento(string novaDescricao)
    {
        descricaoMomento.text = novaDescricao;
    }

    public void ColocarItensIniciais()
    {
        midiaMomento1.ResetItem();
        midiaMomento2.ResetItem();
        midiaMomento3.ResetItem();
    }
}
