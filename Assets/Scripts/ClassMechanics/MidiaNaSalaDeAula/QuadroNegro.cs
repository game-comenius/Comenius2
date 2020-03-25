using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroNegro : LocalParaColocarItem {

    public override void ColocarItem(ItemName midia)
    {
        if (itemNesteLocal == null)
        {
            itemNesteLocal = new GameObject("MidiaNoQuadro");
            itemNesteLocal.AddComponent<SpriteRenderer>();
            itemNesteLocal.transform.SetParent(this.transform);
        }

        var sr = itemNesteLocal.GetComponent<SpriteRenderer>();
        sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        sr.enabled = true;

        switch (midia)
        {
            case ItemName.Cartazes:
                sr.transform.localScale = Vector3.one * .8f;
                break;
            case ItemName.CartazComColecaoDePenas:
                sr.transform.localScale = Vector3.one * .8f;
                sr.flipX = true;
                break;
            default:
                Debug.LogWarning("Não é possível colocar esta mídia sobre o quadro");
                break;
        }

        // Profundidade do item será a mesma que a do quadro negro
        var profundidade = this.transform.localPosition.z;
        itemNesteLocal.transform.localPosition = new Vector3(posicaoDoItem.x, posicaoDoItem.y, profundidade);
    }
}
