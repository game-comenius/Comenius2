using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaDeConhecimentoDropdown : MonoBehaviour {

    TMP_Dropdown myDropdown;

    // Use this for initialization
    private void Awake()
    {
        myDropdown = GetComponent<TMP_Dropdown>();
        myDropdown.ClearOptions();
    }

    public void DefinirItens(AreaDeConhecimento[] areasDeConhecimento)
    {
        myDropdown.value = 0;
        myDropdown.ClearOptions();
        var nomes = new List<string>();
        foreach (var areaDeConhecimento in areasDeConhecimento)
            nomes.Add(areaDeConhecimento.nome);

        myDropdown.AddOptions(nomes);
    }

    public AreaDeConhecimento AreaDeConhecimentoSelecionada()
    {
        var nomeSelecionado = myDropdown.options[myDropdown.value].text;
        var selecionada = AreaDeConhecimento.Get(nomeSelecionado);
        return selecionada;
    }
}
