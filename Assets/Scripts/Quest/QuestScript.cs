using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestScript : MonoBehaviour
{
    private static List<QuestScript> questList = new List<QuestScript>();

    [SerializeField] public QuestStruct questInfo = new QuestStruct();

    //public QuestStruct questInfo
    //{
    //    get
    //    {
    //        return _questInfo;
    //    }

    //    set
    //    {
    //        questInfo = value;
    //    }
    //}

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

        if (questInfo.isQuest && QuestManager.GetQuestControl(questInfo.questIndex))
        {
            questFeita.Invoke();
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
