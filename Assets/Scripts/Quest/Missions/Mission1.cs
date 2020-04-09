using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 
{
    //Index 0 ele sempre retorn false para completa e true para available.
    //Side quests deve ter Indexes superiores a 10.000
    //Colocar dependência com valor negativo indica que você não a quer feita.

    public readonly static QuestGroup[] _questGroups =
    {
        new QuestGroup("Converse com o pessoal", new int[] { 2, 3, 5 }),
        new QuestGroup("Vá para a aula", new int[] { 6, 9 }),
        new QuestGroup("Pegue mídias", new int[] { 300000 })
    };


    public readonly static QuestClass[] _mainQuests =
    {
        new QuestClass (1, "Fale como o Comenios no começo do jogo.", new DoQuest(), new int[]{ }),
        new QuestClass (2, "Fale com o Diretor no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (3, "Sala dos professores", new DoQuest(), new int[]{ 2 }, "Vá até a sala dos professores"),
        new QuestClass (4, "Fale com o Jean.", new DoQuest(), new int[]{ }),
        new QuestClass (5, "Conversar com a Madá", new DoQuest(), new int[]{ 3 }, "Converse com a aluna Madá no pátio"),
        new QuestClass (12, "Conversar com a Drica", new DoQuest(), new int[]{}, "Converse com a Drica na sala multimeios"),
        new QuestClass (13, "Explorar a biblioteca", new DoQuest(), new int[]{}, "Explore a biblioteca", "A biblioteca está entre a sala dos professores e a sala multimeios"),
        new QuestClass (6, "Ajudar com o Planejamento", new DoQuest(), new int[]{ 5, 13, 100600, 11200 }, "Explore os espaços da escola", "Ajude o professor Jean com o seu planejamento", "Acesse o planejamento através da prancheta sobre a mesa"),
        new QuestClass (7, "Ir para o Pátio com o plano pronto.", new DoQuest(), new int[]{ 6 }),
        new QuestClass (8, "Falar com a Madá no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (9, "Ir para a sala de aula.", new DoQuest(), new int[]{ 6 }),
        new QuestClass (10, "Falar com o Jean depois a aula", new DoQuest(), new int[]{ }),
        new QuestClass (11, "Ir para o encerramento do dia", new DoQuest(), new int[]{ 10 })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (10100, "Fale com o Jean. (1)", new DoQuest(), new int[]{ 4, -7 }),
        new QuestClass (10200, "Fale com o Jean. (2)", new DoQuest(), new int[]{ 10100, -7 }),
        new QuestClass (10300, "Fale com o Jean. (3)", new DoQuest(), new int[]{ 10200, -7 }),
        new QuestClass (10400, "Fale com o Jean. (4)", new DontQuest(), new int[]{ 10300, -7 }),
        new QuestClass (10410, "Fale com o Jean. (5)", new DoQuest(), new int[]{ }),
        new QuestClass (10411, "Confirme o planejamento com o Jean", new DoQuest(), new int[]{}),

        new QuestClass (10500, "Falar com o Menino Ambiente no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10510, "Responder corretamente o Menino Ambiente", new DoQuest(), new int[]{ }),
        new QuestClass (10511, "Controle: Responder corretamente o Menino Ambiente", new DoQuest(), new int[]{ 10510 }),

        new QuestClass (10600, "Falar com o 021 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Falar com o 031 no Pátio.", new DoQuest(), new int[]{ }),
        new QuestClass (10900, "Falar com o Pássaro no Pátio.", new DoQuest(), new int[]{ }),

        new QuestClass (11000, "Falar com o Literatura na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11010, "Responder corretamente o Literatura", new DoQuest(), new int[]{ }),
        new QuestClass (11011, "Controle: Responder corretamente o Literatura", new DoQuest(), new int[]{ 11010 }),

        new QuestClass (11100, "Falar com o 101 na Biblioteca.", new DoQuest(), new int[]{ }),
        new QuestClass (11200, "Visite a Coordenação", new DoQuest(), new int[]{ }, "Encontre a coordenadora Antônia na coordenação pedagógica", "Fale com a Antônia"),
        new QuestClass (11300, "Falar com a Drica (1) na Multimeios.", new DoQuest(), new int[]{ 100600 }),
        new QuestClass (11400, "Falar com a 061 na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (11500, "Falar Diretor no Pátio depois da aula.", new DoQuest(), new int[]{ }),
        new QuestClass (11600, "Falar Comenius no Pátio depois da aula. (1)", new DoQuest(), new int[]{ 11500 }),



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

        new QuestClass (100700, "Pegar Aparelho de Som na Sala Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (100701, "Controle: Pegar Aparelho de Som na Sala Multimeios.", new DoQuest(), new int[]{ 100700 }),

        new QuestClass (100800, "Pegar Mapa na Sala de Aula.", new DoQuest(), new int[]{ }),
        new QuestClass (100801, "Controle: Pegar Mapa na Sala de Aula.", new DoQuest(), new int[]{ 100800 }),

        new QuestClass (100900, "Gravar pássaro.", new DoQuest(), new int[]{ 100600 }),
        new QuestClass (100901, "Controle: Gravar pássaro.", new DoQuest(), new int[]{ 100900 }),

        new QuestClass (101000, "Fotografar pássaro.", new DoQuest(), new int[]{ 100500, -100600 }),
        new QuestClass (101001, "Controle: Fotografar pássaro.", new DoQuest(), new int[]{ 101000 }),

        new QuestClass (101010, "Fotografar pássaro.", new DoQuest(), new int[]{ 100500, 100900 }),
        new QuestClass (101011, "Controle: Fotografar pássaro.", new DoQuest(), new int[]{ 101010 }),

        new QuestClass (101100, "Pegar TV na multimeios", new DoQuest(), new int[]{ }),
        new QuestClass (101101, "Controle: Pegar TV na multimeios", new DoQuest(), new int[]{ 101100 }),



        new QuestClass (200000, "Interagir com a Estante na Multimeios.", new DoQuest(), new int[]{ }),
        new QuestClass (200001, "Terminar dialogo com a Estante na Multimeios.", new DoQuest(), new int[]{ }),

        new QuestClass (300000, "Encontre 3 mídias", new CounterQuest(0, 3), new int[]{ }, "Progresso:"),
        new QuestClass (300001, "Encontre mídias", new CounterQuest(0, int.MaxValue), new int[]{ })
    };
}
