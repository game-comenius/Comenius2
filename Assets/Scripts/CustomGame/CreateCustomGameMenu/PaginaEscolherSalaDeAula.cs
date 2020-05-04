using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class PaginaEscolherSalaDeAula : MonoBehaviour {

    [HideInInspector]
    public SalaDeAula SalaSelecionada
    {
        get { return salaSelecionada.Value; }
    }

    [SerializeField] private Image imageSalaCiencias;
    [SerializeField] private Image imageSalaHistoria;
    [SerializeField] private Image imageSalaPortugues;
    private Dictionary<SalaDeAula, Image> imagesDasSalas;

    [SerializeField] private TextMeshProUGUI textoBalao;

    private LinkedList<SalaDeAula> salasDisponiveis;
    private LinkedListNode<SalaDeAula> salaSelecionada;

    // Use this for initialization
    void Start()
    {
        // Definir quais professores estão disponíveis para serem escolhidos
        salasDisponiveis = new LinkedList<SalaDeAula>();
        salasDisponiveis.AddLast(SalaDeAula.SalaDeCiencias);
        salasDisponiveis.AddLast(SalaDeAula.SalaDeHistoria);
        salasDisponiveis.AddLast(SalaDeAula.SalaDePortugues);

        // Fazer o vínculo entre SalaDeAula e Image da cena menu criar custom
        imagesDasSalas = new Dictionary<SalaDeAula, Image>
        {
            { SalaDeAula.SalaDeCiencias, imageSalaCiencias },
            { SalaDeAula.SalaDeHistoria, imageSalaHistoria },
            { SalaDeAula.SalaDePortugues, imageSalaPortugues }
        };
        foreach (var image in imagesDasSalas.Values)
            if (image) image.preserveAspect = true;

        SelecionarSala(salasDisponiveis.First);
    }

    public void SelecionarSala(LinkedListNode<SalaDeAula> salaAlvo)
    {
        salaSelecionada = salaAlvo;

        // Ativar somente a image da sala alvo
        foreach (var imageDaSala in imagesDasSalas)
            imageDaSala.Value.enabled = imageDaSala.Key == salaAlvo.Value;

        textoBalao.text = salaAlvo.Value.NomeCompleto();

        // Salas de português e de história ainda não estão funcionando, por
        // isso, impedir que o jogador prossiga caso uma delas seja selecionada
        // Este código deve ser atualizado no futuro quando eles estiverem ok
        if (salaSelecionada.Value == SalaDeAula.SalaDeHistoria || salaSelecionada.Value == SalaDeAula.SalaDePortugues)
            GetComponentInParent<CreateCustomGamePanel>().botaoAvancarPagina.gameObject.SetActive(false);
        else
            GetComponentInParent<CreateCustomGamePanel>().botaoAvancarPagina.gameObject.SetActive(true);
    }

    public void SelecionarSalaSeguinte()
    {
        var proximaSala = salaSelecionada.Next ?? salasDisponiveis.First;
        SelecionarSala(proximaSala);
    }

    public void SelecionarSalaAnterior()
    {
        var professorAnterior = salaSelecionada.Previous ?? salasDisponiveis.Last;
        SelecionarSala(professorAnterior);
    }
}