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
        new QuestClass (3, "Fale com estudantes nos pátio", new CounterQuest(0, 5), new int[]{ }),
        new QuestClass (4, "Fale com o Paulino sobre as opniões dos alunos", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Faça um planejamento na sala dos professores", new DoQuest(), new int[]{ }),
        new QuestClass (6, "Vá para a aula", new DoQuest(), new int[]{ }),
        new QuestClass (7, "Vá para o pátio", new DoQuest(), new int[]{ })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (30100, "Falar com Cadeirante no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30101, "Controle: Falar com Cadeirante no sandbox 1", new DoQuest(), new int[]{ 30100 }),

        new QuestClass (30200, "Falar com Estranho no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30201, "Controle: Falar com Estranho no sandbox 1", new DoQuest(), new int[]{ 30200 }),

        new QuestClass (30300, "Falar com Genérico 1 no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30301, "Controle: Falar com Genérico 1 no sandbox 1", new DoQuest(), new int[]{ 30300 }),

        new QuestClass (30400, "Falar com Literatura no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30401, "Controle: Falar com Literatura no sandbox 1", new DoQuest(), new int[]{ 30400 }),

        new QuestClass (30500, "Falar com Meio Ambiente no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30501, "Controle: Falar com Meio Ambiente no sandbox 1", new DoQuest(), new int[]{ 30500 }),

        new QuestClass (30600, "Falar com Genérico 2 no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30601, "Controle: Falar com Genérico 2 no sandbox 1", new DoQuest(), new int[]{ 30600 }),

        new QuestClass (30700, "Falar com Drica no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30701, "Controle: Falar com Drica no sandbox 1", new DoQuest(), new int[]{ 30700 }),

        new QuestClass (30800, "Falar com Madá no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30801, "Controle: Falar com Madá no sandbox 1", new DoQuest(), new int[]{ 30800 }),

        new QuestClass (30900, "Falar com Ornitóloga no sandbox 1", new DoQuest(), new int[]{ }),
        new QuestClass (30901, "Controle: Falar com Ornitóloga no sandbox 1", new DoQuest(), new int[]{ 30900 })

    };
}
