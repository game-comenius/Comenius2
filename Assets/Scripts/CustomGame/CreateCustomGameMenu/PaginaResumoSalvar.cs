using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaginaResumoSalvar : MonoBehaviour {

    [SerializeField] private GameObject MoedasDoJean;
    [SerializeField] private GameObject MoedasDoVladmir;
    [SerializeField] private GameObject MoedasDoPaulino;
    [SerializeField] private GameObject MoedasDoCelestino;
    [SerializeField] private GameObject MoedasDaAlice;
    [SerializeField] private GameObject MoedasDaAntonia;
    [SerializeField] private GameObject MoedasDaMontanari;
    private List<GameObject> setsDeMoedasDosProfessores;

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
            MoedasDaMontanari
        };
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
        }
    }
}
