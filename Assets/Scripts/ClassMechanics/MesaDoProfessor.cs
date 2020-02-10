﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaDoProfessor : LocalParaColocarItem {

    //[SerializeField]
    //private Vector2 posicaoObjetoNaMesa;

    //private GameObject objetoSobreEstaMesa;

    //public void ApresentarMidia(ItemName midia)
    //{
    //    if (objetoSobreEstaMesa == null)
    //    {
    //        objetoSobreEstaMesa = new GameObject("MidiaSobreEstaMesa");
    //        objetoSobreEstaMesa.AddComponent<SpriteRenderer>().sortingOrder = 5;
    //        objetoSobreEstaMesa.transform.SetParent(this.transform);
    //    }

    //    var sr = objetoSobreEstaMesa.GetComponent<SpriteRenderer>();
    //    sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
    //    sr.enabled = true;

    //    switch (midia)
    //    {
    //        case ItemName.TV:
    //        case ItemName.ReprodutorAudio:
    //            sr.transform.localScale = Vector3.one * .5f;
    //            sr.flipX = true;
    //            break;
    //        default:
    //            break;
    //    }
    //    objetoSobreEstaMesa.transform.localPosition = posicaoObjetoNaMesa;
    //}

    public override void ColocarItem(ItemName midia)
    {
        if (itemNesteLocal == null)
        {
            itemNesteLocal = new GameObject("MidiaSobreEstaMesa");
            itemNesteLocal.AddComponent<SpriteRenderer>().sortingOrder = 5;
            itemNesteLocal.transform.SetParent(this.transform);
        }

        var sr = itemNesteLocal.GetComponent<SpriteRenderer>();
        sr.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        sr.enabled = true;

        switch (midia)
        {
            case ItemName.TV:
            case ItemName.ReprodutorAudio:
                sr.transform.localScale = Vector3.one * .5f;
                sr.flipX = true;
                break;
            default:
                break;
        }
        itemNesteLocal.transform.localPosition = posicaoDoItem;
    }
}
