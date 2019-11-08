using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableItemsPanel : MonoBehaviour {

    
    private ItemName[] midiasDisponiveis;

    // Prefab do botão que será instanciado para cada uma das mídias
    [SerializeField]
    private SelectItemButton botaoSelecionarMidia;

    // Use this for initialization
    void Start () {
        // Mídias que estarão disponíveis para o jogador selecionar
        midiasDisponiveis = new ItemName[]
        {
            ItemName.Caderno,
            ItemName.Gravador,
            ItemName.Livro,
            ItemName.Cartazes,
            ItemName.CameraPolaroid,
            ItemName.QuadroNegro,
            ItemName.ReprodutorAudio,
            ItemName.TV
        };

        // Popular o panel de mídias disponíveis com as mídias do jogo custom
        foreach (var midia in midiasDisponiveis)
        {
            var b = Instantiate<SelectItemButton>(botaoSelecionarMidia, this.transform);
            b.Item = midia;
        }
    }

    public ItemName[] MidiasSelecionadas()
    {
        var midiasSelecionadas = new List<ItemName>();

        var botoes = GetComponentsInChildren<SelectItemButton>();
        foreach (var botao in botoes)
        {
            if (botao.Selected)
                midiasSelecionadas.Add(botao.Item);
        }

        return midiasSelecionadas.ToArray();
    }
}
