using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3 : MonoBehaviour
{
    public readonly static QuestGroup[] _questGroups =
    {

    };

    public readonly static QuestClass[] _mainQuests =
    {
        new QuestClass (1, "Fale como o Diretor no começo do terceiro dia", new DoQuest(), new int[]{ }),
        new QuestClass (2, "Fale com o Paulino na sala dos professores", new DoQuest(), new int[]{ }),
        new QuestClass (3, "Faça um planejamento na sala dos professores", new DoQuest(), new int[]{ }),
        new QuestClass (4, "Vá para a aula", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Vá para o pátio", new DoQuest(), new int[]{ })
    };

    public readonly static QuestClass[] _sideQuests =
    {

    };
}
