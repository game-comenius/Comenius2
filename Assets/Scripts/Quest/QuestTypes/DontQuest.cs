using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontQuest : IQuest
{
    public bool IsComplete()
    {
        return false;
    }

    public void TakeStep()
    {
        //Do nothing
    }
}
