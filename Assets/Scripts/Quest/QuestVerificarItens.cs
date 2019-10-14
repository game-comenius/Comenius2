using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestVerificarItens : QuestScript 
{
    [SerializeField] private List<ItemName> itens = new List<ItemName>();

	public override void Avaliar() 
    {
        Debug.Log(1);

        if (questInfo.questDependencias.Length != 0)
        {
            Debug.Log(2);

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

            Debug.Log(!QuestManager.GetQuestControl(questInfo.questIndex));

            Debug.Log(Analise());

            if (!QuestManager.GetQuestControl(questInfo.questIndex) && Analise())
            {
                bool temItens = true;

                foreach (ItemName item in itens)
                {
                    Debug.Log(item.ToString());

                    Debug.Log(!Player.Instance.Inventory.Contains(item));

                    if (!Player.Instance.Inventory.Contains(item))
                    {
                        temItens = false;

                        break;
                    }
                }

                if (temItens)
                {
                    Debug.Log(3);

                    CompletarQuest();
                }
            }
        }
    }
}
