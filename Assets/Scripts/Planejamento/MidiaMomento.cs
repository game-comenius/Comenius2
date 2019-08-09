using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MidiaMomento : MonoBehaviour, IPointerClickHandler {
    
    public ItemName itemInicial;
    private ItemName item; //item no slot
    private double pontos = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //inicia seleção

        Debug.Log("clicou no momento");
    }

    public double Pontos() {
        //pega pontuação no contexto do momento
        switch (item) {
            case ItemName.Livro: return pontos = 1;
            default: return pontos = 0;
        }
    }

    // Use this for initialization
    void Start () {
        item = itemInicial;
        UpdateSprite();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateSprite() {
        this.GetComponent<Image>().sprite = ItemSpriteDatabase.GetSpriteOf(item);
    }
}
