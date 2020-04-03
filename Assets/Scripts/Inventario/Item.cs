using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemName ItemName { get; private set; }
    public Sprite Sprite { get; private set; }

    public string FriendlyName { get; private set; }

    public string Description { get; private set; }
    public string FullDescription { get; private set; }

    public ItemDescriptionsInOneMission DescriptionsInMission1;
    public ItemDescriptionsInOneMission DescriptionsInMission2;
    public ItemDescriptionsInOneMission DescriptionsInMission3;

    public List<ItemName> UpgradeFrom;
    // Se esta mídia é uma mídia após um upgrade ou não é
    public bool IsAnUpgrade() { return UpgradeFrom.Count > 0; }

    public Item(ItemName itemName)
    {
        ItemName = itemName;

        Sprite = ItemSpriteDatabase.GetSpriteOf(ItemName);

        DescriptionsInMission1 = new ItemDescriptionsInOneMission();
        DescriptionsInMission2 = new ItemDescriptionsInOneMission();
        DescriptionsInMission3 = new ItemDescriptionsInOneMission();

        UpgradeFrom = new List<ItemName>();

        switch (itemName)
        {
            case ItemName.ReprodutorAudio:
                FriendlyName = "Reprodutor de Áudio";
                Description = "Hmmm… Preciso achar um CD em algum lugar para isso daqui ser útil.";
                FullDescription = "Um sistema reprodutor de áudio com leitor de CDs.";

                DescriptionsInMission1.StandardDescription = "Esse aparelho reproduz fitas gravadas, CDs ou DVDs com diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes. É possivel ouvir a programação de rádio AM e FM.";

                DescriptionsInMission1.FirstMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que me ajude.";
                DescriptionsInMission1.SecondMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que seja útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca possa encontrar algum CD que me ajude.";

                DescriptionsInMission2.FirstMomentDescription = "O reprodutor de audio pode ser bem utilizado para trabalhar com exposição de conteúdos, mas somente se houver um bom CD sobre o tema.";
                DescriptionsInMission2.SecondMomentDescription = "O reprodutor de audio pode ser bem utilizado para trabalhar com exposição de conteúdos, mas somente se houver um bom CD sobre o tema.";
                DescriptionsInMission2.ThirdMomentDescription = "O reprodutor de audio pode ser bem utilizado para trabalhar com exposição de conteúdos, mas somente se houver um bom CD sobre o tema.";

                DescriptionsInMission3.FirstMomentDescription = "O reprodutor de áudio pode ser bem utilizado para trabalhar com exposição de conteúdos, se houver um CD sobre o tema.";
                DescriptionsInMission3.SecondMomentDescription = "O reprodutor de áudio pode ser bem utilizado para trabalhar com exposição de conteúdos, se houver um CD sobre o tema.";
                DescriptionsInMission3.ThirdMomentDescription = "O reprodutor de áudio pode ser bem utilizado para trabalhar com exposição de conteúdos, se houver um CD sobre o tema.";
                break;
            case ItemName.Cd:
                FriendlyName = "CD";
                Description = "É um CD.";
                FullDescription = "Um CD para ser utilizado com um reprodutor de áudio";

                DescriptionsInMission1.StandardDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";

                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = DescriptionsInMission1.FirstMomentDescription;
                DescriptionsInMission1.ThirdMomentDescription = DescriptionsInMission1.FirstMomentDescription;

                DescriptionsInMission2.FirstMomentDescription = DescriptionsInMission1.FirstMomentDescription;
                DescriptionsInMission2.SecondMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.FirstMomentDescription;

                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            case ItemName.Gravador:
                FriendlyName = "Gravador";
                Description = "Para capturar os sons da natureza… Os alunos também podem usar para gravar algo durante a aula. Uma entrevista, ou ainda o que aprenderam sobre o conteúdo.";
                FullDescription = "É um aparelho de gravação e reprodução de áudio. Pode ser utilizado possibilitando uma maior autonomia para criação de conteúdo. Possui um uso mais específico, fazendo com que sua linguagem seja praticamente apenas verbal, a sua formalidade vai depender de quem está fazendo a produção do conteúdo.";
                DescriptionsInMission1.StandardDescription = "Instrumento eletrônico que grava, cópia, edita e reproduz todo tipo de som. Com microfone acoplado, faz registro sonoro de vozes, músicas e ruídos, dentre outros. Pode gravar e editar programas de rádio.";
                DescriptionsInMission1.FirstMomentDescription = "Durante a exposição de conteúdo, o gravador poderia ser utilizado somente para gravar os sons da aula. Talvez ele seja mais útil de outra forma.";
                DescriptionsInMission1.SecondMomentDescription = "Pode ser utilizado para gravar os conteúdos dentro de sala de aula, ou até para gravar o som do canto de algum pássaro que apareça próximo à sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Quem sabe se eu usasse o gravador para gravar um pássaro local antes da aula seria interessante.";

                DescriptionsInMission2.FirstMomentDescription = "Um gravador dificilmente conseguiria demonstrar com áudio as caracteristicas da época historica que é o tema da aula.";
                DescriptionsInMission2.SecondMomentDescription = "Como o momento é de discussão, os alunos podem utilizar o gravador para gravar algumas sínteses e conclusões da dupla para apresentar aos colegas.";
                DescriptionsInMission2.ThirdMomentDescription = "Nesse momento de elaboração de sínteses, utilizar o gravador pode ser um pouco confuso.";

                DescriptionsInMission3.FirstMomentDescription = "Os alunos podem gravar diferentes vozes e regionalismos de seus colegas. No entanto, apenas a gravação de exemplos não traria informações suficientes para a pesquisa.";
                DescriptionsInMission3.SecondMomentDescription = "O gravador poderia ser utilizado para escutar as vozes gravadas pelos alunos durante a exposição.";
                DescriptionsInMission3.ThirdMomentDescription = "A proposta é trazer algo que faz parte do dia a dia das crianças para esse último momento, talvez o gravador seja melhor aproveitado em outra parte.";
                break;
            case ItemName.GravacaoPassaro:
                FriendlyName = "Gravação do Pássaro";
                Description = "Gravação do canto do pássaro no pátio da escola";
                FullDescription = "Gravação do canto do pássaro no pátio da escola";
                DescriptionsInMission1.FirstMomentDescription = "Uma ótima forma de ilustrar o canto de um pássaro da mesma região que a escola, mas isso não ajuda os alunos a entender sobre os diferentes tipos de pássaros.";
                DescriptionsInMission1.SecondMomentDescription = "Acho que essa gravação pode não ser muito interessante em uma atividade...";
                DescriptionsInMission1.ThirdMomentDescription = "A gravação pode ser boa nesse momento por apresentar um exemplo de um passáro da mesma regionalidade que os alunos.";
                UpgradeFrom.Add(ItemName.Gravador);
                break;
            case ItemName.CameraPolaroid:
                FriendlyName = "Câmera Fotográfica Polaroid";
                Description = "Os alunos podem usar isso para tirar fotos durante a aula, só não sei se tem muito o que fotografar na sala de aula.";
                FullDescription = "É um instrumento de captação de imagem estática para posterior impressão. Permite a captura de representações imagéticas do tempo histórico vivido. Seu uso pedagógico é de captar imagem e na elaboração de conteúdo aprendido. Como possui uma função bem específica sua linguagem se limita a uma linguagem visual de imagens, escolhidas pelos alunos ou professor pode ter um diferente enfoque, que permite uma variedade na linguagem visual.";
                DescriptionsInMission1.StandardDescription = "Máquina fotográfica utilizada para capturar imagens  de qualquer tipo de registro visual como pessoas, animais, espaços ou momentos que são impressas instantaneamente em papel.";
                DescriptionsInMission1.FirstMomentDescription = "Que foto interessante eu poderia tirar dentro da sala de aula? Talvez ela seja mais útil no pátio, se eu achar algo para fotografar.";
                DescriptionsInMission1.SecondMomentDescription = "Como os alunos estão em sala de aula realizando a atividade, não parece ter boa aplicabilidade. Se estivessem no pátio, os alunos poderiam encontrar pássaros ou ninhos.";
                DescriptionsInMission1.ThirdMomentDescription = "Que foto interessante eu poderia tirar dentro da sala de aula? Talvez ela seja mais útil no pátio, se eu achar algo para fotografar.";

                DescriptionsInMission2.FirstMomentDescription = "Não há locais na sala que pudessem ser fotografados com a temática trabalhada pelo professor.";
                DescriptionsInMission2.SecondMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                break;
            case ItemName.FotografiaPassaro:
                FriendlyName = "Fotografia";
                Description = "É a foto de um pássaro local pousado em um banco de escola";
                FullDescription = "É um recurso imagético de expressão de amplo uso. Seu uso pedagógico serve para ilustrar, apresentar e demonstrar conteúdo, permitir a elaboração de novos aprendizados e ampliação de conhecimento. O estudante usa para expressar ideias, ilustrar a elaboração dos conhecimentos aprendidos, apresentar e socializar com o grande grupo. Sua prática social é a de guardar recordações, registrar momentos e locais, denunciar fatos e divulgação. Esta mídia de linguagem não verbal, serve perfeitamente para trabalhar com imagens específicas.";
                DescriptionsInMission1.StandardDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission1.FirstMomentDescription = "O professor pode utilizar essa fotografia de pássaro para ilustrar suas características. Complementando a fala e texto do conteúdo que ele quer trabalhar.";
                DescriptionsInMission1.SecondMomentDescription = "Somente uma foto pode ser pouco para a realização de uma atividade pelo aluno.";
                DescriptionsInMission1.ThirdMomentDescription = "A fotografia apresenta mais detalhes que uma figura do pássaro, como onde está localizado. Pode ser utilizada para complementar a explicação do professor.";
                UpgradeFrom.Add(ItemName.CameraPolaroid);
                break;
            case ItemName.TVComVHS:
                FriendlyName = "TV com reprodutor de VHS";
                Description = "Sem uma fita VHS vai ser difícil achar algo de interessante passando na TV";
                FullDescription = "É um aparelho de exibição de canais sintonizados por satélite e imagens conectados a um reprodutor VHS. Seu uso pedagógico pode ser através de programas que estejam passando ao vivo ou naquele momento, tende a motivar o aprendizado por conta do conteúdo audiovisual. Assim como o reprodutor VHS possui uma linguagem visual, podendo trabalhar em conjunto com ele, ou utilizando canais de televisão.";
                DescriptionsInMission1.StandardDescription = "Sistema eletrônico de recepção/reprodução de imagens e sons de programas televisivos jornalísticos, esportivos, educacionais e ficcionais, gravados ou ao vivo.";
                DescriptionsInMission1.FirstMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";
                DescriptionsInMission1.SecondMomentDescription = "Somente a televisão, caso não esteja passando algo específico sobre pássaros, não contribuirá com o real objetivo da aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";

                DescriptionsInMission2.FirstMomentDescription = "Sem um VHS específico da época historica de estudo , a TV poderia atrapalhar o andamento da aula.";
                DescriptionsInMission2.SecondMomentDescription = "Sem um VHS específico, a TV só teria os canais abertos locais, o que poderia atrapalhar o andamento da aula por conta da dispersão.";
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.SecondMomentDescription;

                DescriptionsInMission3.FirstMomentDescription = "A televisão aberta apresenta informações sobre as regiões do país, mas talvez cause dispersão em sala de aula.";
                DescriptionsInMission3.SecondMomentDescription = "A Televisão poderia ser mais bem utilizada com um vídeo.  Ligada em canal aberto, durante a apresentação da pesquisa, causaria dispersão dos alunos.";
                DescriptionsInMission3.ThirdMomentDescription = "A televisão, embora faça parte da realidade das crianças, pode causar muitas dispersões.";
                break;
            case ItemName.VHS:
                FriendlyName = "Fita VHS";
                Description = "Pássaros, nossos amigos penosos.";
                FullDescription = "É uma fitas em  formato VHS (Vídeo Home System) contendo um documentário curta metragem. Seu uso pedagógico é relacionado a apresentação de conteúdos pelo meio visual, contato com outras culturas sociais e linguísticas. Utiliza de uma linguagem audiovisual, que tende a ser mais interessante para os alunos.";

                DescriptionsInMission1.StandardDescription = "O VHS é a sigla para Video Home System, que consiste em um sistema de captação e reprodução de vídeo e áudio.";

                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = DescriptionsInMission1.FirstMomentDescription;
                DescriptionsInMission1.ThirdMomentDescription = DescriptionsInMission1.FirstMomentDescription;

                DescriptionsInMission2.FirstMomentDescription = DescriptionsInMission1.FirstMomentDescription;
                DescriptionsInMission2.SecondMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.CartazComColecaoDePenas:
                FriendlyName = "Cartaz com Coleção de Penas";
                Description = "Um cartaz com uma coleção de penas";
                FullDescription = "Um cartaz com uma coleção de penas";
                DescriptionsInMission1.FirstMomentDescription = "Esse cartaz pode ser útil para ilustrar as características dos pássaros pelo professor, o que seria mais rico do que somente uma fotografia.";
                DescriptionsInMission1.SecondMomentDescription = "O cartaz com as penas é interessante, pois ilustra de forma real os pássaros, mas seria a melhor escolha em uma atividade individual, sendo que só há um cartaz?";
                DescriptionsInMission1.ThirdMomentDescription = "Talvez o cartaz com penas possa ser melhor utilizado para a apresentação das características gerais dos pássaros, pois fala pouco sobre regionalidades.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.LivroDidatico:
                FriendlyName = "Livro Didático";
                Description = "Para fazer atividades, aprender e revisar o conteúdo. Um clássico!";
                FullDescription = "É um livro de cunho pedagógico composto de conteúdo do currículo escolar. Pode ser usado para pesquisa e resolução de exercícios. A linguagem presente no livro didático é visual e principalmente textual, trazendo o conteúdo de forma intercalada entre textos e imagens.";

                DescriptionsInMission1.StandardDescription = "É um livro impresso de cunho pedagógico composto de exercícios, textos e imagens do conteúdo estudado em sala que acompanha o currículo escolar.";

                DescriptionsInMission1.FirstMomentDescription = "Apresenta o conteúdo por imagem e pela leitura de forma mais específica. O professor pode utilizar trechos do livro para complementar sua exposição.";
                DescriptionsInMission1.SecondMomentDescription = "Um dos maiores aliados dos alunos e professores por apresentar o conteúdo com imagens, conteúdos específicos sobre o tema do currículo, atividades interessantes e linguagem didática.";
                DescriptionsInMission1.ThirdMomentDescription = "O conteúdo enriquecido com imagem e leitura específicos da disciplina pode ser útil, pois os alunos podem acompanhar a explicação de forma individualizada";

                DescriptionsInMission2.FirstMomentDescription = "O Livro didático apresenta algumas informações mais gerais da história da época, dando acesso a todos os alunos.";
                DescriptionsInMission2.SecondMomentDescription = "Os conteúdos sistematizados do livro didático podem auxiliar bastante os alunso durante a discussão, por ser uma linguagem mais acessível.";
                DescriptionsInMission2.ThirdMomentDescription = "O livro não é muito interessante para produzir algo nesse momento da aula.";

                DescriptionsInMission3.FirstMomentDescription = "Livros didáticos, além de são muito utilizados na escola, trazem muitas palavras de diversas regiões.";
                DescriptionsInMission3.SecondMomentDescription = "O livro didático contêm muitas informações e é acessível aos alunos. No entanto, para uma apresentação expositiva a visualização individual pode dispersar a atenção.";
                DescriptionsInMission3.ThirdMomentDescription = "O livro didático já faz parte do dia a dia da escola das crianças, é bastante útil, mas talvez não seja interessante nesse momento.";
                break;
            case ItemName.LivroIlustrado:
                FriendlyName = "Livro Ilustrado";
                Description = "Uaaaaau, quantas figuras lindas! Os alunos vão adorar!";
                FullDescription = "É um livro belamente ilustrado com desenhos, gravuras e fotos coloridas concatenadas a textos explicativos. Usa uma linguagem visual, que exprime certos conteúdos mais facilmente.";

                DescriptionsInMission1.StandardDescription = "É um livro com imagens que ajudam o aluno a entender o conteúdo da aula. As ilustrações podem ser usadas nas aulas de forma a complementar outras mídias mais básicas.";

                DescriptionsInMission1.FirstMomentDescription = "O professor apresenta as características gerais dos pássaros no livro ilustrado, mostrando as figuras aos alunos. Essa exposição é mais rica do que só usar palavras.";
                DescriptionsInMission1.SecondMomentDescription = "O livro ilustrado possui além de texto, varias imagens sobre os conteúdos trabalhados em aula. É bastante rico, tanto para exposição, quanto para as atividades de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "Para além das palavras e ilustrações, é possível encontrar no livro, escritos sobre onde encontrar tais espécies, que fazem parte do momento da aula.";

                DescriptionsInMission2.FirstMomentDescription = "O Livro ilustrado possui muitas informações e imagens da época de forma didática e atrativa aos alunos.";
                DescriptionsInMission2.SecondMomentDescription = "Os conteúdos do livro, além das diversas ilustrações, são ótimos para gerar discussões.";
                DescriptionsInMission2.ThirdMomentDescription = "O livro não é muito interessante para produzir algo nesse momento da aula.";

                DescriptionsInMission3.FirstMomentDescription = "O livro ilustrado traz informações e imagens ilustrando as diversas regiões brasileiras e sendo um bom material de pesquisa.";
                DescriptionsInMission3.SecondMomentDescription = "O livro ilustrado traz imagens e informações importantes para a pesquisa, mas a proposta é de exposição da pesquisa, por isso pode não ser uma boa mídia para o momento.";
                DescriptionsInMission3.ThirdMomentDescription = "O livro ilustrado já faz parte do dia a dia da escola das crianças, é bastante útil, mas talvez não seja interessante nesse momento.";

                UpgradeFrom.Add(ItemName.LivroDidatico);
                break;
            case ItemName.QuadroNegro:
                FriendlyName = "Quadro Negro";
                Description = "Hmmm, é bom lembrar que tanto o professor quanto os alunos podem usar essa mídia. O velho companheiro dos professores.";
                FullDescription = "É um espaço plano reutilizável geralmente riscado com giz branco para transmissão de conteúdo.  Exposição da matéria para grande quantidade de estudantes. Possibilita ao aluno expor suas elaborações sobre os conteúdos e experiências. Tanto o professor quanto o aluno podem utilizá-lo de forma visual, expondo o assunto trabalhado de forma expositiva e/ou colaborativa.";

                DescriptionsInMission1.StandardDescription = "Superfície reutilizável geralmente riscada com giz branco para exposição do conteúdo ou usado de forma colaborativa entre professor e estudante.";

                DescriptionsInMission1.FirstMomentDescription = "O quadro é a mídia mais comumente ultilizada em aulas expositivas, podendo ser utilizada pelo professor para  exposição e sistematização de conteúdos.";
                DescriptionsInMission1.SecondMomentDescription = "Os alunos podem usar o quadro negro como um recurso adicional ao apresentar suas falas durante atividade.";
                DescriptionsInMission1.ThirdMomentDescription = "Com o quadro negro o professor pode elaborar tabelas e esquemas para a sistematização do conteúdo da aula.";

                DescriptionsInMission2.FirstMomentDescription = "O quadro negro é bastante útil para apresentações e sistematizações dos conteúdos, mas ele não é uma novidade para os alunos.";
                DescriptionsInMission2.SecondMomentDescription = "O quadro negro é bastante útil para a mediação das discussões, mas ele não é uma novidade para os alunos.";
                DescriptionsInMission2.ThirdMomentDescription = "O quadro negro, embora muito importante, acaba gerando uma centralidade de quem está escrevendo. A intenção da aula é a interação entre os indivíduos.";

                DescriptionsInMission3.FirstMomentDescription = "O quadro negro não possui informações para uma atividade de pesquisa sobre regionalismo, talvez seja mais útil em um momento expositivo.";
                DescriptionsInMission3.SecondMomentDescription = "O quadro negro pode ser usado na criação de esquemas e sínteses das pesquisas dos alunos, Também interessa por geralmente ser usado pelo professor.";
                DescriptionsInMission3.ThirdMomentDescription = "O quadro negro é muito versátil e pode ser utilizado para a síntese dos conteúdos da aula, mas a proposta continua sendo a de trazer a realidade dos alunos de fora da escola.";
                break;
            case ItemName.TVComVHSPassaros:
                FriendlyName = "TV com VHS sobre pássaros";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission1.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre pássaros.";
                DescriptionsInMission1.FirstMomentDescription = "A exposição de informações é bastante completa nesta mídia. Apresenta imagens, sons, oralidade e leitura.";
                DescriptionsInMission1.SecondMomentDescription = "Passar um vídeo enquanto os alunos fazem um atividade pode ser um pouco confuso...";
                DescriptionsInMission1.ThirdMomentDescription = "A exposição de informações é bastante completa nesta mídia. Apresenta imagens, sons, oralidade e leitura.  Mas como o vídeo não especifica a regionalidade dos pássaros, pode confundir um pouco os alunos.";
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.Cartazes:
                FriendlyName = "Cartazes";
                Description = "Um cartaz em branco, perfeito para criar algo durante a aula";
                FullDescription = "É uma litografia. Geralmente feitos pelos próprios estudantes, utilizados para atividades pedagógicas individuais ou em grupo para proporcionar a produção coletiva, apresentação e socialização do que foi produzido com o grupo escolar, produção de trabalhos escolares que permitem a pesquisa, produção textual, produção artística, expressar opiniões, elaboração dos conteúdos aprendidos e exposição visual de conteúdo pedagógico. Dependendo do uso pode ter um viés mais textual ou mais imagético, mas voltado para o lado visual, dado a sua possibilidade de exposição tende a ter um melhor uso para imagens trabalhadas pela turma.";

                DescriptionsInMission1.StandardDescription = "Anúncio ou aviso de dimensões variadas, geralmente ilustrado com desenhos ou fotografias, apropriado para ser afixado em lugares públicos.";

                DescriptionsInMission1.FirstMomentDescription = "O cartaz em branco funciona de forma semelhante ao quadro negro, porém é possível colar ilustrações e utilizar diversas cores de canetas para a exposição.";
                DescriptionsInMission1.SecondMomentDescription = "O cartaz é uma mídia bastante interessante para atividades práticas dos alunos por conta da versatilidade. Proporciona a escrita, colagem de imagens, desenho, etc.";
                DescriptionsInMission1.ThirdMomentDescription = "Semelhante ao quadro negro, o professor pode elaborar tabelas e esquemas para a sistematização do conteúdo da aula, além da colar imagens para ilustrar.";

                DescriptionsInMission2.FirstMomentDescription = "O cartaz possibilita a apresentação dos conteúdos para os alunos, mas alem de ele não ser uma novidade para eles não possibilita muita versatilidade no trabalho de exposição do professor.";
                DescriptionsInMission2.SecondMomentDescription = "O cartaz tem muitas possibilidades em meio a construção de uma apresentação ou sistematização, mas esse é um momento de discussões, talvez gere dispersões e seja melhor utilizado em outro momento.";
                DescriptionsInMission2.ThirdMomentDescription = "O cartaz é uma mídia bastante conhecida pelos alunos na elaboração de atividades, eles já estão acostumados com ela e sabem como utilizá-la, por isso pode ser uma boa escolha.";
                break;
            case ItemName.Mapa:
                FriendlyName = "Mapa";
                Description = "Um cartaz de um mapa";
                FullDescription = "Um cartaz de um mapa";
                DescriptionsInMission1.StandardDescription = "Um mapa-mundi atualizado, contendo uma representação cartográfica plana, em escala reduzida, de toda a superfície do planeta Terra.";
                DescriptionsInMission1.FirstMomentDescription = "O mapa é uma mídia para representação de um espaço geográfico por imagens. Pode ser um complemento a aula expositiva do professor, apresentando o habitat dos pássaros. Mas acho que seria mais indicado falando da regionalidade.";
                DescriptionsInMission1.SecondMomentDescription = "O Mapa, por conta da leitura associada a imagens, pode ser interessante na utilização dos alunos para uma pesquisa ou atividade. Apesar de não ser a mídia mais completa.";
                DescriptionsInMission1.ThirdMomentDescription = "Como o momento da aula é para falar sobre pássaros locais e regionais, o mapa pode ser um bom aliado.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.Caderno:
                FriendlyName = "Caderno";
                Description = "Na minha época não tinha tantas cores de canetas e adesivos divertidos…";
                FullDescription = "É um artefato histórico atualmente produzidos em papel. Utilizadas para a anotação, expressão textual e armazenamento de informações mais relevantes para os estudantes durante as aulas, serve também de registro histórico daquela época. É um instrumento de comunicação entre o professor e o estudante, de desenvolver a coordenação motora fina, cognitiva, raciocínio lógico, de audição, de visão e também como instrumento de avaliação e de caligrafia. Possui uma linguagem muito versátil, já que pode trabalhar com a forma textual, tanto verbal, sendo ditado pelo professor, como escrita, copiando o que está no quadro, ou em alguma outra mídia. Além do uso de textos, pode-se usar de elementos visuais.";

                DescriptionsInMission1.StandardDescription = "Conjunto de folhas de papel agrupadas num formato portátil, usado para registro, estudo ou anotações escritas, desenhos ou colagens com uso de diversos recursos e materiais para tanto.";

                DescriptionsInMission1.FirstMomentDescription = "O caderno pode ser utilizado como uma mídia de registro ou leitura de conteúdos, bem adequada para os alunos, mas por si só não seria um bom recurso para o professor na exposição.";
                DescriptionsInMission1.SecondMomentDescription = "É o melhor aliado dos alunos para registro e sistematização dos conteúdos. É individual, de fácil acesso e os usuários podem desenvolver esquemas pessoais de registro.";
                DescriptionsInMission1.ThirdMomentDescription = "O caderno pode ser utilizado como uma mídia de registro ou leitura de conteúdos, mas por si só não seria um bom recurso para o professor utilizar na exposição.car";

                DescriptionsInMission2.FirstMomentDescription = "Como o principal objetivo da aula nesse momento é expor os conteúdos, só o caderno não seria suficiente.";
                DescriptionsInMission2.SecondMomentDescription = "O caderno é bom para anotar pontos importantes da discussão, mas o processode anotar enquanto prestão atenção pode deixar os alunos um pouco confusos.";
                DescriptionsInMission2.ThirdMomentDescription = "O caderno é potente na realização de atividades de síntese, porém é bastante individual e não proporciona muita interação entre os colegas.";

                DescriptionsInMission3.FirstMomentDescription = "A proposta da atividade é pesquisar sobre regionalismo, o caderno seria útil para escrever esquemas e sínteses e não tanto para esse caso.";
                DescriptionsInMission3.SecondMomentDescription = "O caderno é uma mídia de registro individual e a proposta é uma exposição da pesquisa para a turma, talvez seja melhor utilizado em outro momento.";
                DescriptionsInMission3.ThirdMomentDescription = "O caderno é útil para escrever esquemas e sínteses, mas o professor pensa em trazer algo de fora do cotidiano escolar.";
                break;
            case ItemName.Jornais:
                FriendlyName = "Jornais";
                Description = "A inflação subiu de novo… Desse jeito onde será que as coisas vão parar?";
                FullDescription = "Esta mídia teve seu início no formato impresso. É utilizada para transmitir informações, contém um gênero textual amplamente estudado em sala de aula, expressam diferentes perspectivas políticas, econômicas, culinárias, de lazer, entre outras que são produzidas socialmente, registram os acontecimentos históricos da época.  É acessível somente a pessoas letradas. Sua prática pedagógica serve para introduzir os estudantes nos conhecimentos da alfabetização, demonstra a diferença de produção textual na perspectiva da diversidade de opiniões, modos, vertentes no ensino e aprendizagem de conteúdos. Assim como os livros didáticos, traz texto escrito complementado por imagens, porém com um estilo de escrita geralmente diferente.";

                DescriptionsInMission1.StandardDescription = "São um meio de comunicação impresso e um produto derivado do conjunto de atividades denominado jornalismo.";

                DescriptionsInMission1.FirstMomentDescription = "Jornais podem auxiliar na exposição por conter imagens e textos em linguagem mais simples. Mas é fundamental encontrar um jornal com reportagem sobre o tema de estudo.";
                DescriptionsInMission1.SecondMomentDescription = "Os Jornais apresentam muitos conteúdos de leitura e algumas imagens, de fácil interpretação, e pode ser utilizado pelos alunos para a criação de um mural, por exemplo.";
                DescriptionsInMission1.ThirdMomentDescription = "As matérias contidas nos jornais não contribuem muito para a temática da aula, mas as figuras e textos podem ser utilizados em outros momentos.";

                DescriptionsInMission2.FirstMomentDescription = "Os jornais possuem informações sobre o ocorrido, mas não possuem exemplares iguais para todos os alunos nesse momento.. então os alunos iriam ficar perdidos durante a exposição.";
                DescriptionsInMission2.SecondMomentDescription = "Os jornais possuem poucas informações sobre o trabalho infantil, além de não possuirem exemplares iguais para todos os alunos nesse momento.. então os alunos iriam ficar perdidos durante a exposição.";
                DescriptionsInMission2.ThirdMomentDescription = "Esses jornais contém imagens e textos que podem ser resignificados e que, depois podem ser recortados ou utilizados pelos alunos na elaboração da síntese.";

                DescriptionsInMission3.FirstMomentDescription = "Os jornais trazem algumas informações sobre palavras regionais em suas entrevistas e notícias.";
                DescriptionsInMission3.SecondMomentDescription = "Os jornais trazem informações sobre as palavras regionais e os costumes das diferentes regiões brasileiras, talvez seja útil para uma pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "Os jornais fazem mais parte da realidade dos adultos do que das crianças, mas pelo menos podem trazer algumas informações sobre regionalismo em suas colunas.";
                break;
            case ItemName.JornaisERevistas:
                FriendlyName = "Jornais e Revistas";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";

                DescriptionsInMission1.StandardDescription = "Material impresso contendo textos e imagens com diversos assuntos, dependendo do tipo de revista. Podem conter entrevistas, comentários, matérias e propagandas.";

                DescriptionsInMission1.FirstMomentDescription = "Uma coleção de jornais e revistas com essa temática pode complementar a aula expositiva do professor com pequenos textos e ilustrações, com linguagem mais simples e de fácil acesso.";
                DescriptionsInMission1.SecondMomentDescription = "Se for possível encontra matérias específicas sobre os pássaros, se torna bastante rico para a utilização dos alunos em atividades dentro da sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Os jornais e, principalmente, as revistas sobre pássaros trazidas pelo professor podem ser aliadas importantes durante a sistematização de conteúdos. Mas não falam muito sobre as questões de regionalidade.";

                DescriptionsInMission2.FirstMomentDescription = "Os jornais e revistas possuem informações sobre o ocorrido, mas não possuem exemplares iguais para todos os alunos nesse momento..";
                DescriptionsInMission2.SecondMomentDescription = "Os jornais e revistas possuem poucas informações sobre o trabalho infantil, além de não possuem exemplares iguais para todos os alunos nesse momento..";
                DescriptionsInMission2.ThirdMomentDescription = "As revistas e jornais contém imagens e algumas informações sobre a temática do trabalho, que podem ser recortadas ou utilizada pelos alunos na elaboração da síntese.";

                DescriptionsInMission3.FirstMomentDescription = "As revistas e jornais possuem diversas entrevistas, matérias e notícias onde se é possível encontrar regionalismo.";
                DescriptionsInMission3.SecondMomentDescription = "As revistas e jornais possuem diversas entrevistas, matérias e notícias onde se é possível encontrar regionalismo, talvez seja útil para uma pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "As revistas e jornais não fazem tanto parte da realidade dos alunos, mas trazem em suas colunas e entrevistas, diversos exemplos de regionalismos.";
                UpgradeFrom.Add(ItemName.Jornais);
                break;
            case ItemName.Retroprojetor:
                FriendlyName = "Retroprojetor";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.StandardDescription = "Aparelho óptico utilizado para a projeção de imagens por meio de transparências.";

                DescriptionsInMission2.StandardDescription = DescriptionsInMission1.StandardDescription;
                DescriptionsInMission2.FirstMomentDescription = "O retroprojetor é uma ótima mídia, se aliada com bons slides, pois sozinho só reflete a luz.";
                DescriptionsInMission2.SecondMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                break;
            case ItemName.RetroprojetorSlideMapa:
                FriendlyName = "Retroprojetor com Slide e  Mapa ";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Transparência, contendo um mapa mundi, que combinadas com um retroprojetor, podem ser refletidas na parede.";

                DescriptionsInMission2.FirstMomentDescription = "O slide com mapa pode ser utilizado para ilustrar as regiões onde ocorreu a Revolução Industrial durante a explicação do professor, tornando mais rico o aprendizado do aluno.";
                DescriptionsInMission2.SecondMomentDescription = "Esse slide, embora trate de questões menos específicas, pode ser utilizado durante a  discussão para localizar os alunos sobre o tema e os locais mencionados durante a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de uma atividade de síntese e de discussão sobre o trabalho no período estudado e na atualidade, talvez esse slide possa ser melhor utilizado em outro momento.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.RetroprojetorSlideLinhaTempo:
                FriendlyName = "Retroprojetor com Slide com Linha do Tempo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre as condições do trabalho desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";

                DescriptionsInMission2.FirstMomentDescription = "O slide com linha do tempo ajuda na compreensão dos fatos trabalhados, de forma esquemática, auxiliando durante a explicação do professor.";
                DescriptionsInMission2.SecondMomentDescription = "Esse slide, embora trate de questões mais gerais, pode ser utilizado para ajudar na localização temporal dos alunos durante a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de um trabalho de síntese e de discussão sobre o trabalho no período trabalhado e na atualidade, talvez esse slide possa ser melhor utilizado em outro momento.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.RetroprojetorSlideCicloTrabalho:
                FriendlyName = "Retroprojetor com Slide com Ciclo do trabalho";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre o ciclo do trabalho infantil desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";

                DescriptionsInMission2.FirstMomentDescription = "Esse slide seria melhor utilizado em outro momento da aula, por hora o professor pretende trabalhar características mais gerais da Revolução Industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um slide com informações sobre o ciclo do trabalho infantil pode ser usado para iniciar discussões sobre os tópicos específicos trabalhados nesse momento, me parece uma ótima ideia.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de um trabalho de síntese e de discussão sobre o trabalho no período trabalhado e na atualidade, talvez esse slide possa ser melhor utilizado em outro momento.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.CartazComCanetas:
                FriendlyName = "Cartaz com canetas coloridas";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Um conjunto de canetas coloridas junto de cartazes, geralmente utilizados para trabalhos expositivos.";

                DescriptionsInMission2.FirstMomentDescription = "Embora as canetas coloridas sejam bastante atrativas aos alunos, não seria o melhor momento para esse conjunto.";
                DescriptionsInMission2.SecondMomentDescription = "Como a proposta é uma discussão, talvez essa mídia expositiva acabe dispersando os alunos e possa ser usada em outro momento de atividade mais prática.";
                DescriptionsInMission2.ThirdMomentDescription = "As canetas coloridas são muito atrativas para os alunos, além do cartaz que já estão habituados a utilizar. Provavelmente não haverão problemas em relação a utilização da mídia para esse trabalho.";

                DescriptionsInMission3.FirstMomentDescription = "O cartaz em branco, junto das canetas coloridas, talvez sejam melhor utilizados em uma atividade de apresentação.";
                DescriptionsInMission3.SecondMomentDescription = "O cartaz em branco, junto das canetas coloridas, é uma mídia bastante utilizada e de domínio dos alunos da turma, onde podem expor os resultados de sua pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "O cartaz em branco com canetas coloridas é uma mídia bastante utilizada e de domínio dos alunos, mas uma mídia inovadora pode proporcionar uma atividade interessante.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.VhsEditado:
                FriendlyName = "VHS Editado";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "O VHS (Video Home System) consiste em um sistema de captação e reprodução de vídeo e áudio, contendo informações selecionadas sobre a revolução industrial.";

                DescriptionsInMission2.FirstMomentDescription = "O VHS foi editado pelo professor, contendo os principais tópicos que ele quer trabalhar em seu planejamento, de duração suficiente para manter a atenção da turma.";
                DescriptionsInMission2.SecondMomentDescription = "Por conta do cuidado do professor em editar as principais partes do documentário, ele tem duração e conteúdo suficiente para manter a atenção dos alunos e mediar a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "Com a atenção dos alunos voltadas para a realização do trabalho de síntese, talvez eles não prestem muita atenção nos detalhes escolhidos pelo professor.";
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.Diario:
                FriendlyName = "Diário";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Diário escrito à mão por uma criança que viveu no período da revolução industrial, contendo registros de suas vivências e seu trabalho na fábrica.";

                DescriptionsInMission2.FirstMomentDescription = "O diário possui muitas informações sobre o dia a dia de uma criança naquele período, de uma perspectiva pouco geral dos fatos, o que pode ajudar um pouco na exposição do professor.";
                DescriptionsInMission2.SecondMomentDescription = "O diário tem informações pessoais e bem específicas sobre a condição do trabalho de uma criança durante esse período histórico, ótimo para utilizar em uma discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "O diário possui muitas informações sobre o dia a dia de uma criança naquele período, mas para esse momento não deve ser muito interessante.";
                break;
            case ItemName.ReprodutorAudioComCDPassaros:
                FriendlyName = "Reprodutor de Áudio + CD sobre Pássaros";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";

                DescriptionsInMission2.StandardDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com o som do canto dos pássaros.";

                DescriptionsInMission1.FirstMomentDescription = "O CD com o som do canto dos pássaros, mesmo que não apresente imagens, pode ser uma boa ferramenta para complementar a explicação do professor.";
                DescriptionsInMission1.SecondMomentDescription = "A reprodução por CD durante a realização da atividade pode proporcionar uma imersão dos alunos na temática dos pássaros, mas não facilita o registro da informação.";
                DescriptionsInMission1.ThirdMomentDescription = "A reprodução do CD poderia contribuir para outro momento da aula, visto que não fala sobre regionalidades. Lembrando que é uma mídia somente sonora, sem conteúdo visual.";
                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            case ItemName.FotografiaRevolucaoIndustrial:
                FriendlyName = "Fotografia da Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission2.FirstMomentDescription = "Fotografias são importantes para a ambientação do aluno dentro daquela perspectiva, durante a explicação do professor, mas podem não ser suficiente nesse caso.";
                DescriptionsInMission2.SecondMomentDescription = "A fotografia de uma criança em situação de trabalho infantil na Revolução Industrial pode ilustrar muitas das condições apresentadas pelo professor e ajudar a sensibilizar os alunos para a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A fotografia, por ser apenas uma, não seria muito útil em uma atividade.";
                break;
            case ItemName.TVComVHSRevolucaoIndustrial:
                FriendlyName = "TV com VHS sobre Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Essas mídias(TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre a revolução industrial.";

                DescriptionsInMission2.FirstMomentDescription = "Um documentário sobre a temática da aula é uma ótima opção para tratar do tema, mas talvez o tempo de duração cause algumas dispersões.";
                DescriptionsInMission2.SecondMomentDescription = "O documentário é bastante extenso, a idéia desse momento é que os alunos foquem na discussão entre si e não em prestar atenção em um documentário.";
                DescriptionsInMission2.ThirdMomentDescription = "O documentário traz informações muito importantes sobre a temática, mas esse é um momento de síntese e atividades, talvez ele seja melhor utilizado em outro momento.";
                break;
            case ItemName.ReprodutorAudioComCDRevolucaoIndustrial:
                FriendlyName = "Reprodutor de Áudio + CD sobre Pássaros";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com uma entrevista de um especialista sobre a revolução industrial.";

                DescriptionsInMission2.FirstMomentDescription = "Levar uma entrevista com um especialista no assunto enriquece muito a exposição.";
                DescriptionsInMission2.SecondMomentDescription = "Esse é um momento de discussão entre a turma, talvez um audío de entrevista ajude a guiar a conversa.";
                DescriptionsInMission2.ThirdMomentDescription = "O CD traz muitas informações sobre o fato histórico escolhido, mas esse é um momento de criação e síntese dos alunos, por isso não deve ser uma boa escolha.";
                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            case ItemName.TVComVHSRevolucaoIndustrialEditado:
                FriendlyName = "TV com VHS Editado sobre Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission2.StandardDescription = "Essas mídias(TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre a revolução industrial.";

                DescriptionsInMission2.FirstMomentDescription = "O VHS foi editado pelo professor, contendo os principais tópicos que ele quer trabalhar em seu planejamento, de duração suficiente para manter a atenção da turma.";
                DescriptionsInMission2.SecondMomentDescription = "Por conta do cuidado do professor em editar as principais partes do documentário, ele tem duração e conteúdo suficiente para manter a atenção dos alunos e mediar a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "Com a atenção dos alunos voltadas para a realização do trabalho de síntese, talvez eles não prestem muita atenção nos detalhes escolhidos pelo professor.";
                break;
            case ItemName.FolhaSulfite:
                FriendlyName = "Folha Sulfite";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Folhas brancas de dimensão A4, que pode ter diversos usos de acordo com a situação, sejam desenhos, avisos, dobraduras, etc.";

                DescriptionsInMission3.FirstMomentDescription = "A folha sulfite está em branco, podendo ser registradas muitas informações, mas sem nenhum registro para pesquisar.";
                DescriptionsInMission3.SecondMomentDescription = "Com a folha sulfite é potente para a exposição da pesquisa, mas é muito pequena e talvez seja difícil dos alunos de trás enxergarem.";
                DescriptionsInMission3.ThirdMomentDescription = "O professor pode solicitar que os alunos escrevam diversas palavras regionais na folha sulfite, mas não foge do que elas já fazem durante os dias na escola.";
                break;
            case ItemName.VHSregionalismo:
                FriendlyName = "VHS Documentário sobre Regionalismo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o documentário sobre os diferentes costumes ou tradições regionais.";

                DescriptionsInMission3.FirstMomentDescription = "Um documentário sobre regionalismos trazido pelo professor, com uma longa duração, pode ser uma boa fonte para pesquisa dos alunos.";
                DescriptionsInMission3.SecondMomentDescription = "Passar um vídeo durante a exposição, por mais que esteja dentro do assunto, pode tirar o foco dos alunos nesse momento.";
                DescriptionsInMission3.ThirdMomentDescription = "O documentário possui muitas informações importantes, mas talvez seja melhor utilizado nos primeiros momentos da aula.";
                break;
            case ItemName.VHSregionalismoEditado:
                FriendlyName = "VHS Editado de Regionalismo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver partes selecionadas pelo professor sobre o documentário sobre regionalismo.";

                DescriptionsInMission3.FirstMomentDescription = "O VHS editado pelo professor apresenta as informações mais pertinentes de forma sucinta, sendo uma boa fonte para a pesquisa dos alunos sobre a temática da aula.";
                DescriptionsInMission3.SecondMomentDescription = "O VHS editado pelo professor apresenta as informações mais pertinentes, mas não ajuda os alunos a expor a sua pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "O documentário editado pelo professor compila muitas informações importantes, de forma suscinta, mas talvez seja melhor utilizado em outro momento da aula.";
                break;
            case ItemName.Enciclopedia:
                FriendlyName = "Enciclopédia";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Coletânea de livros e textos que tratam com maior profundidade o conhecimento humano.";

                DescriptionsInMission3.FirstMomentDescription = "A enciclopédia compila muitas informações importantes para a realização da pesquisa dos alunos sobre regionalismo, possuindo imagens e textos sobre a temática.";
                DescriptionsInMission3.SecondMomentDescription = "A Enciclopédia possui muitos textos sobre regionalismos e exemplos, talvez seja melhor utilizada em outro momento.";
                DescriptionsInMission3.ThirdMomentDescription = "A Enciclopédia não faz parte do dia a dia de muitas crianças, fora da escola. É uma mídia muito útil para pesquisas, mas não para sínteses.";
                break;
            case ItemName.Adedonha:
                FriendlyName = "Jogo Adedonha (Stop)";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Adedonha ou Stop! consiste em um jogo de conhecimentos gerais onde cada coluna da tabela recebe o nome de uma categoria de palavras como animais, carros, cidades, cores etc., e cada linha representa uma rodada do jogo.";

                DescriptionsInMission3.FirstMomentDescription = "O jogo da Adedonha é bastante rico, pois trabalha com palavras que podem trazer a discussão sobre regionalismo, mas talvez seja melhor utilizada em atividade.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo da Adedonha é bastante rico, pois trabalha com palavras e assim trazer a discussão sobre regionalismos, mas talvez seja melhor utilizada em uma atividade.";
                DescriptionsInMission3.ThirdMomentDescription = "Trazer a realidade dos alunos e incluir jogos educativos em atividade de síntese torna a aula mais divertida. É possível trabalhar diferentes palavras regionais com jogo.";
                break;
            case ItemName.Forca:
                FriendlyName = "Jogo da Forca";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Este é um jogo em que o jogador precisa acertar a palavra, tendo somente o número de letras e o tema como dica, antes que o boneco seja enforcado, com um número limitado de erros.";

                DescriptionsInMission3.FirstMomentDescription = "O jogo da forca é bastante potente, pois trabalha com palavras e assim podem trazer a discussão sobre regionalismos, mas talvez seja melhor utilizada em uma atividade.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo da forca é bastante potente, pois trabalha com palavras e as mesmas podem trazer a discussão sobre regionalismos, mas talvez seja melhor utilizada em uma atividade.";
                DescriptionsInMission3.ThirdMomentDescription = "O jogo da forca pode ser utilizado como recurso para apresentar diversas palavras e informações para os alunos em uma atividade atrativa de síntese.";
                break;
            case ItemName.PalavrasCruzadas:
                FriendlyName = "Jogo Palavras Cruzadas";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "O jogo de palavras cruzadas é um passatempo bastante popular e consiste em linhas e colunas que devem ser preenchidas por palavras descobertas pelo jogador.";

                DescriptionsInMission3.FirstMomentDescription = "O jogo das Palavras Cruzadas é bastante rico, pois trabalha com palavras e essas podem trazer a discussão sobre regionalismos, mas talvez seja melhor usar em atividade.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo das Palavras Cruzadas é bastante rico, pois trabalha com palavras e elas podem trazer a discussão sobre regionalismos, mas talvez seja melhor usar em atividade.";
                DescriptionsInMission3.ThirdMomentDescription = "O jogo das palavras cruzadas faz parte do dia a dia das crianças, trazer a ludicidade para a aula, em momentos de atividade pode enriquecer a apropriação do conhecimento.";
                break;
            case ItemName.CDsotaques:
                FriendlyName = "CD Sotaques do Brasil";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission3.StandardDescription = "Um CD com uma entrevista sobre o tema da aula, contendo uma entrevista falando sobre sotaques e regionalismos.";

                DescriptionsInMission3.FirstMomentDescription = "O CD com uma entrevista sobre o tema da aula, pode ser um interessante meio de pesquisa para os alunos, em forma de áudio.";
                DescriptionsInMission3.SecondMomentDescription = "A proposta é de uma apresentação dos resultados da pesquisa, o CD com a entrevista pode ser utilizado, mas não comporta os resultados da pesquisa dos alunos.";
                DescriptionsInMission3.ThirdMomentDescription = "O CD com uma entrevista sobre o tema da aula, pode ser um interessante meio de pesquisa para os alunos, em forma de áudio. Melhor utilizado em outro momento.";
                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            default:
                FriendlyName = "Sem nome";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
        }


        // Manter este código para popular as descrições padrão que faltam
        // enquanto não tivermos descrições para todas as mídias e para os
        // momentos de todas as missões
        // Itens que não têm descrição padrão para a missão 1, nem para a 2 e
        // nem para a missão 3, terão sua descrição padrão = "" (string vazia)

        // Se o item não tem descrição padrão para a missão 2, copiar da
        // sua descrição padrão para a missão 1, se houver
        if (DescriptionsInMission2.StandardDescription.Equals("") && !DescriptionsInMission1.StandardDescription.Equals(""))
            DescriptionsInMission2.StandardDescription = DescriptionsInMission1.StandardDescription;

        // Se o item não tem descrição padrão para a missão 3, copiar da
        // sua descrição padrão para a missão 2 ou da missão 1, nesta ordem
        if (DescriptionsInMission3.StandardDescription.Equals(""))
        {
            if (!DescriptionsInMission2.StandardDescription.Equals(""))
                DescriptionsInMission3.StandardDescription = DescriptionsInMission2.StandardDescription;
            else if (!DescriptionsInMission1.StandardDescription.Equals(""))
                DescriptionsInMission3.StandardDescription = DescriptionsInMission1.StandardDescription;
        }
    }
}