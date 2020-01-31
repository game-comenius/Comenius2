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
    public int vhsEditadoPoints = 0;
    public int cartazComPenasPoints = 0;
    public int livroPoints = 0;
    public int livroilustradoPoints = 0;
    public int quadroNegroPoints = 0;
    public int quadroNegroStencilPoints = 0;
    public int cartazesPoints = 0;
    public int cartasComCanetasPoints= 0;
    public int mapaPoints = 0;
    public int cadernoPoints = 0;        
    public int jornaisPoints = 0;
    public int jornaiserevistasPoints = 0;
    public int retrorojetorSLidesMapaPoints = 0;
    public int retroprojetorSlidesLinhadoTempoPoints = 0;
    public int retroprojetorSlidesCicloDoTrabalhoPoints = 0;
    public int diarioPoints = 0;
  
    

    // Campos relacionados a UI para destacar o momento selecionado
    private static Image activeItemBorder;
    [SerializeField]
    private Image myItemBorder;
    [SerializeField]
    private Sprite selectedItemBorder;
    [SerializeField]
    private Sprite neutralItemBorder;

    [SerializeField]
    private Text descricaoGameObject;
    [SerializeField]
    public string minhaDescricao;


    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("PlanManager").GetComponent<PlanManager>().setSelectedMoment(gameObject);

        // Destaque visual do momento selecionado
        // Desabilitar o último destaque de slot de item
        if (activeItemBorder) activeItemBorder.sprite = neutralItemBorder;
        // Habilitar o destaque para este item
        myItemBorder.sprite = selectedItemBorder;
        // A borda ativa/selecionada agora é a borda deste item
        activeItemBorder = myItemBorder;

        // Apresenta a descrição deste momento na folha do planejamento
        descricaoGameObject.text = minhaDescricao;
    }

    //chame essa função para mudar o item no momento
    public void AddItem(ItemName newItem) {
        item = newItem;
        UpdateSprite();
    }

    public void ResetItem() {
        item = initialItem;
        UpdateSprite();
    }

    public double Points() {
        //pega pontuação no contexto do momento
        switch (item) {
            case ItemName.ReprodutorAudio: return reprodutoraudioPoints;
            case ItemName.Cd: return cdPoints;
            case ItemName.Gravador: return gravadorPoints;
            case ItemName.GravacaoPassaro: return gravacaoPoints;
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
            case ItemName.RetroprojetorSlideMapa: return retrorojetorSLidesMapaPoints;
            case ItemName.RetroprojetorSlideLinhaTempo: return retroprojetorSlidesLinhadoTempoPoints;
            case ItemName.RetroprojetorSlideCicloTrabalho: return retroprojetorSlidesCicloDoTrabalhoPoints;
            case ItemName.CartazComCanetas: return cartasComCanetasPoints;
            case ItemName.VhsEditado: return vhsEditadoPoints;
            case ItemName.Diario: return diarioPoints;
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

    private void UpdateSprite() {
        var image = GetComponent<Image>();
        if (item == ItemName.SemNome)
        {
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);

        }
        else
        {
            image.sprite = ItemSpriteDatabase.GetSpriteOf(item);
            image.color = new Color(1, 1, 1, 1);
        }
    }
}
