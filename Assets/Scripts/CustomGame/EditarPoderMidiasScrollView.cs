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

    private ItemName[] midias;

    public List<FaixaEditarPoderMidia> FaixasEditarPoderMidia { get; private set; }

	// Use this for initialization
	void Awake () {
        FaixasEditarPoderMidia = new List<FaixaEditarPoderMidia>();
    }

    private void OnEnable()
    {
        // Se o criador do jogo voltou e selecionou outras mídias, atualizar
        // as mídias deste painel para serem configuradas
        var midiasSelecionadas = availableItemsPanel.MidiasSelecionadas();
        if (!ArraysSaoIguais(midias, midiasSelecionadas))
        {
            // Apagar todas as mídias
            for (int i = 0; i < contentContainer.transform.childCount; i++)
                Destroy(contentContainer.transform.GetChild(i).gameObject);
            FaixasEditarPoderMidia.Clear();

            midias = midiasSelecionadas;

            // Popular com uma faixa para cada mídia selecionada
            foreach (var midia in midias)
            {
                var faixa = Instantiate(prefabFaixaEditarPoderMidia);
                faixa.SetMidia(midia);
                FaixasEditarPoderMidia.Add(faixa);

                faixa.transform.SetParent(contentContainer.transform);
                faixa.transform.localScale = Vector3.one;
            }
        }
    }

    // Função está aqui apenas como uma utilidade
    private bool ArraysSaoIguais(ItemName[] arr1, ItemName[] arr2)
    {
        if (arr1 == null || arr2 == null)
            return false;

        int n = arr1.Length;
        int m = arr2.Length;

        // If lengths of array are not 
        // equal means array are not equal 
        if (n != m)
            return false;

        // Sort both arrays 
        Array.Sort(arr1);
        Array.Sort(arr2);

        // Linearly compare elements 
        for (int i = 0; i < n; i++)
            if (arr1[i] != arr2[i])
                return false;

        // If all elements were same. 
        return true;
    }
}
