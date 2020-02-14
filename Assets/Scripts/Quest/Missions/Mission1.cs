using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 
{
    public readonly static QuestClass[] _mainQuests = //Index 0 ele sempre retorn false para completa e true para available.
    {
        new QuestClass (1, "Fale como o Comenios no começo do jogo.", new DoQuest(), new int[]{ }),

    };

    public readonly static QuestClass[] _sideQuests = //Index 0 ele sempre retorn false para completa e true para available.
    {

    };
}
