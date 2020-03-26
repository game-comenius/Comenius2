using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaDoProfessor : LocalParaColocarItem {

    public override void ColocarItem(ItemName midia)
    {
        // Remover item anterior
        RemoverItem();

        itemNesteLocal = new GameObject("MidiaSobreEstaMesa");
        itemNesteLocal.AddComponent<SpriteRenderer>().sortingOrder = 5;
        itemNesteLocal.transform.SetParent(this.transform);

        var sr = itemNesteLocal.GetComponent<SpriteRenderer>();
        sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        sr.enabled = true;

        itemNesteLocal.transform.localPosition = posicaoDoItem;

        switch (midia)
        {
            case ItemName.TVComVHS:
            case ItemName.ReprodutorAudio:
                sr.transform.localScale = Vector3.one * .5f;
                sr.flipX = true;
                break;
            case ItemName.Mapa:
            case ItemName.GravacaoPassaro:
            case ItemName.Gravador:
                sr.transform.localScale = Vector3.one * .4f;
                break;
            case ItemName.Retroprojetor:
            case ItemName.RetroprojetorSlideCicloTrabalho:
            case ItemName.RetroprojetorSlideLinhaTempo:
            case ItemName.RetroprojetorSlideMapa:
                sr.transform.localScale = Vector3.one * .3f;
                sr.transform.localPosition += new Vector3(.12f, .35f, 0);
                break;
            default:
                sr.transform.localScale = Vector3.one * .5f;
                break;
        }
    }
}
