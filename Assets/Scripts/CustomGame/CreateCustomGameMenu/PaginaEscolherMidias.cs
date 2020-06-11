using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginaEscolherMidias : MonoBehaviour {

    private CreateCustomGamePanel parentPanel;

    [SerializeField]
    private GameObject espacoParaBotoesMidiaDisponivelCustom;
    [SerializeField]
    private GameObject botaoMidiaDisponivelCustom;
    [SerializeField]
    private Sprite bordaMidiaNaoSelecionada;
    [SerializeField]
    private Sprite bordaMidiaSelecionada;

    private List<ItemName> midiasSelecionadas;
    public List<ItemName> MidiasSelecionadas
    {
        get
        {
            if (midiasSelecionadas == null)
                midiasSelecionadas = new List<ItemName>();
            return midiasSelecionadas;
        }
    }

    private void Awake()
    {
        parentPanel = GetComponentInParent<CreateCustomGamePanel>();
    }

    // Use this for initialization
    void Start () {
        ItemName[] midiasDisponiveis =
        {
            ItemName.ReprodutorAudio,
            ItemName.Gravador,
            ItemName.CameraPolaroid,
            ItemName.TVComVHS,
            ItemName.LivroDidatico,
            ItemName.LivroIlustrado,
            ItemName.VHS,
            ItemName.QuadroNegro,
            ItemName.Caderno,
            ItemName.Cartazes,
            ItemName.Jornais,
            ItemName.Retroprojetor,
            ItemName.Enciclopedia,
            ItemName.PalavrasCruzadas,
            ItemName.FolhaSulfite,
        };
        var ItemList = midiasDisponiveis.Select((midia) => new Item(midia));

        // Desativar botão de próx. página enquanto player não selecionar mídias
        parentPanel.botaoAvancarPagina.gameObject.SetActive(false);

        foreach (var midia in ItemList)
        {
            var botao = Instantiate(botaoMidiaDisponivelCustom);
            botao.transform.SetParent(espacoParaBotoesMidiaDisponivelCustom.transform);
            botao.transform.localScale = Vector3.one;

            botao.GetComponentInChildren<TextMeshProUGUI>().text = midia.FriendlyName;

            var spriteDaMidia = botao.GetComponentInChildren<ItemInUserInterface>();
            spriteDaMidia.ItemName = midia.ItemName;

            //
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
                    // Se esta era a única mídia selecionada e portanto agora o
                    // jogador não tem nenhuma, desativar botão de próx. página
                    if (!midiasSelecionadas.Any())
                        parentPanel.botaoAvancarPagina.gameObject.SetActive(false);
                }
                else
                {
                    MidiasSelecionadas.Add(midia.ItemName);
                    var bordaObj = button.transform.parent;
                    bordaObj.GetComponent<Image>().sprite = bordaMidiaSelecionada;
                    parentPanel.botaoAvancarPagina.gameObject.SetActive(true);
                }
            });
        }
	}
}
