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

    public void AdicionarMissao(string tituloMissao, string[] ordensMissao)
    {
        var botao = Instantiate(prefabBotaoMissao);
        botao.transform.SetParent(this.transform);
        botao.transform.localScale = Vector3.one;

        botao.Configurar(tituloMissao, ordensMissao);

        botoesMissao.Add(botao);
    }
}
