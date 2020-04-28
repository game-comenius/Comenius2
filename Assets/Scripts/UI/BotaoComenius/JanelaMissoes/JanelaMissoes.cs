using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JanelaMissoes : MonoBehaviour
{
    public bool Ativa { get; set; }
    public bool Aberta { get; set; }

    // UI da janela de missões
    private CorpoJanelaMissoes corpoJanelaMissoes;

    private List<QuestClass> listaDeQuestsRemovidas;
    private List<QuestClass> listaDeQuestsAtivas;


    void Start()
    {
        corpoJanelaMissoes = GetComponentInChildren<CorpoJanelaMissoes>();

        listaDeQuestsRemovidas = new List<QuestClass>();
        listaDeQuestsAtivas = new List<QuestClass>();

        // Para facilitar o desenvolvimento, a janela de missões sempre ativa
        #if UNITY_EDITOR
        Ativa = true;
        #endif

        // Para ajudar no desenvolvimento, este objeto sempre começará visível
        // quando a equipe começa o jogo direto na missão 2 ou na missão 3
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Equals("M2_Patio1_inicio") || currentScene.Equals("M3_Patio1_Chamado"))
            Ativa = true;
    }

    // Esse método deve ser chamado quando você quiser cadastrar uma missão
    // nesta janela de missões, basta passar a missão:quest
    public void AdicionarMissao(QuestClass quest)
    {
        if (listaDeQuestsRemovidas.Contains(quest)) return;
        if (listaDeQuestsAtivas.Contains(quest))
        {
            Debug.LogWarning("A quest '" + quest.description + "' já está na janela de missões e você está tentando adicioná-la novamente");
            return;
        }

        listaDeQuestsAtivas.Add(quest);
        corpoJanelaMissoes.AdicionarMissao(quest);
    }

    // Esse método deve ser chamado quando você quiser remover uma missão
    // nesta janela de missões, basta passar a missão:quest
    // Retorna a quest removida
    public QuestClass RemoverMissao(QuestClass quest)
    {
        if (!listaDeQuestsAtivas.Contains(quest)) return null;

        listaDeQuestsAtivas.Remove(quest);
        listaDeQuestsRemovidas.Add(quest);
        corpoJanelaMissoes.RemoverMissao(quest);
        return quest;
    }

    public bool CompletamenteExplorada()
    {
        return corpoJanelaMissoes.TodosOsBotoesForamAbertos();
    }
}
