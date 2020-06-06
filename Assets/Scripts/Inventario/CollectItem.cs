using System;
using System.Collections;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    public ItemName target;
    private bool done;

    private bool CanCollect
    {
        get
        {
            bool canCollect = false;

            var item = new Item(target);
            if (!item.IsAnUpgrade())
            {
                canCollect = true;
            }
            else
            {
                var baseItems = item.UpgradeFrom;
                foreach (var baseItem in baseItems)
                {
                    if (Player.Instance.Inventory.Contains(baseItem))
                        canCollect = true;
                }
            }
            return canCollect;
        }
    }

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
            var idDaMissao = Player.Instance.missionID;
            ItemDescriptionsInOneMission descricoes;
            switch (idDaMissao)
            {
                case 0:
                    descricoes = new Item(target).DescriptionsInMission1;
                    break;
                case 1:
                    descricoes = new Item(target).DescriptionsInMission2;
                    break;
                default:
                    descricoes = new Item(target).DescriptionsInMission3;
                    break;
            }
            var descricao = descricoes.DialogueWhenAcquired;

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

    public void addItem()
    {
        if (!done)
        {
            Player.Instance.Inventory.Add(target);
            done = true;
        }
    }

    // Impede que o jogador colete esta mídia se ele não possui a mídia base
    private void Update()
    {
        var polygonCollider = GetComponentInChildren<PolygonCollider2D>();
        if (polygonCollider) polygonCollider.enabled = CanCollect;
    }

    
}
