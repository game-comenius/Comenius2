using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginaEscolherMidias : MonoBehaviour {

    [SerializeField]
    private GameObject espacoParaBotoesMidiaDisponivelCustom;
    [SerializeField]
    private GameObject botaoMidiaDisponivelCustom;
    [SerializeField]
    private Sprite bordaMidiaNaoSelecionada;
    [SerializeField]
    private Sprite bordaMidiaSelecionada;

    public List<ItemName> MidiasSelecionadas { get; private set; }

	// Use this for initialization
	void Start () {
        ItemName[] midiasDisponiveis =
        {
            ItemName.ReprodutorAudio,
            ItemName.Gravador,
            ItemName.CameraPolaroid,
            ItemName.TVComVHS,
            ItemName.LivroDidatico,
            ItemName.QuadroNegro,
            ItemName.Caderno,
            ItemName.Cartazes,
            ItemName.Jornais,
            ItemName.Retroprojetor,
            ItemName.Enciclopedia,
            ItemName.FolhaSulfite,
        };
        var ItemList = midiasDisponiveis.Select((midia) => new Item(midia));

        foreach (var midia in ItemList)
        {
            var botao = Instantiate(botaoMidiaDisponivelCustom);
            botao.transform.SetParent(espacoParaBotoesMidiaDisponivelCustom.transform);
            botao.transform.localScale = Vector3.one;

            botao.GetComponentInChildren<TextMeshProUGUI>().text = midia.DescriptionsInMission3.StandardDescription;

            var spriteDaMidia = botao.GetComponentInChildren<ItemInUserInterface>();
            spriteDaMidia.ItemName = midia.ItemName;

            var button = spriteDaMidia.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                // Quando o jogador apertar sobre o sprite da mídia, adicionar
                // ou remover essa mídia do jogo custom
                if (MidiasSelecionadas.Contains(midia.ItemName))
                {
                    MidiasSelecionadas.Remove(midia.ItemName);
                    var bordaObj = button.transform.parent;
                    bordaObj.GetComponent<Image>().sprite = bordaMidiaNaoSelecionada;
                }
                else
                {
                    MidiasSelecionadas.Add(midia.ItemName);
                    var bordaObj = button.transform.parent;
                    bordaObj.GetComponent<Image>().sprite = bordaMidiaSelecionada;
                }
            });
        }
        MidiasSelecionadas = new List<ItemName>();
	}
}
