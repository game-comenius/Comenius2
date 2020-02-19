using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public readonly static QuestClass[] _mainQuests =
    {
        new QuestClass (1, "Fale como o Diretor no começo do segundo dia.", new DoQuest(), new int[]{ }),
        new QuestClass (2, "Vá para a Sala dos Professores", new DoQuest(), new int[]{ }),
        new QuestClass (3, "Fale com o Vladmir.", new DoQuest(), new int[]{ -5 }),
        new QuestClass (4, "Faça o plano de aula na Sala dos Professores.", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Ir para o Pátio com o plano pronto.", new DoQuest(), new int[]{ 4 }),
        new QuestClass (6, "Ir para a sala de aula.", new DoQuest(), new int[]{ }),
        new QuestClass (7, "Falar com o Jean depois a aula", new DoQuest(), new int[]{ }),
        new QuestClass (8, "Falar com o Comenius depois a aula", new DoQuest(), new int[]{ })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (10100, "Fale com o Vladmir. (1)", new DoQuest(), new int[]{ 3, -5 }),
        new QuestClass (10200, "Fale com o Vladmir. (2)", new DoQuest(), new int[]{ 10100, -5 }),
        new QuestClass (10300, "Fale com o Vladmir. (3)", new DoQuest(), new int[]{ 10200, -5 }),
        new QuestClass (10400, "Falar com o 111 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10401, "Controle: Pegar fotografia com o garoto estranho.", new DoQuest(), new int[]{ 10400 }),
        new QuestClass (10500, "Falar com o 021 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10600, "Falar com o 031 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10700, "Falar com o Garoto Ambiente no Pátio 2.", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Falar com a Menina Ornitóloga no Pátio 2.", new DoQuest(), new int[]{ }),
        new QuestClass (10900, "Falar com a Menina Cadeirante no Pátio 2.", new DoQuest(), new int[]{ }),
        new QuestClass (11000, "Falar com a 061 na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (11100, "Falar com a Drica na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (11200, "Falar com a Montanari na Sala de Informática.", new DoQuest(), new int[]{ }),
        new QuestClass (11300, "Falar com a Antônia na Coordenação.", new DoQuest(), new int[]{ }),
        new QuestClass (11400, "Falar com o Garota da Literatura na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11500, "Falar com o 101 na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11600, "Falar com a Alice na Biblioteca.", new DoQuest(), new int[]{ }),

        new QuestClass (12000, "Pegar o retroprojetor na sala multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (12001, "Controle: Pegar o retroprojetor na sala multimeios.", new DoQuest(), new int[]{ 12000 })

    };
}
