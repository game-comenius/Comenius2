using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estante : MonoBehaviour {

    [SerializeField]
    private GameObject modoEstanteAbertaUI;

	private EstanteUI estanteUI;

    public List<ItemName> Items { get; private set; }

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

    private void Awake()
    {
        Items = new List<ItemName>
        {
            // Itens que aparecem por padrão dentro da estante
            //ItemName.ReprodutorAudio,
            //ItemName.Gravador,
            //ItemName.CameraPolaroid,
            ItemName.TV,
            ItemName.Livro,
            ItemName.JornaisEResvistas
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

    private void OnMouseUpAsButton()
    {
        if (!GameManager.uiSendoUsada && !Player.Instance.GetComponent<PathFinder>().hasTarget)
        {
            Player.Instance.GetComponent<PathFinder>().hasTarget = true;

            StartCoroutine(Test());
        }
    }

    private IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();

        if (!GameManager.uiSendoUsada)
        {
            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            PathFinder.gotToInteractable += Interact;

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point);
        }
    }

    private void Interact()
    {
        GameObject.Find("LocalFade").GetComponent<FadeEffect>().Fadeout();
        modoEstanteAbertaUI.SetActive(true);
        estanteUI.DisplayItems();
        GameManager.UISendoUsada();
    }

    // Trocar por algo melhor para fechar a interface da estante
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && modoEstanteAbertaUI.activeInHierarchy == true) {
            modoEstanteAbertaUI.SetActive(false);
            GameManager.UINaoSendoUsada();
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        for (int i = 0; i < interactOffset.Length; i++)
        {
            Gizmos.DrawSphere(transform.position + interactOffset[i], 0.07f);
        }
    }
}
