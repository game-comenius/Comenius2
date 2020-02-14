using System.Collections;
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

    public List<QuestHoster> hosters = new List<QuestHoster>();

    public QuestClass(int index, string description, IQuest questType, int[] dependencies)
    {
        _index = index;
        _description = description;
        _questType = questType;
        _dependencies = dependencies;
    }
    public bool QuestAvailable() //As dependencias estão feitas?
    {
        foreach (int dependency in dependencies)
        {
            if (!ManagerQuest.VerifyQuestIsComplete(dependency))
            {
                return false;
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
            for (int i = 0; i < hosters.Count; i++)
            {
                hosters[i].Complete();
            }
        }
    }

    public bool IsComplete()
    {
        return (QuestAvailable() && _questType.IsComplete());
    }
}
