using UnityEngine;

[System.Serializable]
public struct QuestStruct 
{
    public bool isQuest;

    public Vector2Int questIndex;

    public bool isQuestDependent;

    public Vector2Int[] questDependencias;
}
