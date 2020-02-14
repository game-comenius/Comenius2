using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependentQuest : IQuest
{
    public bool IsComplete()
    {
        return true;
    }

    public void TakeStep()
    {
        Debug.Log(this.GetType().Name + " does not take steps.");
    }
}
