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

    private void Start()
    {
        botaoConfirmarPlanejamento.image.enabled = false;
    }

    public void BloquearTodosOsMomentos()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento2.enabled = true;
        bloqueioMomento3.enabled = true;
    }

    public void DesbloquearMomento1()
    {
        bloqueioMomento2.enabled = true;
        bloqueioMomento3.enabled = true;

        bloqueioMomento1.enabled = false;

        botaoConfirmarMomento.image.enabled = true;
        botaoConfirmarMomento.animator.SetBool("IrParaMomento2", false);
    }

    public void DefinirCallbackConfirmacaoMomento1(UnityAction action)
    {
        botaoConfirmarMomento.onClick.RemoveAllListeners();
        botaoConfirmarMomento.onClick.AddListener(action);
    }

    public void DesbloquearMomento2()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento3.enabled = true;

        bloqueioMomento2.enabled = false;

        botaoConfirmarMomento.image.enabled = true;
        botaoConfirmarMomento.animator.SetBool("IrParaMomento2", true);
    }

    public void DefinirCallbackConfirmacaoMomento2(UnityAction action)
    {
        botaoConfirmarMomento.onClick.RemoveAllListeners();
        botaoConfirmarMomento.onClick.AddListener(action);
    }

    public void DesbloquearMomento3()
    {
        bloqueioMomento1.enabled = true;
        bloqueioMomento2.enabled = true;

        bloqueioMomento3.enabled = false;

        botaoConfirmarMomento.image.enabled = false;
        botaoConfirmarPlanejamento.image.enabled = true;
    }

    public void AlterarDescricaoMomento(string novaDescricao)
    {
        descricaoMomento.text = novaDescricao;
    }

    public void ColocarItensIniciais()
    {
        var itens = GetComponentsInChildren<MidiaMomento>();
        foreach (var item in itens)
            item.ResetItem();
    }
}
