using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ManagerQuest : MonoBehaviour
{
    private static int mission;

    public static QuestGroup[] questGroups
    {
        get
        {
            try
            {
                switch (Player.Instance.missionID)
                {
                    default:
                        return Mission1._questGroups;
                    case 1:
                        return Mission2._questGroups;
                }
            }
            catch (System.NullReferenceException)
            {
                int missionID = FindObjectOfType<Player>().missionID;

                switch (missionID)
                {
                    default:
                        return Mission1._questGroups;
                    case 1:
                        return Mission2._questGroups;
                }
            }
        }
    }

    public static QuestClass[] mainQuests
    {
        get
        {
            try
            {
                switch (Player.Instance.missionID)
                {
                    default:
                        return Mission1._mainQuests;
                    case 1:
                        return Mission2._mainQuests;
                }
            }
            catch(System.NullReferenceException)
            {
                int missionID = FindObjectOfType<Player>().missionID;

                switch (missionID)
                {
                    default:
                        return Mission1._mainQuests;
                    case 1:
                        return Mission2._mainQuests;
                }
            }
        }
    }

    public static QuestClass[] sideQuests
    {
        get
        {
            try
            {
                switch (Player.Instance.missionID)
                {
                    default:
                        return Mission1._sideQuests;
                    case 1:
                        return Mission2._sideQuests;
                }
            }
            catch (System.NullReferenceException)
            {
                int missionID = FindObjectOfType<Player>().missionID;

                switch (missionID)
                {
                    default:
                        return Mission1._sideQuests;
                    case 1:
                        return Mission2._sideQuests;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static bool VerifyQuestIsComplete(int index)
    {
        if (index == 0)
        {
            return false;
        }
        else if (index > 0 && index <= 10000)
        {
            foreach(QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    return quest.IsComplete();
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    return quest.IsComplete();
                }
            }
        }

        Debug.Log("Quest " + index + "not found.");

        return false;
    }

    public static bool VerifyQuestIsAvailable(int index)
    {
        if (index == 0)
        {
            return true;
        }
        else if (index > 0 && index <= 10000)
        {
            foreach(QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    return quest.QuestAvailable();
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    return quest.QuestAvailable();
                }
            }
        }

        Debug.Log("Quest " + index + "not found.");

        return false;
    }

    public static string GetQuestDescription(int index)
    {
        if (index > 0 && index <= 10000)
        {
            foreach(QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    return quest.description;
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    return quest.description;
                }
            }
        }

        return "";
    }

    public static void QuestTakeStep(int index)
    {
        if (index > 0 && index <= 10000)
        {
            foreach (QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    quest.TakeStep();
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    quest.TakeStep();
                }
            }
        }

        SetupQuestLog();

        //if (VerifyQuestIsComplete(index))
        //{
        //    for (int i = 0; i < mainQuests.Length; i++)
        //    {
        //        if (mainQuests[i].QuestAvailable())
        //        {
        //            for (int j = 0; j < mainQuests[i].hosters.Count; j++)
        //            {
        //                mainQuests[i].hosters[j].MakeAvailable();
        //            }
        //        }
        //    }
        //}
    }

    public static void SetupQuestLog()
    {    
        JanelaMissoes janelaMissoes = FindObjectOfType<JanelaMissoes>();

        if (janelaMissoes != null)
        {
            janelaMissoes.Clear();

            //foreach (QuestGroup group in questGroups)
            //{
            //    janelaMissoes.AdicionarMissao(group);
            //}

            for (int i = 0; i < questGroups.Length; i++)
            {
                janelaMissoes.AdicionarMissao(questGroups[i]);
            }
        }
    }

    public static void UpdateAvailability(int index)
    {
        for (int i = 0; i < mainQuests.Length; i++)
        {
            if (mainQuests[i].IsDependentOf(index) || mainQuests[i].IsDependentOf(-index))
            {
                if (mainQuests[i].QuestAvailable() && !mainQuests[i].IsComplete())
                {
                    mainQuests[i].MakeAvailable();
                }
            }
        }
        for (int i = 0; i < sideQuests.Length; i++)
        {
            if (sideQuests[i].IsDependentOf(index) || sideQuests[i].IsDependentOf(-index))
            {
                if (sideQuests[i].QuestAvailable() && !sideQuests[i].IsComplete())
                {
                    sideQuests[i].MakeAvailable();
                }
            }
        }
    }

    public static void AddHosterToQuest(int index, QuestGuest hoster)
    {
        if (index > 0 && index <= 10000)
        {
            foreach (QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    quest.guests.Add(hoster);
                    return;
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    quest.guests.Add(hoster);
                    return;
                }
            }
        }
    }

    public static void RemoveHosterToQuest(int index, QuestGuest hoster)
    {
        if (index > 0 && index <= 10000)
        {
            foreach (QuestClass quest in mainQuests)
            {
                if (quest.index == index)
                {
                    quest.guests.Remove(hoster);
                    return;
                }
            }
        }
        else if (index > 10000)
        {
            foreach (QuestClass quest in sideQuests)
            {
                if (quest.index == index)
                {
                    quest.guests.Remove(hoster);
                    return;
                }
            }
        }
    }
}

