using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstanteUI : MonoBehaviour {

    [SerializeField]
    private List<EstanteItemUI> itemSlots;

    [SerializeField]
    private Estante estante;

    // Use this for initialization
    void Start () {
        DisplayItems();
	}

    public void DisplayItems()
    {
        // Limpar slots
        foreach (var slot in itemSlots)
        {
            slot.gameObject.SetActive(false);
        }

        // Popular slots
        var slots = itemSlots.GetEnumerator();
        foreach (var media in estante.Items)
        {
            if (slots.MoveNext())
            {
                slots.Current.Estante = estante;
                slots.Current.Item = media;
            }
        }
    }
}
