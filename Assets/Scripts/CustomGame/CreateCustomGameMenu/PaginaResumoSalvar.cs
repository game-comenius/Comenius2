﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginaResumoSalvar : MonoBehaviour {

    [Header("Símbolos das Moedas")]
    [SerializeField] private GameObject MoedasDoJean;
    [SerializeField] private GameObject MoedasDoVladmir;
    [SerializeField] private GameObject MoedasDoPaulino;
    [SerializeField] private GameObject MoedasDoCelestino;
    [SerializeField] private GameObject MoedasDaAlice;
    [SerializeField] private GameObject MoedasDaAntonia;
    [SerializeField] private GameObject MoedasDaMontanari;
    [SerializeField] private GameObject MoedasDoDiretor;
    private List<GameObject> setsDeMoedasDosProfessores;

    [Header("Input Fields")]
    [SerializeField] TMP_InputField tituloDaAulaInputField;
    [SerializeField] TMP_InputField AutorInputField;

    [Header("Botões")]
    [SerializeField] Button botaoImprimir;
    [SerializeField] Button botaoSalvar;

    private void Awake()
    {
        setsDeMoedasDosProfessores = new List<GameObject>
        {
            MoedasDoJean,
            MoedasDoVladmir,
            MoedasDoPaulino,
            MoedasDoCelestino,
            MoedasDaAlice,
            MoedasDaAntonia,
            MoedasDaMontanari,
            MoedasDoDiretor
        };

        // Botão Imprimir e Salvar começam desabilitados
        DefinirInteratividadeDosBotoes(false);
    }

    private void Start()
    {
        // Quando título da aula for alterado, liberar botão de salvar se tanto
        // o título da aula quanto o nome do autor estiverem ok
        tituloDaAulaInputField.onValueChanged.AddListener((s) =>
        {
            if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(AutorInputField.text))
                DefinirInteratividadeDosBotoes(true);
            else
                DefinirInteratividadeDosBotoes(false);
        });

        // Quando nome do autor for alterado, liberar botão de salvar se tanto
        // o título da aula quanto o nome do autor estiverem ok
        AutorInputField.onValueChanged.AddListener((s) =>
        {
            if (!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(tituloDaAulaInputField.text))
                DefinirInteratividadeDosBotoes(true);
            else
                DefinirInteratividadeDosBotoes(false);
        });
    }

    private void OnEnable()
    {
        var paginaEscolherProfessor = transform.parent.GetComponentInChildren<PaginaEscolherProfessor>(true);
        var professorSelecionado = paginaEscolherProfessor.ProfessorSelecionado;

        // Desabilitar todas as moedas
        foreach (var set in setsDeMoedasDosProfessores)
            set.SetActive(false);

        // Habilitar apenas o set correto de acordo com o professor escolhido
        switch (professorSelecionado)
        {
            case CharacterName.Jean: MoedasDoJean.SetActive(true); break;
            case CharacterName.Vladmir: MoedasDoVladmir.SetActive(true); break;
            case CharacterName.Paulino: MoedasDoPaulino.SetActive(true); break;
            case CharacterName.Celestino: MoedasDoCelestino.SetActive(true); break;
            case CharacterName.Alice: MoedasDaAlice.SetActive(true); break;
            case CharacterName.Antonia: MoedasDaAntonia.SetActive(true); break;
            case CharacterName.Montanari: MoedasDaMontanari.SetActive(true); break;
            case CharacterName.Diretor: MoedasDoDiretor.SetActive(true); break;
        }
    }

    private void DefinirInteratividadeDosBotoes(bool interativo)
    {
        botaoImprimir.interactable = interativo;
        botaoSalvar.interactable = interativo;
    }
}
