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
            { ItemName.TVComVHSRevolucaoIndustrialEditado, "Eita! Que legal foi ver aquele vídeo, falou tudo sobre a revolução industrial e ainda pulou todas as partes chatas. Quero mais vídeos como esse nas outras aulas." },
            { ItemName.RetroprojetorSlideMapa, "Com aquele mapa todo colorido e projetado na parede, explicando direitinho a revolução industrial, ficou muito mais fácil de aprender." },
            { ItemName.RetroprojetorSlideLinhaTempo, "Com aquela linha do tempo toda colorida e projetada na parede, explicando direitinho a revolução industrial, ficou muito mais fácil de aprender." },
            // Mídias Muito Boas
            { ItemName.ReprodutorAudioComCDRevolucaoIndustrial, "Que entrevista mais interessante! Gostei!" },
            { ItemName.RetroprojetorSlideCicloTrabalho, "Era uma transparência legal esse do ciclo do trabalho infantil, mas parecia que foi muito pouco para ajudar a explicar sobre todo conteúdo de revolução industrial." },
            { ItemName.TVComVHS, "O Vídeo era bem bacana professora, acho que deu pra aprender legal sobre a revolução industrial." },
            // Mídias Boas
            { ItemName.ReprodutorAudio, "O reprodutor de áudio foi bem interessante durante a exposição." },
            { ItemName.Retroprojetor, "O retroprojetor foi bem interessante durante a exposição." },
            { ItemName.QuadroNegro, "O quadro negro foi bem interessante durante a exposição, pena que já estamos muito acostumados aos professores quase sempre usarem ele." },
            { ItemName.JornaisERevistas, "Eram jornais e revistas bem interessantes, mas pareciam que foram muito pouco para ajudar a explicar sobre todo conteúdo de revolução industrial." },
            { ItemName.Jornais, "Eram jornais bem interessantes, mas pareciam que foram muito pouco para ajudar a explicar sobre todo conteúdo de revolução industrial." },
            { ItemName.FotografiaRevolucaoIndustrial, "Era uma foto bem interessante, mas parecia que foi muito pouco para ajudar a explicar sobre todo conteúdo de revolução industrial." },
            // Mídias Fracas
            { ItemName.Gravador, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.CameraPolaroid, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.VHS, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.LivroDidatico, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.LivroIlustrado, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.Caderno, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.Cartazes, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.Diario, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.CartazComCanetas, "O primeiro momento da aula foi confuso… não gostei." },
            { ItemName.TVComVHSRevolucaoIndustrial, "O primeiro momento da aula foi confuso… não gostei." },
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.FotografiaRevolucaoIndustrial, "Ver aquela foto de um menino da minha idade naquelas condições me fez sentir na pele o que ele deve ter passado, professora. Isso ajudou muito na hora da discussão, parecia que tava todo mundo interessado no assunto." },
            { ItemName.Diario, "Ter aquele diário sobre aquele menino que trabalhou na revolução industrial foi excelente pra entender o conteúdo e poder discutir sobre esse assunto." },
            { ItemName.RetroprojetorSlideCicloTrabalho, "Ter aquela transparência sobre o ciclo do trabalho infantil foi muito bom pra entender o conteúdo e poder discutir melhor sobre esse assunto." },
            { ItemName.ReprodutorAudio, "O reprodutor de áudio foi bem interessante durante o segundo momento da aula." },
            { ItemName.Gravador, "Poder gravar toda essa discussão que a gente fez foi muito útil na hora da atividade." },
            // Mídias Muito Boas
            { ItemName.ReprodutorAudioComCDRevolucaoIndustrial, "A entrevista que foi passada no Reprodutor de Áudio era legal, ajudou a dar temas na hora da discussão." },
            { ItemName.LivroDidatico, "Ler e discutir é uma coisa que ajuda muito mais a entender o conteúdo! Eu consegui visualizar melhor como era o trabalho infantil com a discussão." },
            { ItemName.LivroIlustrado, "As imagens do Livro Ilustrado ajudavam a gente a entender o contexto da revolução industrial. Foi bacana." },
            { ItemName.RetroprojetorSlideMapa, "Usar o mapa projetado para discutir foi bem interessante. Nunca imaginei que fosse possível fazer uma discussão a partir de um mapa!" },
            { ItemName.RetroprojetorSlideLinhaTempo, "Usar a linha do tempo projetada para discutir foi bem interessante. Nunca imaginei que fosse possível fazer uma discussão a partir de uma linha do tempo!" },
            { ItemName.TVComVHSRevolucaoIndustrialEditado, "Discutir o que acontecia no vídeo foi legal, professora. Parece que ele ser mais curtinho ajudou bastante." },
            // Mídias Boas
            { ItemName.TVComVHS, "Eu achei bem legal ver aquele documentário no segundo momento, mas senti que o vídeo atrapalhou a discussão." },
            { ItemName.JornaisERevistas, "As revistas e jornais ajudaram um pouco na discussão, mas eu preferiria que tivessemos outro tipo de material de apoio." },
            { ItemName.QuadroNegro, "A parte em que a gente tinha que discutir enquanto o professor anotava no quadro negro foi legalzinha, mas parece que podia ter sido melhor..." },
            { ItemName.Caderno, "Usar o caderno no meio do debate de aula foi um pouco confuso, eu não sabia se anotava ou se prestava atenção no que os colegas estavam falando." },
            { ItemName.CartazComCanetas, "Adoro escrever com as canetas coloridas, mas fazer isso no meio da discussão ficou um tantinho estranho, profe." },
            // Mídias Fracas
            { ItemName.Retroprojetor, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
            { ItemName.Jornais, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
            { ItemName.CameraPolaroid, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
            { ItemName.VHS, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
            { ItemName.Cartazes, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
            { ItemName.TVComVHSRevolucaoIndustrial, "O segundo momento da aula foi chato… Aquela discussão, com aquela mídia, não fazia sentido." },
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Gravador, "Usar o gravador foi legal, adorei gravar nossa atividade na aula!" },
            { ItemName.Retroprojetor, "Adorei o jeito que usamos o retroprojetor na nossa atividade!" },
            { ItemName.ReprodutorAudio, "Adorei o jeito que usamos o retroprojetor na nossa atividade!" },
            { ItemName.CartazComCanetas, "Depois de ter ouvido falar bastante sobre o que foi a revolução industrial, ter discutido sobre como era o trabalho infantil, ficou muito fácil de interagir com meus colegas e  pintar aquele cartaz!! Valeu profe!" },
            // Mídias Muito Boas
            { ItemName.RetroprojetorSlideMapa, "Adorei o jeito que usamos o retroprojetor com slide com mapa na nossa atividade!" },
            { ItemName.RetroprojetorSlideLinhaTempo, "Adorei o jeito que usamos o retroprojetor com slide com linha do tempo na nossa atividade!" },
            { ItemName.RetroprojetorSlideCicloTrabalho, "Adorei o jeito que usamos o retroprojetor com slide com ciclo do trabalho na nossa atividade!" },
            { ItemName.JornaisERevistas, "Gostei bastante de recortar aqueles jornais e revistas para a atividade." },
            { ItemName.Cartazes, "Nossa, professora! Quando eu tava interagindo com meus colegas e fazendo o cartaz sobre o trabalho infantil foi legal." },
            // Mídias Boas
            { ItemName.Jornais, "Gostei de recortar aqueles jornais para a atividade." },
            { ItemName.QuadroNegro, "Professora, gostei de como você usou o quadro negro, mas acho que seria mais legal se fosse uma mídia que desse pra interagir melhor com meus colegas." },
            { ItemName.ReprodutorAudioComCDRevolucaoIndustrial, "Gostei de ter na sala o reprodutor de áudio na atividade, mas ele não me foi muito útil." },
            { ItemName.TVComVHSRevolucaoIndustrialEditado, "Gostei de ter na sala a TV com VHS editado na atividade, mas ele não me foi muito útil." },
            { ItemName.TVComVHS, "Gostei de ter na sala a TV com VHS na atividade, mas ele não me foi muito útil." },
            { ItemName.FotografiaRevolucaoIndustrial, "Gostei de ter na sala a foto para a atividade, mas ele não me foi muito útil." },
            { ItemName.Diario, "Gostei de ter na sala  diário de uma criança no trabalho infantil para a atividade, mas ele não me foi muito útil." },
            // Mídias Fracas
            { ItemName.LivroDidatico, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.LivroIlustrado, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.Caderno, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.CameraPolaroid, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.VHS, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.TVComVHSRevolucaoIndustrial, "Não fez muito sentido usar aquela mídia para uma atividade..." },
        },

        feedbacksAulaMelhor: new[]
        {
            "Nossa! Que aula incrível! Não vejo a hora de contar para os meus pais tudo o que aprendi.",
            "Essa foi a melhor aula do ano! Na próxima será que vamos aprender mais sobre a revolução industrial?"
        },
        feedbacksAulaMuitoBoa: new[]
        {
            "Foi divertida a aula! Não achei que ia gostar muito, mas acabei me surpreendendo.",
            "Gostei da aula! Agora já sei mais sobre a revolução industrial..."
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

    public static readonly FeedbacksDosAlunos FeedbacksNaMissao3 = new FeedbacksDosAlunos
    (
        feedbacksPorMidiaNoMomento1: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Gravador, "Eu adorei gravar a minha pesquisa, assim posso recorrer a ela qundo quiser no futuro." },
            { ItemName.Enciclopedia, "Nossa, adorei essa aula! A Enciclopédia era muito completa! Eu achei tudo nela." },
            { ItemName.VHSregionalismoEditado, "Caramba, fazer a pesquisa vendo um documentário sobre regionalismo foi muito mais simples! E ele ser editado foi ainda melhor, não tinha partes chatas!" },
            { ItemName.CDsotaques, "Aquela entrevista foi bem interessante, deu para fazer a pesquisa tranquilamente com ela." },
            // Mídias Muito Boas
            { ItemName.VHSregionalismo, "Aquele documentário foi bem interessante, deu para fazer a pesquisa tranquilamente com ele." },
            { ItemName.JornaisERevistas, "Achei legal usar aqueles jornais e revistas para fazer a pesquisa! Nunca reparei em quantas palavras das regiões tem nelas." },
            { ItemName.LivroIlustrado, "Achei interessante fazer a pesquisa no livro ilustrado! Tinha muitas ilustrações dahora, e dava para achar tudo bem facilmente." },
            // Mídias Boas
            { ItemName.Caderno, "Eu gostei de ver o meu caderno para a pequisa sobre regionalismo, mas acho que podiamos ter acessado um material mais focado nisso." },
            { ItemName.Jornais, "Os jornais foram interessantes para fazer a pesquisa, mas não tinha muita variedade de palavras de muitas regiões." },
            { ItemName.LivroDidatico, "Achei legal usar o livro didático para fazer a pesquisa! Nunca reparei em quantas palavras das regiões tem nele." },
            // Mídias Fracas
            { ItemName.ReprodutorAudio, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.CameraPolaroid, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.TVComVHS, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.VHS, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.QuadroNegro, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.Cartazes, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.FolhaSulfite, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.Adedonha, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.Forca, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
            { ItemName.PalavrasCruzadas, "O primeiro momento da aula foi chato… Fazer a pesquisa com aquela mídia, não fazia sentido." },
        },

        feedbacksPorMidiaNoMomento2: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.ReprodutorAudio, "O reprodutor de áudio foi muito bom para a exposição da nossa pesquisa." },
            { ItemName.CDsotaques, "O reprodutor de áudio com CD foi muito bom para a exposição da nossa pesquisa." },
            { ItemName.VHSregionalismo, "A TV com VHS sobre regionalismo foi muito boa para a exposição da nossa pesquisa." },
            { ItemName.VHSregionalismoEditado, "A TV com VHS editado sobre regionalismo foi muito boa para a exposição da nossa pesquisa." },
            { ItemName.Gravador, "Poder gravar a minha pesquisa e mostrar para a classe ouvir foi muito útil, professora." },
            // Mídias Muito Boas
            { ItemName.Enciclopedia, "Foi muito bom poder usar a enciclopédia para expor a minha pesquisa sobre regionalismo." },
            { ItemName.JornaisERevistas, "Foi muito bom poder usar os jornais e as revistas para expor a minha pesquisa sobre regionalismo." },
            { ItemName.QuadroNegro, "Adorei expor a minha pesquisa no quadro negro, professora. Ajudou todo mundo a ver e deixou minha apresentação mais interessante." },
            // Mídias Boas
            { ItemName.FolhaSulfite, "A parte em que a gente tinha que escrever nossa pesquisa na folha sulfite foi legalzinha, mas parece que podia ter sido melhor..." },
            { ItemName.Caderno, "Usar o caderno para registrar nossas pesquisas é sempre legal, né? Mas achei que dessa forma foi um pouco difícil de dividir com os colegas o que eu tinha escrito." },
            { ItemName.Jornais, "Achei meio estranho usar um jornal para expor a minha pesquisa, mas ajudou às vezes na hora de dar alguns exemplos. " },
            { ItemName.LivroDidatico, "Achei meio estranho usar um livro didático  para expor a minha pesquisa, mas ajudou às vezes na hora de dar alguns exemplos." },
            { ItemName.LivroIlustrado, "Achei meio estranho usar um livro ilustrado para expor a minha pesquisa, mas ajudou às vezes na hora de dar alguns exemplos." },
            { ItemName.Cartazes, "Foi muito bom fazer os cartazes com as nossas pesquisas, professora." },
            // Mídias Fracas
            { ItemName.CameraPolaroid, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
            { ItemName.TVComVHS, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
            { ItemName.VHS, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
            { ItemName.Adedonha, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
            { ItemName.Forca, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
            { ItemName.PalavrasCruzadas, "O segundo momento da aula foi chato… Expor a nossa pesquisa com aquela mídia, não fazia o menor sentido." },
        },

        feedbacksPorMidiaNoMomento3: new Dictionary<ItemName, string>()
        {
            // Mídias Melhores
            { ItemName.Adedonha, "Nossa, jogar em sala de aula, esse era meu sonho desde sempre!! E ainda aprendemos muitas palavras novas!! Valeu profe!" },
            { ItemName.Forca, "Nossa, jogar em sala de aula, esse era meu sonho desde sempre!! E ainda aprendemos muitas palavras novas!! Valeu profe!" },
            { ItemName.PalavrasCruzadas, "Nossa, jogar em sala de aula, esse era meu sonho desde sempre!! E ainda aprendemos muitas palavras novas!! Valeu profe!" },
            // Mídias Muito Boas
            { ItemName.QuadroNegro, "Professora, gostei de como você usou o quadro negro, mas acho que seria mais legal se tivesse uma mídia que fosse mais parecida com as coisas que eu faço na minha vida fora da escola." },
            { ItemName.Caderno, "Escrever as coisas no meu caderno pra praticar o vocabulário foi legal, mas prefiro quando as atividades são mais interativas." },
            { ItemName.FolhaSulfite, "Escrever as coisas na minha folha sulfite pra praticar o vocabulário foi legal, mas prefiro quando as atividades são mais interativas." },
            // Mídias Boas
            { ItemName.Enciclopedia, "Usar a enciclopédia pra praticar o vocabulário foi legalzinho, mas ficou chato bem rápido." },
            { ItemName.JornaisERevistas, "Usar o jornais e revistas pra praticar o vocabulário foi legalzinho, mas ficou chato bem rápido." },
            { ItemName.LivroDidatico, "Usar o livro didático pra praticar o vocabulário foi legalzinho, mas ficou chato bem rápido." },
            { ItemName.LivroIlustrado, "Usar o livro ilustrado pra praticar o vocabulário foi legalzinho, mas ficou chato bem rápido." },
            { ItemName.Cartazes, "Usar o cartaz pra praticar o vocabulário foi legalzinho, mas ficou chato bem rápido." },
            // Mídias Fracas
            { ItemName.ReprodutorAudio, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.CDsotaques, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.VHSregionalismo, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.VHSregionalismoEditado, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.Gravador, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.Jornais, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.CameraPolaroid, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.TVComVHS, "Não fez muito sentido usar aquela mídia para uma atividade..." },
            { ItemName.VHS, "Não fez muito sentido usar aquela mídia para uma atividade..." },
        },

        feedbacksAulaMelhor: new[]
        {
            "Nossa! Que aula incrível! Aprendi muito sobre regionalismo e senti que foi de uma forma muito diferente que facilitou todo o processo!",
            "Essa aula foi uma das melhores de todos os tempos! Amei a forma como o meu jeito de pensar e as coisas que eu tô acostumado a fazer fizeram parte da aula."
        },
        feedbacksAulaMuitoBoa: new[]
        {
            "Foi divertida a aula! Não achei que ia gostar muito, mas acabei me surpreendendo.",
            "Gostei da aula! Agora já sei mais sobre regionalismos..."
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