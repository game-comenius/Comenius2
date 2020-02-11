using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaDoAluno : LocalParaColocarItem
{
    public override void ColocarItem(ItemName midia)
    {
        if (itemNesteLocal == null)
        {
            itemNesteLocal = new GameObject("MidiaSobreEstaMesa");
            itemNesteLocal.AddComponent<SpriteRenderer>();//.sortingOrder = 5; //Comentei para não ficar na frente da Lurdinha quando ela estiver andando
            itemNesteLocal.transform.SetParent(this.transform);
        }

        var sr = itemNesteLocal.GetComponent<SpriteRenderer>();
        sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        sr.enabled = true;

        switch (midia)
        {
            case ItemName.Jornais:
            case ItemName.JornaisEResvistas:
            case ItemName.Caderno:
                sr.transform.localScale = Vector3.one * .15f;
                break;
            default:
                sr.transform.localScale = Vector3.one * .25f;
                break;
        }

        sr.transform.localPosition = (Vector3)posicaoDoItem + new Vector3(0f, 0f, -0.1f);//o vetor3 é para ficar na frente da mesa
    }
}
