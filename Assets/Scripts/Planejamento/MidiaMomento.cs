﻿using UnityEngine;
using UnityEngine.UI;

public class MidiaMomento : MonoBehaviour
{
    // Item inicial do slot
    public ItemName initialItem;
    // Item que está atualmente no slot
    private ItemName item; 

    //se você tiver uma ideia melhor pra fazer isso, por favor melhore isso.
    //está feito assim porque cada momento tem pontuações diferentes pra cada mídia, por isso todos são públicos para serem editados no unity.
    public int reprodutorAudioPoints = 0;
    public int cdPoints = 0;
    public int gravadorPoints = 0;
    public int gravacaoPassaroPoints = 0;
    public int cameraPolaroidPoints = 0;
    public int fotografiaPassaroPoints = 0;
    public int tvPoints = 0;
    public int vhsPoints = 0;
    public int vhsEditadoPoints = 0;
    public int cartazComPenasPoints = 0;
    public int livroDidaticoPoints = 0;
    public int livroilustradoPoints = 0;
    public int quadroNegroPoints = 0;
    public int tvComVhsPoints = 0;
    public int cartazesPoints = 0;
    public int cartasComCanetasPoints = 0;
    public int mapaPoints = 0;
    public int cadernoPoints = 0;
    public int jornaisPoints = 0;
    public int jornaiserevistasPoints = 0;
    public int retrorojetorSLidesMapaPoints = 0;
    public int retroprojetorSlidesLinhadoTempoPoints = 0;
    public int retroprojetorSlidesCicloDoTrabalhoPoints = 0;
    public int diarioPoints = 0;
    public int folhasulfitePoints = 0;
    public int vhsregionalismoPoints = 0;
    public int vhsregionalismoeditadoPoints = 0;
    public int enciclopediaPoints = 0;
    public int adedonhaPoints = 0;
    public int forcaPoints = 0;
    public int palavrascruzadasPoints = 0;
    public int cdsotaquesPoints = 0;

    private Image image;

    //chame essa função para mudar o item no momento
    public void AddItem(ItemName newItem)
    {
        item = newItem;
        UpdateSprite();
    }

    public void ResetItem()
    {
        item = initialItem;
        UpdateSprite();
    }

    public double Points()
    {
        //pega pontuação no contexto do momento
        switch (item)
        {
            case ItemName.ReprodutorAudio: return reprodutorAudioPoints;
            case ItemName.Cd: return cdPoints;
            case ItemName.Gravador: return gravadorPoints;
            case ItemName.GravacaoPassaro: return gravacaoPassaroPoints;
            case ItemName.CameraPolaroid: return cameraPolaroidPoints;
            case ItemName.FotografiaPassaro: return fotografiaPassaroPoints;
            case ItemName.TVComVHS: return tvPoints;
            case ItemName.VHS: return vhsPoints;
            case ItemName.CartazComColecaoDePenas: return cartazComPenasPoints;
            case ItemName.LivroDidatico: return livroDidaticoPoints;
            case ItemName.LivroIlustrado: return livroilustradoPoints;
            case ItemName.QuadroNegro: return quadroNegroPoints;
            case ItemName.TVComVHSPassaros: return tvComVhsPoints;
            case ItemName.Cartazes: return cartazesPoints;
            case ItemName.Mapa: return mapaPoints;
            case ItemName.Caderno: return cadernoPoints;
            case ItemName.Jornais: return jornaisPoints;
            case ItemName.JornaisERevistas: return jornaiserevistasPoints;
            case ItemName.RetroprojetorSlideMapa: return retrorojetorSLidesMapaPoints;
            case ItemName.RetroprojetorSlideLinhaTempo: return retroprojetorSlidesLinhadoTempoPoints;
            case ItemName.RetroprojetorSlideCicloTrabalho: return retroprojetorSlidesCicloDoTrabalhoPoints;
            case ItemName.CartazComCanetas: return cartasComCanetasPoints;
            case ItemName.VhsEditado: return vhsEditadoPoints;
            case ItemName.Diario: return diarioPoints;
            case ItemName.FolhaSulfite: return folhasulfitePoints;
            case ItemName.VHSregionalismo: return vhsregionalismoPoints;
            case ItemName.VHSregionalismoEditado: return vhsregionalismoeditadoPoints;
            case ItemName.Enciclopedia: return enciclopediaPoints;
            case ItemName.Adedonha: return adedonhaPoints;
            case ItemName.Forca: return forcaPoints;
            case ItemName.PalavrasCruzadas: return palavrascruzadasPoints;
            case ItemName.CDsotaques: return cdsotaquesPoints;
            default: return 0;
        }
    }

    public ItemName getItem()
    {
        return item;
    }

    // Use this for initialization
    void Start()
    {
        item = initialItem;

        image = GetComponent<Image>();
        image.preserveAspect = true;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (item == ItemName.SemNome)
        {
            image.enabled = false;
        }
        else
        {
            image.sprite = ItemSpriteDatabase.GetSpriteOf(item);
            image.enabled = true;
        }
    }
}
