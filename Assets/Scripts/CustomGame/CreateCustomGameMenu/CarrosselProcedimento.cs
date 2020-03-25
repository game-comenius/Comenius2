using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CarrosselProcedimento : MonoBehaviour {

    [HideInInspector]
    public Procedimento Selecionado
    {
        get { return nodoSelecionado.Value; }
    }

    [SerializeField] private Image imageProcedimento;
    [SerializeField] private TextMeshProUGUI caixaDeTexto;
    [SerializeField] private Button botaoAnterior;
    [SerializeField] private Button botaoProximo;

    private LinkedList<Procedimento> disponiveis;
    private LinkedListNode<Procedimento> nodoSelecionado;

    // Funções que serão chamadas quando o valor selecionado mudar
    public event Action QuandoValorMudar;

    // Use this for initialization
    void Start()
    {
        disponiveis = new LinkedList<Procedimento>();
        foreach (var procedimento in Enum.GetValues(typeof(Procedimento)))
            disponiveis.AddLast((Procedimento)procedimento);

        // Definir procedimento inicial
        Selecionar(Procedimento.AulaExpositiva);

        botaoAnterior.onClick.AddListener(SelecionarAnterior);
        botaoProximo.onClick.AddListener(SelecionarSeguinte);
    }

    public void Selecionar(Procedimento procedimento)
    {
        var nodo = disponiveis.Find(procedimento);
        if (nodo != null) Selecionar(nodo);
    }

    public void Selecionar(LinkedListNode<Procedimento> nodoAlvo)
    {
        nodoSelecionado = nodoAlvo;
        imageProcedimento.sprite = ProcedimentoSpriteDatabase.SpriteOf(nodoAlvo.Value);
        caixaDeTexto.text = nodoAlvo.Value.Nome();

        if (QuandoValorMudar != null) QuandoValorMudar();
    }

    public void SelecionarSeguinte()
    {
        var proximo = nodoSelecionado.Next ?? disponiveis.First;
        Selecionar(proximo);
    }

    public void SelecionarAnterior()
    {
        var anterior = nodoSelecionado.Previous ?? disponiveis.Last;
        Selecionar(anterior);
    }
}
