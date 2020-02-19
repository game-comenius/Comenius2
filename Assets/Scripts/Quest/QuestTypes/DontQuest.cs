using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontQuest : IQuest
{
    public string GetQuestExibition(string questExibition)
    {
        return questExibition;
    }

    public bool IsComplete()
    {
        return false;
    }

    public void TakeStep()
    {
        //Do nothing
    }
}
