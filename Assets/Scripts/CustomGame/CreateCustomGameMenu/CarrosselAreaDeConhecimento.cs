using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarrosselAreaDeConhecimento : MonoBehaviour {

    [HideInInspector]
    public AreaDeConhecimento AreaDeConhecimentoSelecionada
    {
        get { return areaDeConhecimentoSelecionada.Value; }
    }

    [SerializeField] private TextMeshProUGUI caixaDeTexto;
    [SerializeField] private Button botaoAnterior;
    [SerializeField] private Button botaoProximo;

    private LinkedList<AreaDeConhecimento> areasDeConhecimentoDisponiveis;
    private LinkedListNode<AreaDeConhecimento> areaDeConhecimentoSelecionada;

    // Use this for initialization
    private void Awake()
    {
        areasDeConhecimentoDisponiveis = new LinkedList<AreaDeConhecimento>();
    }

    public void Selecionar(LinkedListNode<AreaDeConhecimento> areaDeConhecimentoAlvo)
    {
        areaDeConhecimentoSelecionada = areaDeConhecimentoAlvo;
        caixaDeTexto.text = areaDeConhecimentoAlvo.Value.nome;
    }

    public void SelecionarSeguinte()
    {
        if (areaDeConhecimentoSelecionada == null) return;
        var proximo = areaDeConhecimentoSelecionada.Next ?? areasDeConhecimentoDisponiveis.First;
        Selecionar(proximo);
    }

    public void SelecionarAnterior()
    {
        if (areaDeConhecimentoSelecionada == null) return;
        var anterior = areaDeConhecimentoSelecionada.Previous ?? areasDeConhecimentoDisponiveis.Last;
        Selecionar(anterior);
    }

    public void DefinirAreasDeConhecimento(NivelDeEnsino nivelDeEnsino)
    {
        // Pegar áreas de conhecimento deste nível de ensino
        var areasDeConhecimento = nivelDeEnsino.AreasDeConhecimento;

        // Preencher o carrossel com as novas áreas de conhecimento
        areasDeConhecimentoDisponiveis.Clear();
        foreach (var area in areasDeConhecimento)
            areasDeConhecimentoDisponiveis.AddLast(area);

        // A primeira delas é a que vai aparecer no carrosel primeiro
        Selecionar(areasDeConhecimentoDisponiveis.First);
    }
}