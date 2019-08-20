using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MidiaMomento : MonoBehaviour, IPointerClickHandler {
    
    public ItemName initialItem;
    private ItemName item; //item no slot
    //private double points = 0;

    //se você tiver uma ideia melhor pra fazer isso, por favor melhore isso.
    //está feito assim porque cada momento tem pontuações diferentes pra cada mídia, por isso todos são públicos para serem editados no unity.
    public double livroPoints = 0;
    public double cartazesPoints = 0;
    public double tvPoints = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //inicia seleção

        GameObject.Find("PlanManager").GetComponent<PlanManager>().setSelectedMoment(gameObject);
        Debug.Log("momento selecionado");
        //to-do: destaque visual no momento selecionado
    }

    //chame essa função para mudar o item no momento
    public void AddItem(ItemName newItem) {
        item = newItem;
        Debug.Log("novo item no momento");
    }
    
    public double Points() {
        //pega pontuação no contexto do momento
        switch (item) {
            case ItemName.Livro: return livroPoints;
            case ItemName.Cartazes: return cartazesPoints;
            case ItemName.TV: return tvPoints;
            default: return 0;
        }
    }

    public ItemName getItem() {
        return item;
    }

    // Use this for initialization
    void Start () {
        item = initialItem;
        UpdateSprite();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateSprite() {
        this.GetComponent<Image>().sprite = ItemSpriteDatabase.GetSpriteOf(item);
    }
}
