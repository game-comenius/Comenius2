using System;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    public ItemName target;
    bool done = false;

    private void Start()
    {
        AcrescentarDescricaoDaMidiaNaFalaDaLurdinha();
    }

    private void AcrescentarDescricaoDaMidiaNaFalaDaLurdinha()
    {
        // Adicionar descrição da mídia no diálogo que a Lurdinha fala ao
        // conquistar a mídia. Essa é a StandardDescription de Item.cs
        var npcDialogo = GetComponentInChildren<NpcDialogo>();
        if (npcDialogo)
        {
            // Descricao que será adicionada à fala de Lurdinha
            var descricao = new Item(target).DescriptionsInMission3.StandardDescription;

            // Extrair o array de falas da Lurdinha
            var falas = npcDialogo.dialogoPrincipal.nodulos[0].falas;
            // Redimensionar o array para aceitar mais um paragrafo de fala
            Array.Resize(ref falas, falas.Length + 1);

            var ultimoIndice = falas.Length - 1;
            falas[ultimoIndice] = new GameComenius.Dialogo.Fala
            {
                fala = descricao,
                personagem = GameComenius.Dialogo.Personagens.Lurdinha,
                emocao = GameComenius.Dialogo.Expressao.Serio
            };

            // Encaixar o array com as (falas originais + fala nova) no
            // conjunto de falas da Lurdinha
            npcDialogo.dialogoPrincipal.nodulos[0].falas = falas;
        }
    }

    public void addItem() {
        if (!done) {
            Player.Instance.Inventory.Add(target);
            done = true;
        }
    }	
}
