using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptProvisorio : MonoBehaviour
{
    public ItemName[] midias = new ItemName[3];
    public double[] points = new double[3];
    public double totalPoint = 0;

    public void FinalizarPlanejamento()
    {
        Player.Instance.chosenMedia = midias;
        Player.Instance.points = points;
        Player.Instance.totalMissionPoints = totalPoint;

        QuestManager.SetQuestControl(GetComponent<QuestScript>().questInfo.questIndex, true);

        QuestScript.ReavaliarTodasQuests();
    }
}
