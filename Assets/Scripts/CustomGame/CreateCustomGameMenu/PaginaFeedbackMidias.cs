using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaginaFeedbackMidias : MonoBehaviour {

    [SerializeField] [Tooltip("Espaço para linha de feedback de uma mídia")]
    private GameObject espacoParaLinhaDeFeedbackDaMidia;
    [SerializeField]
    private LinhaEscolherFeedback prefabLinhaFeedback;
    [SerializeField]
    private PaginaEscolherMidias paginaEscolherMidias;

    private List<LinhaEscolherFeedback> linhasEscolherFeedback;
    public ItemName[] MidiasNestaPagina
    {
        get { return linhasEscolherFeedback.Select((linha) => linha.Midia).ToArray(); }
    }

    private void Awake()
    {
        linhasEscolherFeedback = new List<LinhaEscolherFeedback>();
    }

    private void OnEnable()
    {
        // Se o criador do jogo voltou e selecionou outras mídias, atualizar
        // as mídias deste painel para serem configuradas
        var midiasSelecionadas = paginaEscolherMidias.MidiasSelecionadas.ToArray();
        if (!ArraysSaoIguais(MidiasNestaPagina, midiasSelecionadas))
        {
            // Apagar todas as mídias
            for (int i = 0; i < espacoParaLinhaDeFeedbackDaMidia.transform.childCount; i++)
                Destroy(espacoParaLinhaDeFeedbackDaMidia.transform.GetChild(i).gameObject);
            linhasEscolherFeedback.Clear();

            // Popular com uma faixa para cada mídia selecionada
            foreach (var midia in midiasSelecionadas)
            {
                var linha = Instantiate(prefabLinhaFeedback);
                linha.Midia = midia;
                linhasEscolherFeedback.Add(linha);

                linha.transform.SetParent(espacoParaLinhaDeFeedbackDaMidia.transform);
                linha.transform.localScale = Vector3.one;
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
