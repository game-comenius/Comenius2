using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoQuest : IQuest
{
    private bool completed = false;

    public string GetQuestExibition(string questExibition)
    {
        return questExibition;
    }

    public bool IsComplete()
    {
        return completed;
    }

    public void TakeStep()
    {
        completed = true;
    }
}
