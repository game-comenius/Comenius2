using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroNegro : LocalParaColocarItem {

    public override void ColocarItem(ItemName midia)
    {
        if (itemNesteLocal == null)
        {
            itemNesteLocal = new GameObject("MidiaNoQuadro");
            itemNesteLocal.AddComponent<SpriteRenderer>().sortingOrder = 5;
            itemNesteLocal.transform.SetParent(this.transform);
        }

        var sr = itemNesteLocal.GetComponent<SpriteRenderer>();
        sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        sr.enabled = true;

        switch (midia)
        {
            case ItemName.Cartazes:
                sr.transform.localScale = Vector3.one * .7f;
                break;
            default:
                Debug.Log("Não é possível colocar esta mídia sobre o quadro");
                break;
        }
        itemNesteLocal.transform.localPosition = posicaoDoItem;
    }
}
