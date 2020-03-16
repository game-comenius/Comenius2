using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CorpoJanelaMissoes : MonoBehaviour {

    [SerializeField]
    private BotaoTituloMissao prefabBotaoMissao;

    private Dictionary<QuestClass, BotaoTituloMissao> questsESeusBotoes;

    private void Awake()
    {
        questsESeusBotoes = new Dictionary<QuestClass, BotaoTituloMissao>();
    }

    public void AdicionarMissao(QuestClass quest)
    {
        BotaoTituloMissao botao = Instantiate(prefabBotaoMissao);
        botao.transform.SetParent(this.transform);
        botao.transform.localScale = Vector3.one;

        //botoesMissao.Add(botao);
        questsESeusBotoes.Add(quest, botao);

        botao.Configurar(quest);
    }

    public void RemoverMissao(QuestClass quest)
    {
        BotaoTituloMissao botaoQueDeveSerRemovido;
        var sucesso = questsESeusBotoes.TryGetValue(quest, out botaoQueDeveSerRemovido);

        if (sucesso)
        {
            questsESeusBotoes.Remove(quest);
            Destroy(botaoQueDeveSerRemovido.gameObject);
        }
    }

    public bool TodosOsBotoesForamAbertos()
    {
        return questsESeusBotoes.Values.All((b) => b.abertoPeloMenosUmaVez);
    }
}
