using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarrosselAgrupamento : MonoBehaviour {

	[HideInInspector]
	public Agrupamento Selecionado
	{
		get { return nodoSelecionado.Value; }
	}

	[SerializeField] private Image imageAgrupamento;
	[SerializeField] private TextMeshProUGUI caixaDeTexto;
	[SerializeField] private Button botaoAnterior;
	[SerializeField] private Button botaoProximo;

	private LinkedList<Agrupamento> disponiveis;
	private LinkedListNode<Agrupamento> nodoSelecionado;

	// Funções que serão chamadas quando o valor selecionado mudar
	public event Action QuandoValorMudar;

	// Use this for initialization
	void Start()
	{
		disponiveis = new LinkedList<Agrupamento>();
		foreach (var procedimento in Enum.GetValues(typeof(Agrupamento)))
			disponiveis.AddLast((Agrupamento)procedimento);

		// Definir procedimento inicial
		Selecionar(Agrupamento.Individual);

		botaoAnterior.onClick.AddListener(SelecionarAnterior);
		botaoProximo.onClick.AddListener(SelecionarSeguinte);
	}

	public void Selecionar(Agrupamento agrupamento)
	{
		var nodo = disponiveis.Find(agrupamento);
		if (nodo != null) Selecionar(nodo);
	}

	public void Selecionar(LinkedListNode<Agrupamento> nodoAlvo)
	{
		nodoSelecionado = nodoAlvo;
		imageAgrupamento.sprite = AgrupamentoSpriteDatabase.SpriteOf(nodoAlvo.Value);
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