#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(ManagerQuest))]
public class NewQuestManagerEditor : Editor
{
    private bool[,] toggles;
    private bool[,] dependencies;

    private static bool mainQuest;
    private static bool sideQuest;

    private void OnEnable()
    {
        toggles = new bool[Mathf.Max(ManagerQuest.mainQuests.Length, ManagerQuest.sideQuests.Length), 2];
        dependencies = new bool[Mathf.Max(ManagerQuest.mainQuests.Length, ManagerQuest.sideQuests.Length), 2];
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        mainQuest = EditorGUILayout.Foldout(mainQuest, new GUIContent { text = "Main Quests" });
        if (mainQuest)
        {
            DrawQuestList(ManagerQuest.mainQuests, 0);
        }

        sideQuest = EditorGUILayout.Foldout(sideQuest, new GUIContent { text = "Side Quests" });
        if (sideQuest)
        {
            DrawQuestList(ManagerQuest.sideQuests, 1);
        }


        serializedObject.ApplyModifiedProperties();
    }

    private void DrawQuestList(QuestClass[] quests, int MainOrSide)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            QuestClass quest = quests[i];

            GUIStyle style = new GUIStyle();
            style = EditorStyles.foldout;
            style.fontStyle = FontStyle.Bold;

            string label = "  Quest " + quest.index + ": " + quest.description;

            if (quest.IsComplete())
            {
                label += " Complete!";
                style.fontStyle = FontStyle.Normal;
            }
            else if (!quest.QuestAvailable())
            {
                label += " Not Available!";
                style.fontStyle = FontStyle.Italic;
            }

            toggles[i, MainOrSide] = EditorGUILayout.Foldout(toggles[i, MainOrSide], new GUIContent { text = label }, style);

            if (toggles[i, MainOrSide])
            {
                if (quest.dependencies.Length != 0)
                {
                    label = "    Dependencies";

                    style = EditorStyles.label;
                    style.fontStyle = FontStyle.Normal;

                    dependencies[i, MainOrSide] = EditorGUILayout.Foldout(dependencies[i, MainOrSide], new GUIContent { text = label });

                    if (dependencies[i, MainOrSide])
                    {
                        foreach (int dependency in quest.dependencies)
                        {
                            string labelDep = "        Quest " + dependency;

                            if (quest.IsComplete())
                            {
                                labelDep += " - Complete!";
                                style.fontStyle = FontStyle.Bold;
                            }

                            EditorGUILayout.LabelField(new GUIContent { text = labelDep }, style);
                        }
                    }
                }

                ChooseQuestType(quest.questType);
            }

            style.fontStyle = FontStyle.Normal;
        }
    }

    private void ChooseQuestType(IQuest quest)
    {
        if (quest is CounterQuest)
        {
            DrawCounterQuest(quest as CounterQuest);
        }
        else if (quest is DoQuest)
        {
            //Do nothing
        }
    }

    private void DrawCounterQuest(CounterQuest quest)
    {
        EditorGUILayout.LabelField(new GUIContent { text = "    Status: " + quest.ProgressFraction() });
    }

    //private void DrawDependentQuest(DependentQuest quest)
    //{
    //    foreach (int depen in quest.dependencies)
    //    {
    //        string label = "    Quest " + depen;

    //        //Verificar se quest está completa e adicionar "Complete!".

    //        EditorGUILayout.LabelField(new GUIContent { text = label });
    //    }
    //}
}
#endif
#endregion