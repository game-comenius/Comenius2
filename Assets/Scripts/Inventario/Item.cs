﻿using System;
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
                DescriptionsInMission1.StandardDescription = "Esse aparelho reproduz fitas gravadas, CDs ou DVDs com diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes.";
                DescriptionsInMission1.FirstMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que me ajude.";
                DescriptionsInMission1.SecondMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que seja útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca possa encontrar algum CD que me ajude.";
                break;
            case ItemName.Cd:
                FriendlyName = "CD";
                Description = "É um CD.";
                FullDescription = "Um CD para ser utilizado com um reprodutor de áudio";
                DescriptionsInMission1.StandardDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";
                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                break;
            case ItemName.Gravador:
                FriendlyName = "Gravador";
                Description = "Para capturar os sons da natureza… Os alunos também podem usar para gravar algo durante a aula. Uma entrevista, ou ainda o que aprenderam sobre o conteúdo.";
                FullDescription = "É um aparelho de gravação e reprodução de áudio. Pode ser utilizado possibilitando uma maior autonomia para criação de conteúdo. Possui um uso mais específico, fazendo com que sua linguagem seja praticamente apenas verbal, a sua formalidade vai depender de quem está fazendo a produção do conteúdo.";
                DescriptionsInMission1.StandardDescription = "Instrumento eletrônico que grava, cópia, edita e reproduz todo tipo de som. Com microfone acoplado, faz registro sonoro de vozes, músicas e ruídos, dentre outros.";
                DescriptionsInMission1.FirstMomentDescription = "Durante a exposição de conteúdo, o gravador poderia ser utilizado somente para gravar os sons da aula. Talvez ele seja mais útil de outra forma.";
                DescriptionsInMission1.SecondMomentDescription = "Pode ser utilizado para gravar os conteúdos dentro de sala de aula, ou até para gravar o som do canto de algum pássaro que apareça próximo à sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Quem sabe se eu usasse o gravador para gravar um pássaro local antes da aula seria interessante.";
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
                break;
            case ItemName.FotografiaPassaro:
                FriendlyName = "Fotografia";
                Description = "É a foto de um pássaro local pousado em um banco de escola";
                FullDescription = "É um recurso imagético de expressão de amplo uso. Seu uso pedagógico serve para ilustrar, apresentar e demonstrar conteúdo, permitir a elaboração de novos aprendizados e ampliação de conhecimento. O estudante usa para expressar ideias, ilustrar a elaboração dos conhecimentos aprendidos, apresentar e socializar com o grande grupo. Sua prática social é a de guardar recordações, registrar momentos e locais, denunciar fatos e divulgação. Esta mídia de linguagem não verbal, serve perfeitamente para trabalhar com imagens específicas.";
                DescriptionsInMission1.StandardDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission1.FirstMomentDescription = "O professor pode utilizar essa fotografia de pássaro para ilustrar suas características. Complementando a fala e texto do conteúdo que ele quer trabalhar.";
                DescriptionsInMission1.SecondMomentDescription = "Somente uma foto pode ser pouco para a realização de uma atividade pelo aluno.";
                DescriptionsInMission1.ThirdMomentDescription = "A fotografia apresenta mais detalhes que uma figura do pássaro, como onde está localizado. Pode ser utilizada para complementar a explicação do professor.";
                break;
            case ItemName.TVComVHS:
                FriendlyName = "TV com reprodutor de VHS";
                Description = "Sem uma fita VHS vai ser difícil achar algo de interessante passando na TV";
                FullDescription = "É um aparelho de exibição de canais sintonizados por satélite e imagens conectados a um reprodutor VHS. Seu uso pedagógico pode ser através de programas que estejam passando ao vivo ou naquele momento, tende a motivar o aprendizado por conta do conteúdo audiovisual. Assim como o reprodutor VHS possui uma linguagem visual, podendo trabalhar em conjunto com ele, ou utilizando canais de televisão.";
                DescriptionsInMission1.StandardDescription = "Sistema eletrônico de recepção/reprodução de imagens e sons de programas televisivos jornalísticos, esportivos, educacionais e ficcionais, gravados ou ao vivo.";
                DescriptionsInMission1.FirstMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";
                DescriptionsInMission1.SecondMomentDescription = "Somente a televisão, caso não esteja passando algo específico sobre pássaros, não contribuirá com o real objetivo da aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";
                break;
            case ItemName.VHS:
                FriendlyName = "Fita VHS";
                Description = "Pássaros, nossos amigos penosos.";
                FullDescription = "É uma fitas em  formato VHS (Vídeo Home System) contendo um documentário curta metragem. Seu uso pedagógico é relacionado a apresentação de conteúdos pelo meio visual, contato com outras culturas sociais e linguísticas. Utiliza de uma linguagem audiovisual, que tende a ser mais interessante para os alunos.";
                DescriptionsInMission1.StandardDescription = "O VHS é a sigla para Video Home System, que consiste em um sistema de captação e reprodução de vídeo e áudio.";
                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.CartazComColecaoDePenas:
                FriendlyName = "Cartaz com Coleção de Penas";
                Description = "Um cartaz com uma coleção de penas";
                FullDescription = "Um cartaz com uma coleção de penas";
                DescriptionsInMission1.FirstMomentDescription = "Esse cartaz pode ser útil para ilustrar as características dos pássaros pelo professor, o que seria mais rico do que somente uma fotografia.";
                DescriptionsInMission1.SecondMomentDescription = "O cartaz com as penas é interessante, pois ilustra de forma real os pássaros, mas seria a melhor escolha em uma atividade individual, sendo que só há um cartaz?";
                DescriptionsInMission1.ThirdMomentDescription = "Talvez o cartaz com penas possa ser melhor utilizado para a apresentação das características gerais dos pássaros, pois fala pouco sobre regionalidades.";
                break;
            case ItemName.LivroDidatico:
                FriendlyName = "Livro Didático";
                Description = "Para fazer atividades, aprender e revisar o conteúdo. Um clássico!";
                FullDescription = "É um livro de cunho pedagógico composto de conteúdo do currículo escolar. Pode ser usado para pesquisa e resolução de exercícios. A linguagem presente no livro didático é visual e principalmente textual, trazendo o conteúdo de forma intercalada entre textos e imagens.";
                DescriptionsInMission1.StandardDescription = "É um livro impresso de cunho pedagógico composto de exercícios, textos e imagens do conteúdo estudado em sala que acompanha o currículo escolar.";
                DescriptionsInMission1.FirstMomentDescription = "Apresenta o conteúdo por imagem e pela leitura de forma mais específica. O professor pode utilizar trechos do livro para complementar sua exposição.";
                DescriptionsInMission1.SecondMomentDescription = "Um dos maiores aliados dos alunos e professores por apresentar o conteúdo com imagens, conteúdos específicos sobre o tema do currículo, atividades interessantes e linguagem didática.";
                DescriptionsInMission1.ThirdMomentDescription = "O conteúdo enriquecido com imagem e leitura específicos da disciplina pode ser útil, pois os alunos podem acompanhar a explicação de forma individualizada";
                break;
            case ItemName.LivroIlustrado:
                FriendlyName = "Livro Ilustrado";
                Description = "Uaaaaau, quantas figuras lindas! Os alunos vão adorar!";
                FullDescription = "É um livro belamente ilustrado com desenhos, gravuras e fotos coloridas concatenadas a textos explicativos. Usa uma linguagem visual, que exprime certos conteúdos mais facilmente.";
                DescriptionsInMission1.StandardDescription = "É um livro com imagens que ajudam o aluno a entender o conteúdo da aula. As ilustrações podem ser usadas nas aulas de forma a complementar outras mídias mais básicas.";
                DescriptionsInMission1.FirstMomentDescription = "O professor apresenta as características gerais dos pássaros no livro ilustrado, mostrando as figuras aos alunos. Essa exposição é mais rica do que só usar palavras.";
                DescriptionsInMission1.SecondMomentDescription = "O livro ilustrado possui além de texto, varias imagens sobre os conteúdos trabalhados em aula. É bastante rico, tanto para exposição, quanto para as atividades de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "Para além das palavras e ilustrações, é possível encontrar no livro, escritos sobre onde encontrar tais espécies, que fazem parte do momento da aula.";
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
                break;
            case ItemName.TVComVHSDePassaros:
                FriendlyName = "TV com VHS";
                Description = "Um quadro negro, com estêncil";
                FullDescription = "Um quadro negro, com estêncil";
                DescriptionsInMission1.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre a revolução industrial.";
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
                break;
            case ItemName.Mapa:
                FriendlyName = "Mapa";
                Description = "Um cartaz de um mapa";
                FullDescription = "Um cartaz de um mapa";
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
                break;
            case ItemName.Jornais:
                FriendlyName = "Jornais";
                Description = "A inflação subiu de novo… Desse jeito onde será que as coisas vão parar?";
                FullDescription = "Esta mídia teve seu início no formato impresso. É utilizada para transmitir informações, contém um gênero textual amplamente estudado em sala de aula, expressam diferentes perspectivas políticas, econômicas, culinárias, de lazer, entre outras que são produzidas socialmente, registram os acontecimentos históricos da época.  É acessível somente a pessoas letradas. Sua prática pedagógica serve para introduzir os estudantes nos conhecimentos da alfabetização, demonstra a diferença de produção textual na perspectiva da diversidade de opiniões, modos, vertentes no ensino e aprendizagem de conteúdos. Assim como os livros didáticos, traz texto escrito complementado por imagens, porém com um estilo de escrita geralmente diferente.";
                DescriptionsInMission1.StandardDescription = "São um meio de comunicação impresso e um produto derivado do conjunto de atividades denominado jornalismo.";
                DescriptionsInMission1.FirstMomentDescription = "Jornais podem auxiliar na exposição por conter imagens e textos em linguagem mais simples. Mas é fundamental encontrar um jornal com reportagem sobre o tema de estudo.";
                DescriptionsInMission1.SecondMomentDescription = "Os Jornais apresentam muitos conteúdos de leitura e algumas imagens, de fácil interpretação, e pode ser utilizado pelos alunos para a criação de um mural, por exemplo.";
                DescriptionsInMission1.ThirdMomentDescription = "As matérias contidas nos jornais não contribuem muito para a temática da aula, mas as figuras e textos podem ser utilizados em outros momentos.";
                break;
            case ItemName.JornaisERevistas:
                FriendlyName = "Jornais e Revistas";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";
                DescriptionsInMission1.StandardDescription = "Material impresso contendo textos e imagens com diversos assuntos, dependendo do tipo de revista. Podem conter entrevistas, comentários, matérias e propagandas.";
                DescriptionsInMission1.FirstMomentDescription = "Uma coleção de jornais e revistas com essa temática pode complementar a aula expositiva do professor com pequenos textos e ilustrações, com linguagem mais simples e de fácil acesso.";
                DescriptionsInMission1.SecondMomentDescription = "Se for possível encontra matérias específicas sobre os pássaros, se torna bastante rico para a utilização dos alunos em atividades dentro da sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Os jornais e, principalmente, as revistas sobre pássaros trazidas pelo professor podem ser aliadas importantes durante a sistematização de conteúdos. Mas não falam muito sobre as questões de regionalidade.";
                UpgradeFrom.Add(ItemName.Jornais);
                break;
            case ItemName.RetroprojetorSlideMapa:
                FriendlyName = "Retroprojetor c/ Slide e  Mapa ";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Transparência, contendo um mapa mundi, que combinadas com um retroprojetor, podem ser refletidas na parede.";
                break;
            case ItemName.RetroprojetorSlideLinhaTempo:
                FriendlyName = "Retroprojetor c/ Slide com Linha do Tempo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre as condições do trabalho desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";
                break;
            case ItemName.RetroprojetorSlideCicloTrabalho:
                FriendlyName = "Retroprojetor c/ Slide com Ciclo do trabalho";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre o ciclo do trabalho infantil desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";
                break;
            case ItemName.CartazComCanetas:
                FriendlyName = "Cartas com caneta colorida";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Um conjunto de canetas coloridas junto de cartazes, geralmente utilizados para trabalhos expositivos.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.VhsEditado:
                FriendlyName = "VHS Editado";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "O VHS (Video Home System) consiste em um sistema de captação e reprodução de vídeo e áudio, contendo informações selecionadas sobre a revolução industrial.";
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
            case ItemName.Retroprojetor:
                FriendlyName = "Retroprojetor sem um slide";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                DescriptionsInMission2.StandardDescription = "Aparelho óptico utilizado para a projeção de imagens por meio de transparências.";
                break;
            case ItemName.ReprodutorAudioComCD:
                FriendlyName = "Reprodutor de Áudio + CD sobre Pássaros";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";
                DescriptionsInMission1.FirstMomentDescription = "O CD com o som do canto dos pássaros, mesmo que não apresente imagens, pode ser uma boa ferramenta para complementar a explicação do professor.";
                DescriptionsInMission1.SecondMomentDescription = "A reprodução por CD durante a realização da atividade pode proporcionar uma imersão dos alunos na temática dos pássaros, mas não facilita o registro da informação.";
                DescriptionsInMission1.ThirdMomentDescription = "A reprodução do CD poderia contribuir para outro momento da aula, visto que não fala sobre regionalidades. Lembrando que é uma mídia somente sonora, sem conteúdo visual.";
                DescriptionsInMission2.StandardDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com uma entrevista de um especialista sobre a revolução industrial.";
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