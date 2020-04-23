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
        new QuestClass (3, "Fale com estudantes nos pátio", new CounterQuest(0, 6), new int[]{ }),
        new QuestClass (4, "Fale com o Paulino sobre as opniões dos alunos", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Faça um planejamento na sala dos professores", new DoQuest(), new int[]{ 4 }),
        new QuestClass (6, "Confirme o planejamento com o professor", new DoQuest(), new int[]{ }),
        new QuestClass (7, "Vá para aula", new DoQuest(), new int[]{ 6 }),
        new QuestClass (8, "Vá para o pátio", new DoQuest(), new int[]{ })
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
        new QuestClass (30901, "Controle: Falar com Ornitóloga no sandbox 1", new DoQuest(), new int[]{ 30900 }),

        new QuestClass (31100, "Falar com o Garoto Estranho no sandbox 2", new DoQuest(), new int[]{ }),
        new QuestClass (31101, "Controle: Falar com o Garoto Estranho", new DoQuest(), new int[]{ 31100 }),

        new QuestClass (31200, "Falar com Meio Ambiente no sandbox 2", new DoQuest(), new int[]{ }),
        new QuestClass (31201, "Controle: Falar com o Meio Ambiente", new DoQuest(), new int[]{ 31200 }),

        new QuestClass (32100, "Pegar CD de regionalismos na biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (32101, "Controle: Pegar CD de regionalismos na biblioteca", new DoQuest(), new int[]{ 32100 }),

        new QuestClass (32200, "Pegar VHS de regionalismos na biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (32201, "Controle: Pegar VHS de regionalismos", new DoQuest(), new int[]{ 32200 }),

        new QuestClass (32300, "Pegar receita de jogo da forca com o Vladmir", new DoQuest(), new int[]{ }),
        new QuestClass (32301, "Controle: Pegar jogo da forca com o Vladmir", new DoQuest(), new int[]{ 32300 }),

        new QuestClass (32400, "Pegar enciclopedia na biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (32401, "Controle: Pegar enciclopedia na biblioteca", new DoQuest(), new int[]{ 32400 }),

        new QuestClass (32500, "Pegar papel sulfite na coordenação", new DoQuest(), new int[]{ }),
        new QuestClass (32501, "Controle: Pegar papel sulfite na coordenação", new DoQuest(), new int[]{ 32500 }),

        new QuestClass (32600, "Pegar receita de adedonha com a Drica", new DoQuest(), new int[]{ }),
        new QuestClass (32601, "Controle: Pegar receita de adedonha com a Drica", new DoQuest(), new int[]{ 32600 }),

        new QuestClass (32700, "Pegar receita de palavras cruzadas com Leitura", new DoQuest(), new int[]{ }),
        new QuestClass (32701, "Controle: Pegar receita de palavras cruzadas com Leitura", new DoQuest(), new int[]{ 32700 }),

        new QuestClass (33100, "Fazer VHS editado", new DoQuest(), new int[]{ 32200 }),
        new QuestClass (33101, "Controle: Fazer VHS editado", new DoQuest(), new int[]{ 33100 }),

        new QuestClass (33200, "Fazer jogo Adenonha", new DoQuest(), new int[]{ 32600 }),
        new QuestClass (33201, "Controle: Fazer jogo Adenonha", new DoQuest(), new int[]{ 33200 }),

        new QuestClass (33300, "Fazer jogo Palavras Cruzadas", new DoQuest(), new int[]{ 32700 }),
        new QuestClass (33301, "Controle: Fazer jogo Palavras Cruzadas", new DoQuest(), new int[]{ 33300 }),

        new QuestClass (33400, "Fazer jogo Forca", new DoQuest(), new int[]{ 32300 }),
        new QuestClass (33401, "Controle: Fazer jogo Forca", new DoQuest(), new int[]{ 33400 })

    };
}
