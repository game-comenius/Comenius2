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
    public double livroilustradoPoints = 0;
    public double cartazesPoints = 0;
    public double tvPoints = 0;
    public double cadernoPoints = 0;
    public double gravadorPoints = 0;
    public double reprodutoraudioPoints = 0;
    public double vhsPoints = 0;
    public double cameraPoints = 0;
    public double jornaisPoints = 0;
    public double fotografiaPoints = 0;
    public double quadronegroPoints = 0;


    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //inicia seleção

        GameObject.Find("PlanManager").GetComponent<PlanManager>().setSelectedMoment(gameObject);
        Debug.Log("momento selecionado: " + GameObject.Find("PlanManager").GetComponent<PlanManager>().getSelectedMoment().name);
        //to-do: destaque visual no momento selecionado
    }

    //chame essa função para mudar o item no momento
    public void AddItem(ItemName newItem) {
        item = newItem;
        UpdateSprite();
    }
    
    public double Points() {
        //pega pontuação no contexto do momento
        switch (item) {
            case ItemName.Livro: return livroPoints;
            case ItemName.LivroIlustrado: return livroilustradoPoints;
            case ItemName.Cartazes: return cartazesPoints;
            case ItemName.TV: return tvPoints;
            case ItemName.Caderno: return cadernoPoints;
            case ItemName.ReprodutorAudio: return reprodutoraudioPoints;
            case ItemName.VHS: return vhsPoints;
            case ItemName.Camera: return cameraPoints;
            case ItemName.Jornais: return jornaisPoints;
            case ItemName.Fotografia: return fotografiaPoints;
            case ItemName.QuadroNegro: return quadronegroPoints;
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
