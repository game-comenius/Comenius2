using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class QuestHoster : MonoBehaviour
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

    [SerializeField] private UnityEvent questComplete;

	private void Awake()
    {
        ManagerQuest.AddHosterToQuest(_index, this);

        if (ManagerQuest.VerifyQuestIsComplete(_index))
        {
            questComplete.Invoke();
        }
    }

    private void OnDestroy()
    {
        ManagerQuest.RemoveHosterToQuest(_index, this);
    }

    public void Complete()
    {
        questComplete.Invoke();
    }
}
