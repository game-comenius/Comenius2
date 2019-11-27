using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NivelDeEnsinoDropdown : MonoBehaviour {

    private TMP_Dropdown myDropdown;

    private AreaDeConhecimentoDropdown dropdownAreaDeConhecimento;

    // Use this for initialization
    private void Awake()
    {
        myDropdown = GetComponent<TMP_Dropdown>();
        myDropdown.ClearOptions();
    }

    void Start () {
        dropdownAreaDeConhecimento = transform.parent.GetComponentInChildren<AreaDeConhecimentoDropdown>();

        var niveisDeEnsino = NivelDeEnsino.TodosOsNiveisDeEnsino();
        List<string> nomesNiveisDeEnsino = new List<string>();
        foreach (var nivelDeEnsino in niveisDeEnsino)
            nomesNiveisDeEnsino.Add(nivelDeEnsino.nome);
        
        myDropdown.AddOptions(nomesNiveisDeEnsino);

        myDropdown.onValueChanged.AddListener(DefinirAreasDeConhecimento);
        DefinirAreasDeConhecimento(0);
	}

    // Esta função precisa de um parâmetro porque o AddListener exige uma
    // função do tipo: void (int)
    private void DefinirAreasDeConhecimento(int arg0)
    {
        var nivelDeEnsinoSelecionado = NivelDeEnsinoSelecionado();
        var areas = nivelDeEnsinoSelecionado.areasDeConhecimento;
        dropdownAreaDeConhecimento.DefinirItens(areas);
    }

    public NivelDeEnsino NivelDeEnsinoSelecionado()
    {
        var nomeSelecionado = myDropdown.options[myDropdown.value].text;
        var nivelDeEnsinoSelecionado = NivelDeEnsino.Get(nomeSelecionado);
        return nivelDeEnsinoSelecionado;
    }
}
