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
        new QuestClass (9, "Ir para a sala de aula.", new DoQuest(), new int[]{ }),
        new QuestClass (10, "Falar com o Jean depois a aula", new DoQuest(), new int[]{ })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (10100, "Fale com o Jean. (1)", new DoQuest(), new int[]{ 4, -7 }),
        new QuestClass (10200, "Fale com o Jean. (2)", new DoQuest(), new int[]{ 10100, -7 }),
        new QuestClass (10300, "Fale com o Jean. (3)", new DoQuest(), new int[]{ 10200, -7 }),
        new QuestClass (10500, "Falar com o Menino Ambiente no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10600, "Falar com o 021 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Falar com o 031 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10900, "Falar com o Pássaro no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (11000, "Falar com o Literatura na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11100, "Falar com o 101 na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11200, "Falar com a Antônia na Coordenação.", new DoQuest(), new int[]{ }),
        new QuestClass (11300, "Falar com a Drica (1) na Multimeios.", new DoQuest(), new int[]{ 100600 }),
        new QuestClass (11400, "Falar com a 061 na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (11500, "Falar Diretor no Pátio depois da aula.", new DoQuest(), new int[]{ }),
        new QuestClass (11600, "Falar Comenius no Pátio depois da aula.. (1)", new DoQuest(), new int[]{ 11500 }),



        new QuestClass (100000, "Pegar Mapa no armário da sala dos professores.", new DoQuest(), new int[]{ }),
        new QuestClass (100001, "Controle: Pegar Mapa no armário da sala dos professores.", new DoQuest(), new int[]{ 100000 }),

        new QuestClass (100100, "Pegar Coleção de Penas com o 111 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (100101, "Controle: Pegar Coleção de Penas com o 111 no Pátio.", new DoQuest(), new int[]{ 100100 }),

        new QuestClass (100200, "Pegar VHS com a Alice na Biblioteca.", new DoQuest(), new int[]{  }),
        new QuestClass (100201, "Controle: Pegar VHS com a Alice na Biblioteca.", new DoQuest(), new int[]{ 100200 }),

        new QuestClass (100300, "Pegar Livro Ilustrado na Estante1 na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (100301, "Controle: Pegar Livro Ilustrado na Estante1 na Biblioteca.", new DoQuest(), new int[]{ 100300 }),

        new QuestClass (100400, "Pegar CD na Estante3 na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (100401, "Controle: Pegar CD na Estante3 na Biblioteca.", new DoQuest(), new int[]{ 100400 }),

        new QuestClass (100500, "Pegar Câmera na Coordenação.", new DoQuest(), new int[]{ }),
        new QuestClass (100501, "Controle: Pegar Câmera na Coordenação.", new DoQuest(), new int[]{ 100500 }),

        new QuestClass (100600, "Pegar Gravador com a Drica na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (100601, "Controle: Pegar Gravador com a Drica na Multimeios.", new DoQuest(), new int[]{ 100600 }),

        new QuestClass (100700, "Pegar Aparelho de Som na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (100701, "Controle: Pegar Aparelho de Som na Multimeios.", new DoQuest(), new int[]{ 100700 }),




        new QuestClass (200000, "Interagir com a Estante na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (200001, "Terminar dialogo com a Estante na Multimeios.", new DoQuest(), new int[]{ })
    };
}
