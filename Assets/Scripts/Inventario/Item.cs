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

    public struct DescriptionsInOneMission
    {
        public string StandardDescription;
        public string FirstMomentDescription;
        public string SecondMomentDescription;
        public string ThirdMomentDescription;
    }
    public DescriptionsInOneMission DescriptionsInMission1;
    public DescriptionsInOneMission DescriptionsInMission2;
    public DescriptionsInOneMission DescriptionsInMission3;

    public List<ItemName> UpgradeFrom;
    // Se esta mídia é uma mídia após um upgrade ou não é
    public bool IsAnUpgrade() { return UpgradeFrom.Count > 0; }

    public Item(ItemName itemName)
    {
        ItemName = itemName;

        Sprite = ItemSpriteDatabase.GetSpriteOf(ItemName);

        DescriptionsInMission1 = new DescriptionsInOneMission();
        DescriptionsInMission2 = new DescriptionsInOneMission();
        DescriptionsInMission3 = new DescriptionsInOneMission();

        UpgradeFrom = new List<ItemName>();

        switch (itemName)
        {
            case ItemName.ReprodutorAudio:
                FriendlyName = "Reprodutor de Áudio";
                Description = "Hmmm… Preciso achar um CD em algum lugar para isso daqui ser útil.";
                FullDescription = "Um sistema reprodutor de áudio com leitor de CDs.";
                DescriptionsInMission1.FirstMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que me ajude.";
                DescriptionsInMission1.SecondMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca posso encontrar algum CD que seja útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Um reprodutor de audio sem o CD só iria conseguir ter sons da rádio. Será que tem algo de útil passando agora para essa aula na rádio? Quem sabe na Biblioteca possa encontrar algum CD que me ajude.";
                break;
            case ItemName.Cd:
                FriendlyName = "CD";
                Description = "É um CD.";
                FullDescription = "Um CD para ser utilizado com um reprodutor de áudio";
                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                break;
            case ItemName.Gravador:
                FriendlyName = "Gravador";
                Description = "Para capturar os sons da natureza… Os alunos também podem usar para gravar algo durante a aula. Uma entrevista, ou ainda o que aprenderam sobre o conteúdo.";
                FullDescription = "É um aparelho de gravação e reprodução de áudio. Pode ser utilizado possibilitando uma maior autonomia para criação de conteúdo. Possui um uso mais específico, fazendo com que sua linguagem seja praticamente apenas verbal, a sua formalidade vai depender de quem está fazendo a produção do conteúdo.";
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
                DescriptionsInMission1.FirstMomentDescription = "Que foto interessante eu poderia tirar dentro da sala de aula? Talvez ela seja mais útil no pátio, se eu achar algo para fotografar.";
                DescriptionsInMission1.SecondMomentDescription = "Como os alunos estão em sala de aula realizando a atividade, não parece ter boa aplicabilidade. Se estivessem no pátio, os alunos poderiam encontrar pássaros ou ninhos.";
                DescriptionsInMission1.ThirdMomentDescription = "Que foto interessante eu poderia tirar dentro da sala de aula? Talvez ela seja mais útil no pátio, se eu achar algo para fotografar.";
                break;
            case ItemName.FotografiaPassaro:
                FriendlyName = "Fotografia";
                Description = "É a foto de um pássaro local pousado em um banco de escola";
                FullDescription = "É um recurso imagético de expressão de amplo uso. Seu uso pedagógico serve para ilustrar, apresentar e demonstrar conteúdo, permitir a elaboração de novos aprendizados e ampliação de conhecimento. O estudante usa para expressar ideias, ilustrar a elaboração dos conhecimentos aprendidos, apresentar e socializar com o grande grupo. Sua prática social é a de guardar recordações, registrar momentos e locais, denunciar fatos e divulgação. Esta mídia de linguagem não verbal, serve perfeitamente para trabalhar com imagens específicas.";
                DescriptionsInMission1.FirstMomentDescription = "O professor pode utilizar essa fotografia de pássaro para ilustrar suas características. Complementando a fala e texto do conteúdo que ele quer trabalhar.";
                DescriptionsInMission1.SecondMomentDescription = "Somente uma foto pode ser pouco para a realização de uma atividade pelo aluno.";
                DescriptionsInMission1.ThirdMomentDescription = "A fotografia apresenta mais detalhes que uma figura do pássaro, como onde está localizado. Pode ser utilizada para complementar a explicação do professor.";
                break;
            case ItemName.TV:
                FriendlyName = "TV com reprodutor de VHS";
                Description = "Sem uma fita VHS vai ser difícil achar algo de interessante passando na TV";
                FullDescription = "É um aparelho de exibição de canais sintonizados por satélite e imagens conectados a um reprodutor VHS. Seu uso pedagógico pode ser através de programas que estejam passando ao vivo ou naquele momento, tende a motivar o aprendizado por conta do conteúdo audiovisual. Assim como o reprodutor VHS possui uma linguagem visual, podendo trabalhar em conjunto com ele, ou utilizando canais de televisão.";
                DescriptionsInMission1.FirstMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";
                DescriptionsInMission1.SecondMomentDescription = "Somente a televisão, caso não esteja passando algo específico sobre pássaros, não contribuirá com o real objetivo da aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Será que a TV aberta pode auxiliar a exposição do professor sobre os pássaros? Teria que ter muita sorte de passar um documentário ou matéria no exato momento da aula.";
                break;
            case ItemName.VHS:
                FriendlyName = "Fita VHS";
                Description = "Pássaros, nossos amigos penosos.";
                FullDescription = "É uma fitas em  formato VHS (Vídeo Home System) contendo um documentário curta metragem. Seu uso pedagógico é relacionado a apresentação de conteúdos pelo meio visual, contato com outras culturas sociais e linguísticas. Utiliza de uma linguagem audiovisual, que tende a ser mais interessante para os alunos.";
                DescriptionsInMission1.FirstMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.SecondMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                DescriptionsInMission1.ThirdMomentDescription = "Sem uma forma de reproduzi-la essa mídia não vai ser muito útil.";
                UpgradeFrom.Add(ItemName.TV);
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
                DescriptionsInMission1.FirstMomentDescription = "Apresenta o conteúdo por imagem e pela leitura de forma mais específica. O professor pode utilizar trechos do livro para complementar sua exposição.";
                DescriptionsInMission1.SecondMomentDescription = "Um dos maiores aliados dos alunos e professores por apresentar o conteúdo com imagens, conteúdos específicos sobre o tema do currículo, atividades interessantes e linguagem didática.";
                DescriptionsInMission1.ThirdMomentDescription = "O conteúdo enriquecido com imagem e leitura específicos da disciplina pode ser útil, pois os alunos podem acompanhar a explicação de forma individualizada";
                break;
            case ItemName.LivroIlustrado:
                FriendlyName = "Livro Ilustrado";
                Description = "Uaaaaau, quantas figuras lindas! Os alunos vão adorar!";
                FullDescription = "É um livro belamente ilustrado com desenhos, gravuras e fotos coloridas concatenadas a textos explicativos. Usa uma linguagem visual, que exprime certos conteúdos mais facilmente.";
                DescriptionsInMission1.FirstMomentDescription = "O professor apresenta as características gerais dos pássaros no livro ilustrado, mostrando as figuras aos alunos. Essa exposição é mais rica do que só usar palavras.";
                DescriptionsInMission1.SecondMomentDescription = "O livro ilustrado possui além de texto, varias imagens sobre os conteúdos trabalhados em aula. É bastante rico, tanto para exposição, quanto para as atividades de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "Para além das palavras e ilustrações, é possível encontrar no livro, escritos sobre onde encontrar tais espécies, que fazem parte do momento da aula.";
                UpgradeFrom.Add(ItemName.LivroDidatico);
                break;
            case ItemName.QuadroNegro:
                FriendlyName = "Quadro Negro";
                Description = "Hmmm, é bom lembrar que tanto o professor quanto os alunos podem usar essa mídia. O velho companheiro dos professores.";
                FullDescription = "É um espaço plano reutilizável geralmente riscado com giz branco para transmissão de conteúdo.  Exposição da matéria para grande quantidade de estudantes. Possibilita ao aluno expor suas elaborações sobre os conteúdos e experiências. Tanto o professor quanto o aluno podem utilizá-lo de forma visual, expondo o assunto trabalhado de forma expositiva e/ou colaborativa.";
                DescriptionsInMission1.FirstMomentDescription = "O quadro é a mídia mais comumente ultilizada em aulas expositivas, podendo ser utilizada pelo professor para  exposição e sistematização de conteúdos.";
                DescriptionsInMission1.SecondMomentDescription = "Os alunos podem usar o quadro negro como um recurso adicional ao apresentar suas falas durante atividade.";
                DescriptionsInMission1.ThirdMomentDescription = "Com o quadro negro o professor pode elaborar tabelas e esquemas para a sistematização do conteúdo da aula.";
                break;
            case ItemName.TVComVHS:
                FriendlyName = "Quadro Negro com Estêncil";
                Description = "Um quadro negro, com estêncil";
                FullDescription = "Um quadro negro, com estêncil";
                break;
            case ItemName.Cartazes:
                FriendlyName = "Cartazes";
                Description = "Um cartaz em branco, perfeito para criar algo durante a aula";
                FullDescription = "É uma litografia. Geralmente feitos pelos próprios estudantes, utilizados para atividades pedagógicas individuais ou em grupo para proporcionar a produção coletiva, apresentação e socialização do que foi produzido com o grupo escolar, produção de trabalhos escolares que permitem a pesquisa, produção textual, produção artística, expressar opiniões, elaboração dos conteúdos aprendidos e exposição visual de conteúdo pedagógico. Dependendo do uso pode ter um viés mais textual ou mais imagético, mas voltado para o lado visual, dado a sua possibilidade de exposição tende a ter um melhor uso para imagens trabalhadas pela turma.";
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
                DescriptionsInMission1.FirstMomentDescription = "O caderno pode ser utilizado como uma mídia de registro ou leitura de conteúdos, bem adequada para os alunos, mas por si só não seria um bom recurso para o professor na exposição.";
                DescriptionsInMission1.SecondMomentDescription = "É o melhor aliado dos alunos para registro e sistematização dos conteúdos. É individual, de fácil acesso e os usuários podem desenvolver esquemas pessoais de registro.";
                DescriptionsInMission1.ThirdMomentDescription = "O caderno pode ser utilizado como uma mídia de registro ou leitura de conteúdos, mas por si só não seria um bom recurso para o professor utilizar na exposição.car";
                break;
            case ItemName.Jornais:
                FriendlyName = "Jornais";
                Description = "A inflação subiu de novo… Desse jeito onde será que as coisas vão parar?";
                FullDescription = "Esta mídia teve seu início no formato impresso. É utilizada para transmitir informações, contém um gênero textual amplamente estudado em sala de aula, expressam diferentes perspectivas políticas, econômicas, culinárias, de lazer, entre outras que são produzidas socialmente, registram os acontecimentos históricos da época.  É acessível somente a pessoas letradas. Sua prática pedagógica serve para introduzir os estudantes nos conhecimentos da alfabetização, demonstra a diferença de produção textual na perspectiva da diversidade de opiniões, modos, vertentes no ensino e aprendizagem de conteúdos. Assim como os livros didáticos, traz texto escrito complementado por imagens, porém com um estilo de escrita geralmente diferente.";
                DescriptionsInMission1.FirstMomentDescription = "Jornais podem auxiliar na exposição por conter imagens e textos em linguagem mais simples. Mas é fundamental encontrar um jornal com reportagem sobre o tema de estudo.";
                DescriptionsInMission1.SecondMomentDescription = "Os Jornais apresentam muitos conteúdos de leitura e algumas imagens, de fácil interpretação, e pode ser utilizado pelos alunos para a criação de um mural, por exemplo.";
                DescriptionsInMission1.ThirdMomentDescription = "As matérias contidas nos jornais não contribuem muito para a temática da aula, mas as figuras e textos podem ser utilizados em outros momentos.";
                break;
            case ItemName.JornaisEResvistas:
                FriendlyName = "Jornais e Revistas";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";
                DescriptionsInMission1.FirstMomentDescription = "Uma coleção de jornais e revistas com essa temática pode complementar a aula expositiva do professor com pequenos textos e ilustrações, com linguagem mais simples e de fácil acesso.";
                DescriptionsInMission1.SecondMomentDescription = "Se for possível encontra matérias específicas sobre os pássaros, se torna bastante rico para a utilização dos alunos em atividades dentro da sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Os jornais e, principalmente, as revistas sobre pássaros trazidas pelo professor podem ser aliadas importantes durante a sistematização de conteúdos. Mas não falam muito sobre as questões de regionalidade.";
                UpgradeFrom.Add(ItemName.Jornais);
                break;
            case ItemName.RetroprojetorSlideMapa:
                FriendlyName = "Retroprojetor c/ Slide e  Mapa ";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.RetroprojetorSlideLinhaTempo:
                FriendlyName = "Retroprojetor c/ Slide com Linha do Tempo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.RetroprojetorSlideCicloTrabalho:
                FriendlyName = "Retroprojetor c/ Slide com Ciclo do trabalho";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.CartazComCanetas:
                FriendlyName = "Cartas com caneta colorida";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.VhsEditado:
                FriendlyName = "VHS Editado";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.Diario:
                FriendlyName = "Diário";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.Retroprojetor:
                FriendlyName = "Retroprojetor sem um slide";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
            case ItemName.SemNome:
                FriendlyName = "Sem nome";
                Description = "Sem descrição";
                FullDescription = "Sem texto";
                break;
        }
        // Temporário até o Lucas enviar o link para as descrições gerais
        DescriptionsInMission1.StandardDescription = DescriptionsInMission1.SecondMomentDescription;
    }
}