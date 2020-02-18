using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class QuestGuest : QuestMiscellanous
{
    [Tooltip ("Usar valores negativos para SideQuests.")]
    [SerializeField] private int _index;
    public int index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }

    public UnityEvent questComplete;
    public UnityEvent questAvailable;
    public UnityEvent questNotAvailable;

	private void Awake()
    {
        ManagerQuest.AddHosterToQuest(_index, this);

        if (ManagerQuest.VerifyQuestIsAvailable(index)) 
        {
            if (ManagerQuest.VerifyQuestIsComplete(index))
            {
                Complete();
            }
            else
            {
                MakeAvailable();
            }
        }
        else
        {
            MakeNotAvailable();
        }
    }

    private void OnDestroy()
    {
        ManagerQuest.RemoveHosterToQuest(_index, this);
    }

    public void Complete()
    {
        questComplete.Invoke();
        questComplete.RemoveAllListeners();
    }

    public void MakeAvailable()
    {
        questAvailable.Invoke();
        questAvailable.RemoveAllListeners();
    }

    public void MakeNotAvailable()
    {
        questNotAvailable.Invoke();
        questNotAvailable.RemoveAllListeners();
    }

    public void TakeStep()
    {
        ManagerQuest.QuestTakeStep(_index);
    }
}

#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(QuestGuest))]
public class QuestHosterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        int questIndex = serializedObject.FindProperty("_index").intValue;

        GUIContent content = new GUIContent { text = ManagerQuest.GetQuestDescription(questIndex) };

        EditorGUILayout.LabelField(content);

        base.OnInspectorGUI();
    }
}
#endif
#endregion