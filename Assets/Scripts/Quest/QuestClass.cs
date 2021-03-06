﻿using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuestClass
{
    private int _index;
    public int index
    {
        get
        {
            return _index;
        }
    }

    private string _description;
    public string description
    {
        get
        {
            return _description;
        }
    }

    private IQuest _questType;
    public IQuest questType
    {
        get
        {
            return _questType;
        }
    }

    private int[] _dependencies;
    public int[] dependencies
    {
        get
        {
            return _dependencies;
        }
    }

    private string[] _passosDaQuest;
    public string[] passosDaQuest { get { return _passosDaQuest; } }

    public List<QuestGuest> guests = new List<QuestGuest>();


    public QuestClass(int index, string description, IQuest questType, int[] dependencies)
    {
        _index = index;
        _description = description;
        _questType = questType;
        _dependencies = dependencies;
        _passosDaQuest = new string[0];
    }

    public QuestClass(int index, string description, IQuest questType, int[] dependencies, params string[] passosDaQuest)
    {
        _index = index;
        _description = description;
        _questType = questType;
        _dependencies = dependencies;
        _passosDaQuest = passosDaQuest;
    }


    public bool QuestAvailable() //As dependencias estão feitas?
    {
        foreach (int dependency in dependencies)
        {
            if (dependency > 0)
            {
                if (!ManagerQuest.VerifyQuestIsComplete(dependency))
                {
                    return false;
                }
            }
            else if (dependency < 0)
            {
                if (ManagerQuest.VerifyQuestIsComplete(-dependency))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void TakeStep()
    {
        if (QuestAvailable())
        {
            _questType.TakeStep();
        }

        if (IsComplete())
        {
            for (int i = 0; i < guests.Count; i++)
            {
                guests[i].Complete();
            }

            ManagerQuest.UpdateAvailability(index);
        }
    }

    public bool IsComplete()
    {
        return (QuestAvailable() && _questType.IsComplete());
    }

    public void MakeAvailable()
    {
        for (int i = 0; i < guests.Count; i++)
        {
            guests[i].MakeAvailable();
        }
    }

    public bool IsDependentOf(int index)
    {
        foreach (int dependency in dependencies)
        {
            if (dependency == index || dependency == -index)
            {
                return true;
            }
        }

        return false;
    }
}
