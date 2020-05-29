using System;
using System.Collections.Generic;

// Esta classe usa o padrão "Enumeration Class", que faz com que ela pareça um
// enum, ou seja, podemos acessar instâncias predefinidas (static) como se nós
// estivéssemos usando uma classe enum, exemplo: EstaClasse.A retornaria uma
// instância static de EstaClasse chamada A com as propriedades já definidas
// É permitido criar objetos desta classe em outros lugares, porém ela não foi
// feita para isso, a ideia é que a gente use apenas as instâncias static
public class FeedbacksDosAlunos
{
    // Os feedbacks dos alunos para as mídias estão atualmente nos arquivos:
    // Feedbacks Na Missao 1: https://docs.google.com/document/d/1mmOs3DkDUZR0QVO61Mg1SnRdjvtSwFaQrc47JEeZnwo/edit?usp=sharing
    // Feedbacks Na Missao 2: https://docs.google.com/document/d/17J1gz2s3VF2eecfPqUUaLZAeIEoehiMMvFcO3mphZM8/edit#heading=h.m7omlheb45wz
    // Feedbacks Na Missao 3: https://docs.google.com/document/d/1sBATZFnn3HRRPzCsQM1yexuv32QSVNdMunTIKWkzNVo/edit#heading=h.i3e3vodwzm16

    // Instâncias static || Membros desta "Enumeration Class"
    public static readonly FeedbacksDosAlunos FeedbacksNaMissao1 = new FeedbacksDosAlunos
    (
        feedbacksPorMidiaNoMomento1: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Gravador, "Achei legal poder gravar tudo que a gente pesquisou sobre os pássaros no primeiro momento." }
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas
            { ItemName.Gravador, "Gostei das coisas que gravei na aula. É divertido aprender assim." }

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
            { ItemName.Gravador, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." }
        },

