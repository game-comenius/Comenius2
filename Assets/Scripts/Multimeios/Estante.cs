using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estante : MonoBehaviour {

    [SerializeField]
    private GameObject modoEstanteAbertaUI;

	private EstanteUI estanteUI;

    public List<ItemName> Items { get; private set; }

    private void Awake()
    {
        Items = new List<ItemName>
        {
            // Itens que aparecem por padrão dentro da estante
            ItemName.ReprodutorAudio,
            ItemName.Gravador,
            ItemName.Camera,
            ItemName.TV
        };
    }

    private void Start()
    {
        estanteUI = modoEstanteAbertaUI.GetComponentInChildren<EstanteUI>();
        modoEstanteAbertaUI.SetActive(false);

        // Remover items da estante que o jogador já coletou
        var playerInventory = Player.Instance.Inventory;
        Items.RemoveAll(playerInventory.Contains);
    }

    public void Remove(ItemName item)
    {
        Items.Remove(item);
        estanteUI.DisplayItems();
    }

    public void OnMouseUpAsButton()
    {
        modoEstanteAbertaUI.SetActive(true);
        estanteUI.DisplayItems();
    }


    // Trocar por algo melhor para fechar a interface da estante
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && modoEstanteAbertaUI.activeInHierarchy == true) {
            modoEstanteAbertaUI.SetActive(false);
        }
    }
}
