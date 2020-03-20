using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarrosselNivelDeEnsino : MonoBehaviour {

    [HideInInspector]
    public NivelDeEnsino NivelDeEnsinoSelecionado
    {
        get { return nivelDeEnsinoSelecionado.Value; }
    }

    [SerializeField] private TextMeshProUGUI caixaDeTexto;
    [SerializeField] private Button botaoAnterior;
    [SerializeField] private Button botaoProximo;

    private LinkedList<NivelDeEnsino> niveisDeEnsinoDisponiveis;
    private LinkedListNode<NivelDeEnsino> nivelDeEnsinoSelecionado;

    // As areas de conhecimento dependem do nível de ensino selecionado
    // Toda vez que o nível de ensino mudar, ele deve alterar as áreas
    private CarrosselAreaDeConhecimento carrosselAreaDeConhecimento;

    // Use this for initialization
    void Start () {
        niveisDeEnsinoDisponiveis = new LinkedList<NivelDeEnsino>();
        foreach (var nivelDeEnsino in NivelDeEnsino.TodosOsNiveisDeEnsino())
            niveisDeEnsinoDisponiveis.AddLast(nivelDeEnsino);

        carrosselAreaDeConhecimento = FindObjectOfType<CarrosselAreaDeConhecimento>();
        if (!carrosselAreaDeConhecimento)
            Debug.LogError("Um objeto do tipo " + typeof(CarrosselAreaDeConhecimento) + " é necessário nesta cena!");

        Selecionar(niveisDeEnsinoDisponiveis.First);
	}

    public void Selecionar(LinkedListNode<NivelDeEnsino> nivelDeEnsinoAlvo)
    {
        nivelDeEnsinoSelecionado = nivelDeEnsinoAlvo;
        caixaDeTexto.text = nivelDeEnsinoAlvo.Value.nome;

        if (carrosselAreaDeConhecimento)
            carrosselAreaDeConhecimento.DefinirAreasDeConhecimento(nivelDeEnsinoAlvo.Value);
    }

    public void SelecionarSeguinte()
    {
        var proximo = nivelDeEnsinoSelecionado.Next ?? niveisDeEnsinoDisponiveis.First;
        Selecionar(proximo);
    }

    public void SelecionarAnterior()
    {
        var anterior = nivelDeEnsinoSelecionado.Previous ?? niveisDeEnsinoDisponiveis.Last;
        Selecionar(anterior);
    }
}