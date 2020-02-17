using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 
{
    //Index 0 ele sempre retorn false para completa e true para available.
    //Side quests deve ter Indexes superiores a 10.000
    //Colocar dependência com valor negativo indica que você não a quer feita.

    public readonly static QuestClass[] _mainQuests =
    {
        new QuestClass (1, "Fale como o Comenios no começo do jogo.", new DoQuest(), new int[]{ }),
        new QuestClass (2, "Fale com o Diretor no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (3, "Vá para a Sala dos Professores", new DoQuest(), new int[]{ 2 }),
        new QuestClass (4, "Fale com o Jean.", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Fale com a Madá no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (6, "Faça o plano de aula na Sala dos Professores.", new DoQuest(), new int[]{ }),
        new QuestClass (7, "Ir para o Pátio com o plano pronto.", new DoQuest(), new int[]{ 6 }),
        new QuestClass (8, "Falar com a Madá no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (9, "Ir para a sala de aula.", new DoQuest(), new int[]{ })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (10100, "Fale com o Jean. (1)", new DoQuest(), new int[]{ 4, -7 }),
        new QuestClass (10200, "Fale com o Jean. (2)", new DoQuest(), new int[]{ 10100, -7 }),
        new QuestClass (10300, "Fale com o Jean. (3)", new DoQuest(), new int[]{ 10200, -7 }),
        new QuestClass (10400, "Pegar Mapa no armário da sala dos professores.", new DoQuest(), new int[]{ }),
        new QuestClass (10500, "Falar com o Menino Ambiente no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10600, "Falar com o 021 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10700, "Falar com o 111 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Falar com o 031 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Falar com o Pássaro no Pátio.", new DoQuest(), new int[]{ })
    };
}
