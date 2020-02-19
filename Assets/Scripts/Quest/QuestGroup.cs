using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGroup
{
    private string _name;
    public string name
    {
        get
        {
            return _name;
        }
    }

    private int[] _indexes;
    public int[] indexes
    {
        get
        {
            return _indexes;
        }
    }

    public QuestGroup(string name, int[] indexes)
    {
        _name = name;
        _indexes = indexes;
    }
}
