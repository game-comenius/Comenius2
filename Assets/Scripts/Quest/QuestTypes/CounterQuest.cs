using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CounterQuest : IQuest
{
    private int _counter;

    private int _goal;

    public CounterQuest(int counterStart, int counterGoal)
    {
        _counter = counterStart;
        _goal = counterGoal;
    }

    public string ProgressFraction()
    {
        return _counter + "/" + _goal;        
    }

    public bool IsComplete()
    {
        if (_counter < _goal)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TakeStep()
    {
        _counter++;
    }
}
