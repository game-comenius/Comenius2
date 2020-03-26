using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public readonly static QuestGroup[] _questGroups =
    {
    };

    public readonly static QuestClass[] _mainQuests =
    {
        new QuestClass (1, "Fale como o Diretor no começo do segundo dia", new DoQuest(), new int[]{ }),
        new QuestClass (3, "Fale com o Vladmir", new DoQuest(), new int[]{ }),
        new QuestClass (4, "Faça o plano de aula na Sala dos Professores", new DoQuest(), new int[]{ 3 }),
        new QuestClass (5, "Ir para a sala de aula", new DoQuest(), new int[]{ 4 }),
        new QuestClass (51, "Controle: Ir para a sala de aula", new DoQuest(), new int[]{ }),
        new QuestClass (6, "Falar com o Vladmir depois da aula", new DoQuest(), new int[]{ }),
        new QuestClass (7, "Ir para o encerramento do dia", new DoQuest(), new int[]{ 6 })
    };

    public readonly static QuestClass[] _sideQuests =
    {
        new QuestClass (10100, "Fale com o Vladmir (1)", new DoQuest(), new int[]{ 3 }),
        new QuestClass (10200, "Fale com o Vladmir (2)", new DoQuest(), new int[]{ 10100 }),
        new QuestClass (10300, "Fale com o Vladmir (3)", new DoQuest(), new int[]{ 10200 }),
        new QuestClass (10400, "Falar com o garoto esquisito no pátio sobre a fotografia", new DoQuest(), new int[]{ }),
        new QuestClass (10401, "Controle: Pegar fotografia", new DoQuest(), new int[]{ 10400 }),
        new QuestClass (10600, "Falar com o 031 no Pátio", new DoQuest(), new int[]{ }),

        new QuestClass (10700, "Falar com o Garoto Ambiente no Pátio 2", new DoQuest(), new int[]{ }),    
        new QuestClass (10710, "Responder corretamente o Garoto Ambiente", new DoQuest(), new int[]{ }),
        new QuestClass (10711, "Controle: Responder corretamente o Garoto Ambiente", new DoQuest(), new int[]{ 10710 }),

        new QuestClass (10900, "Falar com a Menina Cadeirante no Pátio 2", new DoQuest(), new int[]{ }),
        new QuestClass (10901, "Controle: Falar com a Menina Cadeirante", new DoQuest(), new int[]{ 10900 }),

        new QuestClass (11000, "Falar com a 061 na Multimeios", new DoQuest(), new int[]{ }),

        new QuestClass (11100, "Falar com a Drica na Multimeios", new DoQuest(), new int[]{ }),
        new QuestClass (11110, "Falar com a Drica sobre as canetas", new DoQuest(), new int[]{ }),
        new QuestClass (10800, "Encontrar aluna com canetas coloridas", new DoQuest(), new int[]{ 11110 }),
        new QuestClass (10801, "Controle: pegar canetar no Pátio 2", new DoQuest(), new int[]{ 10800 }),

        new QuestClass (11200, "Falar com a Montanari na Sala de Informática", new DoQuest(), new int[]{ }),
        new QuestClass (11300, "Falar com a Antônia na Coordenação", new DoQuest(), new int[]{ }),
        new QuestClass (11400, "Falar com o Literatura na Biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (11401, "Controle: Pegar o Diário com o Literatura na Biblioteca", new DoQuest(), new int[]{ 11400 }),
        new QuestClass (11500, "Falar com o 101 na Biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (11600, "Falar com a Alice na Biblioteca", new DoQuest(), new int[]{ }),

        new QuestClass (11700, "Pegar fita VHS na biblioteca", new DoQuest(), new int[]{ }),
        new QuestClass (11701, "Controle: Pegar fita VHS na biblioteca", new DoQuest(), new int[]{ 11700 }),

        new QuestClass (12000, "Pegar o retroprojetor na sala multimeios", new DoQuest(), new int[]{ }),
        new QuestClass (12001, "Controle: Pegar o retroprojetor na sala multimeios", new DoQuest(), new int[]{ 12000 }),

        //depois de falar com a montanari, o jogador pode interagir com as páginas espalhadas pelo mapa,
        //pegar uma página é o requisito para fazer o slide com o conteúdo na montanari
        //ao fazer cada slide, aumenta o contador até ter feito todos, nesse ponto troca o diálogo da montanari
        //explicando que acabaram os slides em branco ou algo assim

        new QuestClass (12100, "Pegar página do livro com mapa", new DoQuest(), new int[]{ 11200 }),
        new QuestClass (12101, "Fazer slide com mapa", new DoQuest(), new int[]{ 12100 }),
        new QuestClass (12102, "Controle: Fazer slide com mapa", new DoQuest(), new int[]{ 12101 }),

        new QuestClass (12200, "Pegar página do livro com linha do tempo", new DoQuest(), new int[]{ 11200 }),
        new QuestClass (12201, "Fazer slide com linha do tempo", new DoQuest(), new int[]{ 12200 }),
        new QuestClass (12202, "Controle: Fazer slide com linha do tempo", new DoQuest(), new int[]{ 12201 }),

        new QuestClass (12300, "Pegar página do livro com ciclo do trabalho", new DoQuest(), new int[]{ 11200 }),
        new QuestClass (12301, "Fazer slide com ciclo do trabalho", new DoQuest(), new int[]{ 12300 }),
        new QuestClass (12302, "Controle: Fazer slide com ciclo do trabalho", new DoQuest(), new int[]{ 12301 }),

        new QuestClass (12401, "Editar o VHS", new DoQuest(), new int[]{ 11700 }),
        new QuestClass (12402, "Controle: Fazer VHS editado", new DoQuest(), new int[]{ 12401 }),

        new QuestClass (13000, "Fazer todas as mídias editadas", new CounterQuest(0, 4), new int[]{  })
    };
}
