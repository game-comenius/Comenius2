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
            { ItemName.Gravador, "Achei legal poder gravar tudo que a gente pesquisou sobre os pássaros no primeiro momento." },
            { ItemName.CameraPolaroid, "Achei bem legal ver a foto do pássaro para a nossa pesquisa no primeiro momento." },
            { ItemName.LivroIlustrado, "Que bacana o livro ilustrado que a gente usou! Quanta gravura linda, consegui entender muito bem como são os pássaros." },
            // Mídias Muito Boas
            { ItemName.TVComVHSPassaros, "Caramba, que videozinho massa! Consegui entender bem melhor o conteúdo com as imagens e o som que foi mostrado!" },
            { ItemName.GravacaoPassaro, "Achei bem legal escutar o som do passarinho da gravação. Mas isso não me ajudou a entender a diferença entre os tipos de pássaros." },
            { ItemName.LivroDidatico, "Achei legal o que deu para aprender lendo no livro didático. Ele até que é bem completinho." },
            { ItemName.CartazComColecaoDePenas, "Essas penas daquela coleção são tão coloridas... Ver todas colocada juntinhas assim dessa forma deixou mais fácil de imaginar os diferentes tipos de pássaros." },
            { ItemName.JornaisERevistas, "As matérias e as fotos de pássaros nos jornais e revistas que o professor trouxe contaram muito sobre a vida deles. Adoro esse tipo de texto!" },
            { ItemName.FotografiaPassaro, "A foto é bem bonita! Mas isso não me ajudou a entender a diferença entre os tipos de pássaros." },
            { ItemName.ReprodutorAudio, "Achei muito útil o reprodutor de áudio para a nossa pesquisa." },
            { ItemName.ReprodutorAudioComCDPassaros, "Achei muito útil o reprodutor de áudio com CD para a nossa pesquisa." },
            // Mídias Boas
            { ItemName.Cartazes, "Vou poder lembrar todos os dias do que eu aprendi na aula hoje com esse cartaz pendurado na parede. Que massa!" },
            { ItemName.Mapa, "Adoro mapas, eles são tão grandes e bonitos. Mas me pareceu meio inadequado para esse momento da aula." },
            { ItemName.Jornais, "Até que naquele jornal tinha matérias e imagens interessantes, mas parece que estava faltando alguma coisa." },
            // Mídias Fracas
            { ItemName.QuadroNegro, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.Caderno, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.TVComVHS, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.Cd, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.VHS, "O primeiro momento da aula foi confuso… não gostei." }
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Cartazes, "Fazer cartazes é tão divertido. Parece que eu aprendo melhor assim." },
            { ItemName.Caderno, "Essa atividade me inspirou para deixar o meu caderno super colorido e bonitão. Ótima ideia!" },
            { ItemName.CartazComColecaoDePenas, "As penas da coleção eram bonitas, mas eu não consegui muito tempo com elas para fazer a atividade..." },
            { ItemName.LivroIlustrado, "Adorei a atividade, na pesquisa que fiz no livro ilustrado eu fiquei ainda mais curioso sobre os pássaros." },
            { ItemName.ReprodutorAudio, "Achei muito útil o reprodutor de áudio para a nossa atividade." },
            // Mídias Muito Boas
            { ItemName.LivroDidatico, "Adorei a atividade, na pesquisa que fiz no livro didático eu fiquei ainda mais curioso sobre os pássaros." },
            { ItemName.Gravador, "Gostei das coisas que gravei na aula. É divertido aprender assim." },
            // Mídias Boas
            { ItemName.TVComVHS, "A atividade de registrar as coisas enquanto passava o vídeo foi legal, mas não deu pra prestar muita atenção nas imagens assim." },
            { ItemName.ReprodutorAudioComCDPassaros, "A atividade de registrar as coisas enquanto tocava o CD foi legal, mas não deu pra prestar muita atenção nos sons assim." },
            { ItemName.Mapa, "Mexer com mapa é sempre legal, mas se eu pudesse ter uma ilustração de um passarinho aqui seria mais legal ainda." },
            { ItemName.JornaisERevistas, "Tinha muita imagem legal para recortar nos jornais e revistas." },
            { ItemName.QuadroNegro, "Eu fico meio nervoso de apresentar algo lá na frente, mas usar a superfície do quadro negro para aprender é bem divertido." },
            { ItemName.Jornais, "Recortar coisas de jornais é sempre bacana, mas faltou material e no final eram poucas imagens." },
            { ItemName.GravacaoPassaro, "A gravação do pássaro foi uma boa escolha!" }, // Essa descrição foi o Bruno que inventou pois não tinha nada escrito no documento, revisar!
            // Mídias Fracas
            { ItemName.CameraPolaroid, "O segundo momento da aula foi chato… Aquela atividade não fazia sentido." },
            { ItemName.TVComVHSPassaros, "O segundo momento da aula foi chato… Aquela atividade não fazia sentido." },
            { ItemName.FotografiaPassaro, "O segundo momento da aula foi chato… Aquela atividade não fazia sentido." },
            { ItemName.Cd, "O segundo momento da aula foi chato… Aquela atividade não fazia sentido." },
            { ItemName.VHS, "O segundo momento da aula foi chato… Aquela atividade não fazia sentido." }
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Mapa, "Não sabia que tinham tantos pássaros regionais e que ficavam onde ficam. Entendi tudo melhor com o mapa." },
            { ItemName.CartazComColecaoDePenas, "A coleção de penas era bacana, ver tantas penas durante a aula foi bem interessante." },
            // Mídias Muito Boas
            { ItemName.LivroIlustrado, "Aquele livro ilustrado era muito completo! Deu para ver as diferenças entre os pássaros de cada região certinho." },
            { ItemName.LivroDidatico, "A explicação do nosso livro didático é bem completa, deu pra entender sobre quais são os pássaros naturais da nossa região." },
            { ItemName.FotografiaPassaro, "Aquela foto do passarinho era super maneira, eu sempre vejo ele voando aqui pelo pátio!" },
            { ItemName.GravacaoPassaro, "Sério que aquela gravação era de um pássaro aqui da escola mesmo? Quem diria que a gente pode aprender com as coisas que tem no pátio. Uau!" },
            { ItemName.QuadroNegro, "Vendo a tabela que o professor fez no quadro negro ficou mais fácil de entender a matéria da aula." },
            { ItemName.Cartazes, "Vendo a tabela que o professor fez para a gente no cartaz ficou mais fácil de entender a matéria da aula." },
            // Mídias Boas
            { ItemName.TVComVHS, "O vídeo era bem bacana, mas tinha muitos pássaros que não eram dessa região, o que acabou deixando essa parte da aula confusa." },
            { ItemName.ReprodutorAudioComCDPassaros, "O CD era bem bacana, mas tinha muitos pássaros que não eram dessa região, o que acabou deixando essa parte da aula confusa." },
            { ItemName.JornaisERevistas, "As matérias nos jornais e revistas eram bacanas, mas tinha muitos pássaros que não eram dessa região, o que acabou deixando essa parte da aula confusa." },
            // Mídias Fracas
            { ItemName.Gravador, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.Caderno, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.ReprodutorAudio, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.Jornais, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.CameraPolaroid, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.TVComVHSPassaros, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.Cd, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." },
            { ItemName.VHS, "A terceira parte da aula foi estranha, não sei como aquilo me ajuda a entender sobre os pássaros da minha região..." }
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