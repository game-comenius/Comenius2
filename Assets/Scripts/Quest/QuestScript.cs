﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestScript : MonoBehaviour
{
    private static List<QuestScript> questList = new List<QuestScript>();

    [HideInInspector]
    public QuestStruct questInfo = new QuestStruct
    {
        isQuest = false,
        questIndex = Vector2Int.zero,
        questDependencias = new Vector2Int[0]
    };

    public UnityEvent questFeita;

    public UnityEvent dependenciasFeitas;

    public UnityEvent dependenciasNaoFeitas;

    protected virtual void Awake()
    {
        questList.Add(this);
    }

    protected virtual void Start()
    {
        Avaliar();
    }

    protected virtual void OnDestroy()
    {
        questList.Remove(this);
    }

    public void Avaliar()
    {
        if (questInfo.isQuest && QuestManager.GetQuestControl(questInfo.questIndex))
        {
            questFeita.Invoke();
        }
        else
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
            }
        }
    }

    public void ReavaliarTodasQuests()
    {
        foreach (QuestScript quest in questList)
        {
            if (quest != this)
            {
                quest.Avaliar();
            }
        }

        Avaliar();
    }

    public void ReavaliarTodasMenosEssa()
    {
        foreach (QuestScript quest in questList)
        {
            if (quest != this)
            {
                quest.Avaliar();
            }
        }
    }

    public void DestroirObjeto(Object obj)
    {
        if (obj is QuestScript)
        {
            QuestScript.questList.Remove(obj as QuestScript);
        }

        Destroy(obj);
    }
}
