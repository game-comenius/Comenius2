﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Essa classe é temporária! Por favor, trocar quando tivermos algo melhor
// Isto aqui é para fazer o pássaro do pátio lhe dar a gravação do seu canto
public class Passaro : MonoBehaviour {

    // bool para que a OnMouseUpAsButton só faça o seu trabalho 1 vez
    private bool a;

    private void OnMouseUpAsButton()
    {
        if (!a)
        {
            //Player.Instance.Inventory.Add(ItemName.Gravador);
            Player.Instance.Inventory.Add(ItemName.GravacaoPassaro);
            Debug.Log("Pássaro Cantou!");
            a = true;
        }
    }
}
