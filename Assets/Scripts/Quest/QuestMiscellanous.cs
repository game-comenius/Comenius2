using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMiscellanous : MonoBehaviour
{
    public void DestroirObjeto(Object obj)
    {
        if (obj is QuestScript)
        {
            QuestScript.questList.Remove(obj as QuestScript);
        }

        Destroy(obj);
    }

    public void UISendoUsada()
    {
        GameManager.UISendoUsada();
    }

    public void UINaoSendoUsada()
    {
        GameManager.UINaoSendoUsada();
    }

    public void SetMissionGuide(string mission)
    {
        MissionGuideManager.missionGuideManager.SetMissionGuide(mission);
    }

    public void ShowMissionGuide(bool action)
    {
        MissionGuideManager.missionGuideManager.ShowMissionGuide(action);
    }


    public void ShowMidiaCounter(bool action)
    {
        MissionGuideManager.missionGuideManager.ShowMidiaCounter(action);
    }

}
