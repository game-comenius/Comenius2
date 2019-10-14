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
    public int reprodutoraudioPoints = 0;
    public int cdPoints = 0;
    public int gravadorPoints = 0;
    public int gravacaoPoints = 0;
    public int cameraPolaroidPoints = 0;
    public int fotografiaPoints = 0;
    public int tvPoints = 0;
    public int vhsPoints = 0;
    public int cartazComPenasPoints = 0;
    public int livroPoints = 0;
    public int livroilustradoPoints = 0;
    public int quadroNegroPoints = 0;
    public int quadroNegroStencilPoints = 0;
    public int cartazesPoints = 0;
    public int mapaPoints = 0;
    public int cadernoPoints = 0;        
    public int jornaisPoints = 0;        
    public int jornaiserevistasPoints = 0;
    

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
            case ItemName.ReprodutorAudio: return reprodutoraudioPoints;
            case ItemName.Cd: return cdPoints;
            case ItemName.Gravador: return gravadorPoints;
            case ItemName.Gravacao: return gravacaoPoints;
            case ItemName.CameraPolaroid: return cameraPolaroidPoints;
            case ItemName.Fotografia: return fotografiaPoints;
            case ItemName.TV: return tvPoints;
            case ItemName.VHS: return vhsPoints;
            case ItemName.CartazComColecaoDePenas: return cartazComPenasPoints;
            case ItemName.Livro: return livroPoints;
            case ItemName.LivroIlustrado: return livroilustradoPoints;
            case ItemName.QuadroNegro: return quadroNegroPoints;
            case ItemName.QuadroNegroComEstencil: return quadroNegroStencilPoints;
            case ItemName.Cartazes: return cartazesPoints;
            case ItemName.Mapa: return mapaPoints;
            case ItemName.Caderno: return cadernoPoints;
            case ItemName.Jornais: return jornaisPoints;
            case ItemName.JornaisEResvistas: return jornaiserevistasPoints;
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
