using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalParaColocarItem : MonoBehaviour {

    [SerializeField]
    protected Vector2 posicaoDoItem = Vector2.zero;

    protected GameObject itemNesteLocal;

    public abstract void ColocarItem(ItemName midia);


    public void RemoverItem()
    {
        if (itemNesteLocal) Destroy(itemNesteLocal.gameObject);
        itemNesteLocal = null;
    }

    // Desenhar ponto no editor para mostrar onde vai ficar o objeto na mesa
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var pItem = new Vector3(posicaoDoItem.x, posicaoDoItem.y, 0);
        Gizmos.DrawSphere(transform.localPosition + pItem, .1f);
    }
}