        feedbacksAulaMelhor: new[]
        {
            "Nossa! Que aula incrível! Não vejo a hora de contar para os meus pais tudo o que aprendi.",
            "Essa foi a melhor aula do ano! Na próxima será que vamos aprender mais sobre os pássaros?"
        },
        feedbacksAulaMuitoBoa: new[]
        {
            "Gostei da aula! Agora já sei mais sobre os pássaros...",
            "Foi divertida a aula! Não achei que ia gostar muito, mas acabei me surpreendendo."
        },
        feedbacksAulaBoa: new[]
        {
            "Foi legal a aula hoje. Mas algumas coisas ficaram confusas para mim.",
            "A aula foi boa, mas fiquei com sono em alguns momentos..."
        },
        feedbacksAulaFraca: new[]
        {
            "Não gostei da aula, fiquei confuso o tempo todo.",
            "Que aula chata, até dormi em alguns momentos."
        }
    );

    public static readonly FeedbacksDosAlunos FeedbacksNaMissao2 = new FeedbacksDosAlunos
    (
        feedbacksPorMidiaNoMomento1: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Gravador, "Achei legal poder gravar tudo que a gente pesquisou sobre os pássaros no primeiro momento." }
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas
            { ItemName.Gravador, "Gostei das coisas que gravei na aula. É divertido aprender assim." }

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
            { ItemName.Gravador, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." }
        },

        feedbacksAulaMelhor: new[] { "a2", "b2" },
        feedbacksAulaMuitoBoa: new[] { "a", "b" },
        feedbacksAulaBoa: new[] { "a", "b" },
        feedbacksAulaFraca: new[] { "a", "b" }
    );

    public static readonly FeedbacksDosAlunos FeedbacksNaMissao3 = new FeedbacksDosAlunos
    (
        feedbacksPorMidiaNoMomento1: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Gravador, "Achei legal poder gravar tudo que a gente pesquisou sobre os pássaros no primeiro momento." }
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas
            { ItemName.Gravador, "Gostei das coisas que gravei na aula. É divertido aprender assim." }

            // Mídias Boas

            // Mídias Fracas
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            // Mídias Muito Boas

            // Mídias Boas

            // Mídias Fracas
            { ItemName.Gravador, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." }
        },

        feedbacksAulaMelhor: new[] { "a3", "b3" },
        feedbacksAulaMuitoBoa: new[] { "a", "b" },
        feedbacksAulaBoa: new[] { "a", "b" },
        feedbacksAulaFraca: new[] { "a", "b" }
    );


    // 1 dicionário de mídias e seus feedbacks para cada um dos momentos da aula
    // Se você quiser, por exemplo, o feedback para a mídia "Retroprojetor"
    // quando usada no terceiro momento da missão 2, é só escrever:
    // FeedbacksDosAlunos.FeedbacksNaMissao1.FeedbacksPorMidiaNoMomento3[ItemName.Retroprojetor];
    public Dictionary<ItemName, string> FeedbacksPorMidiaNoMomento1;
    public Dictionary<ItemName, string> FeedbacksPorMidiaNoMomento2;
    public Dictionary<ItemName, string> FeedbacksPorMidiaNoMomento3;

    // Textos que os alunos podem falar de acordo com a pontuação geral da aula
    // Por exemplo, se a aula foi fraca, o aluno pode falar qualquer um dos
    // textos presentes no array de strings 'FeedbacksAulaFraca'
    public string[] FeedbacksAulaMelhor;
    public string[] FeedbacksAulaMuitoBoa;
    public string[] FeedbacksAulaBoa;
    public string[] FeedbacksAulaFraca;


    public string[] ObterFeedbacksGeraisDaAula(int pontuacaoDaAula)
    {
        if (pontuacaoDaAula >= 26 && pontuacaoDaAula <= 30)
            return FeedbacksAulaMelhor;
        else if (pontuacaoDaAula >= 21 && pontuacaoDaAula <= 25)
            return FeedbacksAulaMuitoBoa;
        else if (pontuacaoDaAula >= 14 && pontuacaoDaAula <= 20)
            return FeedbacksAulaBoa;
        else
            return FeedbacksAulaFraca;
    }


    public FeedbacksDosAlunos(
        Dictionary<ItemName, string> feedbacksPorMidiaNoMomento1,
        Dictionary<ItemName, string> feedbacksPorMidiaNoMomento2,
        Dictionary<ItemName, string> feedbacksPorMidiaNoMomento3,
        string[] feedbacksAulaMelhor, string[] feedbacksAulaMuitoBoa,
        string[] feedbacksAulaBoa, string[] feedbacksAulaFraca)
    {
        FeedbacksPorMidiaNoMomento1 = feedbacksPorMidiaNoMomento1;
        FeedbacksPorMidiaNoMomento2 = feedbacksPorMidiaNoMomento2;
        FeedbacksPorMidiaNoMomento3 = feedbacksPorMidiaNoMomento3;

        // Dar os feedbacks padrões para todas as outras mídias que não estão
        // nos dicionários para evitar nulls, exceções e strings vazias
        Dictionary<ItemName, string>[] feedbacksPorMidiaArray = { FeedbacksPorMidiaNoMomento1, FeedbacksPorMidiaNoMomento2, FeedbacksPorMidiaNoMomento3 };
        for (int i = 0; i < feedbacksPorMidiaArray.Length; i++)
        {
            Dictionary<ItemName, string> feedbacksPorMidia = feedbacksPorMidiaArray[i];
            foreach (ItemName midia in Enum.GetValues(typeof(ItemName)))
            {
                if (!feedbacksPorMidia.ContainsKey(midia))
                {
                    string momento;
                    switch (i)
                    {
                        default: momento = "primeiro"; break;
                        case 1: momento = "segundo"; break;
                        case 2: momento = "terceiro"; break;
                    }
                    feedbacksPorMidia[midia] = "Gostei bastante da mídia que o professor escolheu para o " + momento + " momento da aula!";
                }
            }
        }

        FeedbacksAulaMelhor = feedbacksAulaMelhor;
        FeedbacksAulaMuitoBoa = feedbacksAulaMuitoBoa;
        FeedbacksAulaBoa = feedbacksAulaBoa;
        FeedbacksAulaFraca = feedbacksAulaFraca;
    }
}