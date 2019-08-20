using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MidiaMomento : MonoBehaviour, IPointerClickHandler {
    
    public ItemName initialItem;
    private ItemName item; //item no slot
    private double points = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //inicia seleção

        Debug.Log("clicou no momento");
        //to-do: passar própria referência para manager, inventário vai usar referência para colocar item
        GameObject.Find("PlanManager").GetComponent<PlanManager>().setSelectedMoment(gameObject);
    }

    //chame essa função para mudar o item no momento
    public void AddItem(ItemName newItem) {
        item = newItem;
        Debug.Log("novo item no momento");
    }
    
    public double Points() {
        //pega pontuação no contexto do momento
        switch (item) {
            case ItemName.Livro: return points = 1;
            default: return points = 0;
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
