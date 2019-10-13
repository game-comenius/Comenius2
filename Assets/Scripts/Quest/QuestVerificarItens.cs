using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestVerificarItens : QuestScript 
{
    [SerializeField] private List<ItemName> itens = new List<ItemName>();

	public override void Avaliar() 
    {
        if (questInfo.questDependencias.Length != 0)
        {
            bool controlador = true;

            foreach (Vector2Int quest in questInfo.questDependencias)
            {
                if (!QuestManager.GetQuestControl(quest))
                {
                    controlador = false;
                    break;
                }
            }


            if (controlador)
            {
                dependenciasFeitas.Invoke();
            }
            else
            {
                dependenciasNaoFeitas.Invoke();
            }

            if (!QuestManager.GetQuestControl(questInfo.questIndex) && Analise())
            {
                bool temItens = true;

                foreach (ItemName item in itens)
                {
                    if (!Player.Instance.Inventory.Contains(item))
                    {
                        temItens = false;

                        break;
                    }
                }

                if (temItens)
                {
                    CompletarQuest();
                }
            }
        }
    }
}
