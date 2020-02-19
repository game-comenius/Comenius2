using System.Collections;
using System.Collections.Generic;

public interface IQuest
{
    void TakeStep();
    bool IsComplete();
    string GetQuestExibition(string questExibition);
}
