using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableItemsPanel : MonoBehaviour {

    private ItemName[] midiasDisponiveis;

    [SerializeField]
    private Button botaoSelecionarMidia;

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
            var b = Instantiate(botaoSelecionarMidia, this.transform);
            b.image.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
            b.image.preserveAspect = true;
        }
    }
}
