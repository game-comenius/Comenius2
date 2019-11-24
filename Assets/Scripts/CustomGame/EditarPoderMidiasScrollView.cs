using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditarPoderMidiasScrollView : MonoBehaviour {

    [SerializeField]
    private AvailableItemsPanel availableItemsPanel;

    [SerializeField]
    private GameObject contentContainer;
    [SerializeField]
    private FaixaEditarPoderMidia prefabFaixaEditarPoderMidia;

    public List<FaixaEditarPoderMidia> FaixasEditarPoderMidia { get; private set; }

	// Use this for initialization
	void Start () {
        FaixasEditarPoderMidia = new List<FaixaEditarPoderMidia>();

        var midiasSelecionadas = availableItemsPanel.MidiasSelecionadas();
        foreach (var midia in midiasSelecionadas)
        {
            var faixa = Instantiate(prefabFaixaEditarPoderMidia);
            faixa.SetMidia(midia);
            FaixasEditarPoderMidia.Add(faixa);

            faixa.transform.SetParent(contentContainer.transform);
            faixa.transform.localScale = Vector3.one;
        }
	}
}
