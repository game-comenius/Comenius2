using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaMissoes : MonoBehaviour
{
    public bool Ativa { get; set; }
    public bool Aberta { get; set; }

    private CorpoJanelaMissoes corpoJanelaMissoes;

    private List<GameObject> listOfQuests = new List<GameObject>();

    // Esse método deve ser chamado quando você quiser cadastrar uma missão
    // nesta janela de missões, basta passar o título da missão e as
    // ordens/passos/parágrafos da missão
    public void AdicionarMissao(QuestGroup quest)
    {
        corpoJanelaMissoes.AdicionarMissao(quest);
    }

    public void Clear()
    {
        foreach(GameObject go in listOfQuests)
        {
            Destroy(go);
        }

        listOfQuests.Clear();
    }

    // Use this for initialization
    void Start()
    {
        corpoJanelaMissoes = GetComponentInChildren<CorpoJanelaMissoes>();
    }

    public bool CompletamenteExplorada()
    {
        return corpoJanelaMissoes.TodosOsBotoesForamAbertos();
    }
}
