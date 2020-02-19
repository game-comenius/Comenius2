using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorpoJanelaMissoes : MonoBehaviour {

    [SerializeField]
    private BotaoTituloMissao prefabBotaoMissao;

    private List<BotaoTituloMissao> botoesMissao;

    private void Awake()
    {
        botoesMissao = new List<BotaoTituloMissao>();
    }

    public BotaoTituloMissao AdicionarMissao(QuestGroup quest)
    {
        BotaoTituloMissao botao = Instantiate(prefabBotaoMissao);
        botao.transform.SetParent(this.transform);
        botao.transform.localScale = Vector3.one;

        if (botao.Configurar(quest))
        {
            botoesMissao.Add(botao);

            return botao;
        }

        Destroy(botao.gameObject);

        return null;
    }

    public bool TodosOsBotoesForamAbertos()
    {
        return botoesMissao.TrueForAll((b) => b.abertoPeloMenosUmaVez);
    }
}
