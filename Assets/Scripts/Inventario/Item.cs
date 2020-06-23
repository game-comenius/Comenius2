using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public ItemName ItemName { get; private set; }
    public Sprite Sprite { get; private set; }

    public string FriendlyName { get; private set; }

    //descrições antigas, deixando aqui por enquanto pra não quebrar nada (TODO: limpar isso)
    public string Description { get; private set; }
    public string FullDescription { get; private set; }

    /*//string com o que a Lurdinha fala quando pega a mídia
    public string DialogueWhenAcquired { get; private set; }

    //string com a descrição que vai para o inventário
    public string InventoryDescription { get; private set; }

    //textos do "Saiba Mais"
    public string Geral { get; private set; }
    public string Com { get; private set; }
    public string Sobre { get; private set; }
    public string Atraves { get; private set; }*/

    //descrições que aparecem na interface de planejamento
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
                FriendlyName = "Aparelho de Som";
                Description = "Hmmm… Preciso achar um CD em algum lugar para isso daqui ser útil.";
                FullDescription = "Um sistema aparelho de som com leitor de CDs.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um aparelho de som! Posso gravar, usar o rádio e se eu encontrar alguma fita cassete ou CD os alunos vão amar a aula.";
                DescriptionsInMission1.InventoryDescription = "Esse aparelho reproduz fitas cassete e CDs com diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes. Ainda pode gravar ou ser sintonizado em uma estação de rádio.";
                DescriptionsInMission1.Geral = "Reproduz diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes.";
                DescriptionsInMission1.Com = "Uso diretamente ligado à ouvir, possibilitando a reprodução da atividade realizada por ele, seja para estudo, entretenimento ou informação.";
                DescriptionsInMission1.Sobre = "Pode ser utilizado para estudar os conteúdos, as formas, as linguagens da gravação, por exemplo, de diferentes gêneros musicais ou programas literários sonoros, como audiolivros.";
                DescriptionsInMission1.Atraves = "Pode ser usado como base sonora complementar para qualquer atividade, como ilustração, informação ou acompanhamento. ";

                //DescriptionsInMission1.StandardDescription = "Esse aparelho reproduz fitas gravadas, CDs ou DVDs com diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes. É possivel ouvir a programação de rádio AM e FM.";
                DescriptionsInMission1.StandardDescription = "Um aparelho de som! Agora só falta achar um CD ou fita gravada para usar com ele!";


                DescriptionsInMission1.FirstMomentDescription = "O aparelho de som pode ser usado para gravar o canto de algum pássaro ou até quem sabe pegar algum CD na bibiloteca.";
                DescriptionsInMission1.SecondMomentDescription = "O aparelho de som pode ser usado para gravar o canto de algum pássaro.";
                DescriptionsInMission1.ThirdMomentDescription = "Com essa mídia os alunos podem fazer gravações de áudio que podem ser usadas na exposição da pesquisa.";

                DescriptionsInMission2.FirstMomentDescription = "O aparelho de som pode ser bem utilizado para trabalhar com exposição de conteúdos.";
                DescriptionsInMission2.SecondMomentDescription = "O aparelho de som pode ser bem utilizado para trabalhar com exposição de conteúdo e para gravar momentos de discussões dos alunos.";
                DescriptionsInMission2.ThirdMomentDescription = "O aparelho de som pode ser bem utilizado em atividades práticas, podendo ser usado para sistematização de conteúdos discutidos e depois apresentado para a turma.";

                DescriptionsInMission3.FirstMomentDescription = "Com essa mídia os alunos podem gravar diferentes vozes e regionalismos de seus colegas.";
                DescriptionsInMission3.SecondMomentDescription = "Essa mídia pode ser bem utilizada para escutar as vozes gravadas pelos alunos durante a exposição.";
                DescriptionsInMission3.ThirdMomentDescription = "Essa mídia reproduz fitas gravadas e CDS com diversos tipos de conteúdo sonoro como músicas, entrevistas ou sons ambientes, como também faz gravações de áudio.";
                break;
            case ItemName.Cd:
                FriendlyName = "CD";
                Description = "É um CD.";
                FullDescription = "Um CD para ser utilizado com um aparelho de som";

                DescriptionsInMission1.DialogueWhenAcquired = "Um CD junto com o aparelho de som os alunos poderão ouvir o canto de pássaros de diferentes regiões!";
                DescriptionsInMission1.InventoryDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";
                DescriptionsInMission1.Geral = "Áudio com o canto de pássaros de diferentes regiões.";
                DescriptionsInMission1.Com = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";
                DescriptionsInMission1.Sobre = "Com o áudio contido nesse recurso, os estudantes podem refletir em grupos sobre os diferentes cantos que ouviram.";
                DescriptionsInMission1.Atraves = "Podem criar perguntas e fazer uma pesquisa sobre as suas dúvidas.";

                DescriptionsInMission2.DialogueWhenAcquired = "Um CD com entrevista de um especialista vai ser muito importante para a aula!";
                DescriptionsInMission2.InventoryDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";
                DescriptionsInMission2.Geral = "Áudio com uma entrevista de um especialista sobre a revolução industrial.";
                DescriptionsInMission2.Com = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";
                DescriptionsInMission2.Sobre = "Com a entrevista contida neste recurso, os estudantes terão que refletir em grupos e em seguida escrever um texto apontando o que significou mais para eles do que ouviram na entrevista.";
                DescriptionsInMission2.Atraves = "Os estudantes poderiam criar perguntas e fazer uma nova entrevista com algum outro professor de história.";

                //DescriptionsInMission1.StandardDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";
                DescriptionsInMission1.StandardDescription = "Um aparelho de som com CD! Isso pode fazer os alunos se divertirem mais na aula!";

                DescriptionsInMission1.FirstMomentDescription = "CD com o som do canto dos pássaros.";
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

                DescriptionsInMission1.DialogueWhenAcquired = "Um gravador! Posso gravar sons de animais, fazer entrevistas, gravar os conteúdos. São muitas as possibilidades com essa mídia!";
                DescriptionsInMission1.InventoryDescription = "Instrumento eletrônico que grava, cópia, edita e reproduz todo tipo de som. Com microfone acoplado, faz registro sonoro de vozes, músicas e ruídos, dentre outros.";
                DescriptionsInMission1.Geral = "Dispositivo de gravação de sons, dentro ou fora da sala de aula.";
                DescriptionsInMission1.Com = "Usar para ouvir o registro de um diário pessoal, sons de animais, entrevistas ou de conteúdos por áudio.";
                DescriptionsInMission1.Sobre = "Permite reflexão sobre as diferenças entre os registros de texto e os que contém voz/sons, qual a importância de ouvir a voz/som original.";
                DescriptionsInMission1.Atraves = "É possível realizar atividades com os alunos de criação e produção de materiais sonoros como entrevistas, radionovelas, programas de humor bem como gravação de sons na natureza ou de apresentações musicais no momento em que acontecem.";

                //DescriptionsInMission1.StandardDescription = "Instrumento eletrônico que grava, cópia, edita e reproduz todo tipo de som. Com microfone acoplado, faz registro sonoro de vozes, músicas e ruídos, dentre outros. Pode gravar e editar programas de rádio.";
                DescriptionsInMission1.StandardDescription = "Um gravador! Posso gravar muitas coisas com ele, mas é claro que durante a aula só posso gravar coisas dentro da sala de aula.";

                DescriptionsInMission1.FirstMomentDescription = "Essa mídia pode ser usada para gravar o canto de diferentes pássaros.";
                DescriptionsInMission1.SecondMomentDescription = "Pode ser utilizado para gravar os conteúdos ou até para gravar o som do canto de algum pássaro e ajudar na sitematização do assunto.";
                DescriptionsInMission1.ThirdMomentDescription = "Essa mídia pode gravar o som de algum pássaro local e depois ser reproduzida para o grupo.";

                DescriptionsInMission2.FirstMomentDescription = "Um gravador dificilmente conseguiria demonstrar com áudio as caracteristicas da época historica que é o tema da aula.";
                DescriptionsInMission2.SecondMomentDescription = "Como o momento é de discussão, os alunos podem utilizar o gravador para gravar algumas sínteses e conclusões da dupla para apresentar aos colegas.";
                DescriptionsInMission2.ThirdMomentDescription = "Nesse momento de elaboração de sínteses, utilizar o gravador pode ser um pouco confuso.";

                DescriptionsInMission3.FirstMomentDescription = "Os alunos podem gravar diferentes vozes e regionalismos de seus colegas. ";
                DescriptionsInMission3.SecondMomentDescription = "O gravador pode ser utilizado para escutar as vozes gravadas pelos alunos durante a exposição.";
                DescriptionsInMission3.ThirdMomentDescription = "Dispositivo de gravação de sons, dentro ou fora da sala de aula.";
                break;
            case ItemName.GravacaoPassaro:
                FriendlyName = "Gravação do Pássaro";
                Description = "Gravação do canto do pássaro no pátio da escola";
                FullDescription = "Gravação do canto do pássaro no pátio da escola";

                DescriptionsInMission1.DialogueWhenAcquired = "A gravação do canto de um pássaro! Isso pode fazer os alunos se divertirem na aula e conhecerem mais sobre os pássaros e seus cantos!";
                DescriptionsInMission1.InventoryDescription = "Essa mídia reproduz o som do canto do pássaro do pátio da escola";
                DescriptionsInMission1.Geral = "Um aparelho utilizado para a reprodução de gravações que armazena informações em formato compacto, podem ser usadas para ouvir o aúdio na aula.";
                DescriptionsInMission1.Com = "Pode ser ouvido pela turma para auxiliar nos processos de ensino aprendizagem.";
                DescriptionsInMission1.Sobre = "Permite reflexão sobre as diferenças entre os registros de texto e os que contém voz/sons, qual a importância de ouvir a voz/som original.";
                DescriptionsInMission1.Atraves = "Realizar com a turma uma gravação sobre qualquer assunto, de acordo com a disciplina e interesse dos alunos e, reproduzir em sala para as outros colegas.";

                //DescriptionsInMission1.StandardDescription = "Gravação do canto do pássaro no pátio da escola.";
                DescriptionsInMission1.StandardDescription = "Um aparelho de som com gravação! Isso pode fazer os alunos se divertirem mais na aula!";

                DescriptionsInMission1.FirstMomentDescription = "Uma forma de ilustrar o canto de um pássaro da mesma região que a escola.";
                DescriptionsInMission1.SecondMomentDescription = "A gravação do canto contribui em uma ativiade de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "A gravação do canto do pássaro pode ser útil para complementar a exposição.";
                UpgradeFrom.Add(ItemName.Gravador);
                break;
            case ItemName.CameraPolaroid:
                FriendlyName = "Câmera Fotográfica Polaroid";
                Description = "Os alunos podem usar isso para tirar fotos durante a aula, só não sei se tem muito o que fotografar na sala de aula.";
                FullDescription = "É um instrumento de captação de imagem estática para posterior impressão. Permite a captura de representações imagéticas do tempo histórico vivido. Seu uso pedagógico é de captar imagem e na elaboração de conteúdo aprendido. Como possui uma função bem específica sua linguagem se limita a uma linguagem visual de imagens, escolhidas pelos alunos ou professor pode ter um diferente enfoque, que permite uma variedade na linguagem visual.";

                DescriptionsInMission1.DialogueWhenAcquired = "Uma máquina Polaroid! Nós podemos tirar muitas fotos com ela.";
                DescriptionsInMission1.InventoryDescription = "Máquina fotográfica utilizada para capturar imagens  de qualquer tipo de registro visual como pessoas, animais, espaços ou momentos que são impressas instantaneamente em papel.";
                DescriptionsInMission1.Geral = "Ferramenta que captura imagens e imprime fotografias instantâneas, para ser usada dentro ou fora da sala de aula.";
                DescriptionsInMission1.Com = "É possível visualizar as imagens de pessoas, animais, espaços e momentos em um suporte de papel, por exemplo, numa exposição de fotos nos ambientes da escola pelos alunos.";
                DescriptionsInMission1.Sobre = "As imagens podem ser estudadas a partir de diversas maneiras, seja pelo conteúdo retratado, ou formato escolhido, pelas cores, assuntos, intenção do fotógrafo, etc.";
                DescriptionsInMission1.Atraves = "É possível trabalhar com alunos para fotografar algum assunto e depois realizar uma exposição de suas fotos nos ambientes da escola.";

                //DescriptionsInMission1.StandardDescription = "Máquina fotográfica utilizada para capturar imagens  de qualquer tipo de registro visual como pessoas, animais, espaços ou momentos que são impressas instantaneamente em papel.";
                DescriptionsInMission1.StandardDescription = "Os alunos podem usar isso para tirar fotos durante a aula, só não sei se tem muito o que fotografar na sala de aula.";

                DescriptionsInMission1.FirstMomentDescription = "Pode ser usada para tirar foto de algum pássaro.";
                DescriptionsInMission1.SecondMomentDescription = "Depois dos alunos realizarem uma pesquisa sobre pássaros podem fazer várias fotos de diferentes espécies.";
                DescriptionsInMission1.ThirdMomentDescription = "Com a câmera polaroid podemos incentivar os alunos para fazerem diferentes registros sobre temáticas variadas.";

                DescriptionsInMission2.FirstMomentDescription = "Não há locais na sala que pudessem ser fotografados com a temática trabalhada pelo professor.";
                DescriptionsInMission2.SecondMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                DescriptionsInMission2.ThirdMomentDescription = DescriptionsInMission2.FirstMomentDescription;
                break;
            case ItemName.FotografiaPassaro:
                FriendlyName = "Fotografia";
                Description = "É a foto de um pássaro local pousado em um banco de escola";
                FullDescription = "É um recurso imagético de expressão de amplo uso. Seu uso pedagógico serve para ilustrar, apresentar e demonstrar conteúdo, permitir a elaboração de novos aprendizados e ampliação de conhecimento. O estudante usa para expressar ideias, ilustrar a elaboração dos conhecimentos aprendidos, apresentar e socializar com o grande grupo. Sua prática social é a de guardar recordações, registrar momentos e locais, denunciar fatos e divulgação. Esta mídia de linguagem não verbal, serve perfeitamente para trabalhar com imagens específicas.";

                DescriptionsInMission1.DialogueWhenAcquired = "Uma foto! Ela pode ser útil para a aula, só preciso pensar com cuidado o momento certo para usá-la!";
                DescriptionsInMission1.InventoryDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission1.Geral = "Imagem impressa de um pássaro no pátio.";
                DescriptionsInMission1.Com = "É um recurso interessante para ilustrar animais, imagens, fatos históricos, acontecimentos ou situações cotidianas. Com ela é possível analisar e entender o contexto ao qual as fotos foram tiradas.";
                DescriptionsInMission1.Sobre = "Refletir sobre como o registro fotográfico oferece mais do que a própria imagem em si, mas é uma ferramenta que comporta uma memória, sentimentos e percepções.";
                DescriptionsInMission1.Atraves = "Produzir registros fotográficos sobre os pássaros da região.";

                //DescriptionsInMission1.StandardDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission1.StandardDescription = "Uma foto! Ela pode ser útil para a aula, só preciso pensar com cuidado na forma que posso usar essa mídia!";
                DescriptionsInMission1.FirstMomentDescription = "Com a fotografia de pássaro os alunos podem observar e analisar suas características.";
                DescriptionsInMission1.SecondMomentDescription = "A fotografia do pássaro pode ajudar na elaboração da atividade.";
                DescriptionsInMission1.ThirdMomentDescription = "A fotografia mostra vários detalhes das carcterísticas físicas do pássaro.";
                UpgradeFrom.Add(ItemName.CameraPolaroid);
                break;
            case ItemName.TVComVHS:
                FriendlyName = "TV Escola VHS";
                Description = "Sem uma fita VHS vai ser difícil achar algo de interessante passando na TV";
                FullDescription = "É um aparelho de exibição de canais sintonizados por satélite e imagens conectados a um reprodutor VHS. Seu uso pedagógico pode ser através de programas que estejam passando ao vivo ou naquele momento, tende a motivar o aprendizado por conta do conteúdo audiovisual. Assim como o reprodutor VHS possui uma linguagem visual, podendo trabalhar em conjunto com ele, ou utilizando canais de televisão.";

                DescriptionsInMission1.DialogueWhenAcquired = "Uma TV! Com ela podemos conectar a TV Escola e ver as  programações diárias ou assistir algum VHS com temas específicos da aula. Que maravilha! ";
                DescriptionsInMission1.InventoryDescription = "Uma TV conectada a emissora TV Escola, com ela os alunos podem ver as  programações diárias ou assistir algum VHS com temas específicos da aula.";
                DescriptionsInMission1.Geral = "A TV é um sistema de recepção/reprodução de imagens e sons de programas televisivos, como o da TV Escola, por exemplo.";
                DescriptionsInMission1.Com = "Pode ser utilizada na sala de aula para mostrar vídeos, filmes e documentários para contribuir com os conteúdos trabalhados.";
                DescriptionsInMission1.Sobre = "É possível refletir sobre a televisão como um veículo de informação de massa, que pode atingir diversos lugares ao mesmo tempo. Além disso, discutir sobre a criticidade das informações que são passadas, bem como os pontos de vistas e a quais interesses essa mídia atende.";
                DescriptionsInMission1.Atraves = "Com o auxílio de equipamentos de gravação ou contato com emissoras é possível a criação de vídeos feitos pela turma sobre determinados assuntos, seja sobre o cotidiano escolar ou disciplinas específicas.";

                //DescriptionsInMission1.StandardDescription = "Sistema eletrônico de recepção/reprodução de imagens e sons de programas televisivos jornalísticos, esportivos, educacionais e ficcionais, gravados ou ao vivo.";
                DescriptionsInMission1.StandardDescription = "Uma TV! Eu posso achar um VHS para ter algo relacionado à aula para os alunos assistirem durante ela!";

                DescriptionsInMission1.FirstMomentDescription = "Um VHS da TV Escola sobre pássaros pode ser usado como material de pesquisa, é uma ótima opção para tratar o tema.";
                DescriptionsInMission1.SecondMomentDescription = "O VHS específico da TV Escola traz informções muito importantes sobre a temática.";
                DescriptionsInMission1.ThirdMomentDescription = "A exposição com essa mídia é bem completa. Os alunos podem usar a televisão com um VHS da TV Escola sobre pássaros para mostrar algumas partes do assunto pesquisado.";

                DescriptionsInMission2.FirstMomentDescription = "Um VHS da TV Escola sobre a Revolução Industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um VHS da TV Escola sobre o Trabalho Infantil pode fomentar a discussão sobre a temática.";
                DescriptionsInMission2.ThirdMomentDescription = "O VHS específico da TV Escola traz informções muito importantes sobre a temática.";

                DescriptionsInMission3.FirstMomentDescription = "Um VHS da TV Escola sobre regionalismo é uma ótima opção para tratar do tema.";
                DescriptionsInMission3.SecondMomentDescription = "Os alunos podem usar a televisão com um VHS da TV Escola sobre regionalismo para mostrar algumas partes do assunto pesquisado.";
                DescriptionsInMission3.ThirdMomentDescription = "Televisão conectada a TV Escola com programação e VHS específicos.";
                break;
            case ItemName.VHS:
                FriendlyName = "Fita VHS";
                Description = "Pássaros, nossos amigos penosos.";
                FullDescription = "É uma fitas em  formato VHS (Vídeo Home System) contendo um documentário curta metragem. Seu uso pedagógico é relacionado a apresentação de conteúdos pelo meio visual, contato com outras culturas sociais e linguísticas. Utiliza de uma linguagem audiovisual, que tende a ser mais interessante para os alunos.";

                DescriptionsInMission1.DialogueWhenAcquired = "Uma TV com VHS! Essa mídia vai prender a atenção dos alunos.";
                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) combinadas serão usadas para ver o documentário sobre pássaros.";
                DescriptionsInMission1.Geral = "O VHS capta e reproduz  vídeo e áudio, essas mídias combinadas permitem que o professor passe ofilme sobre pássaros.";
                DescriptionsInMission1.Com = "Assistir o filme para conhecer detalhessobre os diferentes tipos de pássaros.";
                DescriptionsInMission1.Sobre = "Analisar as diferenças de pássaros de cada região.";
                DescriptionsInMission1.Atraves = "Gravar ou fazer um vídeo dsobre o tema estudado.";

                DescriptionsInMission2.DialogueWhenAcquired = " Aqui! Achei um documentário sobre a Revolução Industrial.  Mas ele me parece ser bem longo… Não daria tempo de mostrar o documentário todo...";
                DescriptionsInMission2.InventoryDescription = "Essas mídias (TV-VHS) combinadas serão usadas para ver um documentário sobre a Revolução Industrial.";
                DescriptionsInMission2.Geral = "Consiste em um sistema de captação e reprodução de vídeo e áudio. Vai ser usado para ver o filme sobre a revolução industrial.";
                DescriptionsInMission2.Com = "Assistir o filme para conhecer detalhes da época.";
                DescriptionsInMission2.Sobre = "Analisar como as crianças eram exploradas a partir das imagens e informações.";
                DescriptionsInMission2.Atraves = "Junto com a câmera poderia gravar depoimentos de crianças contando sobre situações vivenciadas de exploração infantil, se conhecem, já viveram ou impressões do filme.";

                //DescriptionsInMission1.StandardDescription = "O VHS é a sigla para Video Home System, que consiste em um sistema de captação e reprodução de vídeo e áudio.";
                DescriptionsInMission1.StandardDescription = "Um VHS! Os alunos adoram assistir alguma coisa na aula!";

                DescriptionsInMission1.FirstMomentDescription = "O VHS sobre pássaros trazido pelo professor pode ser uma boa fonte de pesquisa dos alunos.";
                DescriptionsInMission1.SecondMomentDescription = "O documentário possui muitas informações importantes sobre o tema estudado.";
                DescriptionsInMission1.ThirdMomentDescription = "Os alunos podem escolher algumas partes do VHS para apresentar para a turma.";

                DescriptionsInMission2.FirstMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";
                DescriptionsInMission2.ThirdMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";


                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.CartazComColecaoDePenas:
                FriendlyName = "Cartaz com Coleção de Penas";
                Description = "Um cartaz com uma coleção de penas";
                FullDescription = "Um cartaz com uma coleção de penas";

                DescriptionsInMission1.DialogueWhenAcquired = "Minha nossa! Um cartaz feito por ornitólogo, avô de um aluno na escola, contendo penas de algumas espécies de pássaros da região.";
                DescriptionsInMission1.InventoryDescription = "Esse cartaz foi feito por ornitólogo, avô de um aluno na escola, contendo penas de algumas espécies de pássaros da região.";
                DescriptionsInMission1.Geral = "";
                DescriptionsInMission1.Com = "";
                DescriptionsInMission1.Sobre = "";
                DescriptionsInMission1.Atraves = "";

                DescriptionsInMission1.StandardDescription = "Cartaz feito por um ornitólogo, avô de um aluno da escola, contendo penas de algumas espécies de pássaros da região.";

                DescriptionsInMission1.FirstMomentDescription = "O cartaz com as penas é interessante, pois ilustra de forma real as penas de algumas espécies de pássaros.";
                DescriptionsInMission1.SecondMomentDescription = "Esse cartaz pode ser útil para ilustrar as características dos pássaros e ajudar na atividade de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "Esse cartaz pode ser usado na apresentação por conter penas de algumas espécies de pássaros da região.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.LivroDidatico:
                FriendlyName = "Livro Didático";
                Description = "Para fazer atividades, aprender e revisar o conteúdo. Um clássico!";
                FullDescription = "É um livro de cunho pedagógico composto de conteúdo do currículo escolar. Pode ser usado para pesquisa e resolução de exercícios. A linguagem presente no livro didático é visual e principalmente textual, trazendo o conteúdo de forma intercalada entre textos e imagens.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um livro didático! Com ele os alunos podem fazer exercícios e ler sobre os conteúdos estudados.";
                DescriptionsInMission1.InventoryDescription = "É um livro impresso de cunho pedagógico composto de exercícios, textos e imagens do conteúdo estudado em sala que acompanha o currículo escolar.";
                DescriptionsInMission1.Geral = "Livro impresso fornecido pela escola que traz informações sobre todos os conteúdos das disciplinas do ano letivo em questão.";
                DescriptionsInMission1.Com = "Normalmente utilizado como leitura do material didático complementar para as disciplinas específicas.";
                DescriptionsInMission1.Sobre = "Pode ser estudado o tratamento dado pelos autores aos conteúdos e recursos como imagens, gráficos e diversos gêneros textuais, bem como as questões transversais são abordadas neles.";
                DescriptionsInMission1.Atraves = "Estudantes podem produzir seus próprios livros didáticos mediados pelos professores.";

                //DescriptionsInMission1.StandardDescription = "É um livro impresso de cunho pedagógico composto de exercícios, textos e imagens do conteúdo estudado em sala que acompanha o currículo escolar.";
                DescriptionsInMission1.StandardDescription = "Um livro didático! Os alunos aprendem tanto com essa mídia! Pena que alguns não gostam tanto de ler...";

                DescriptionsInMission1.FirstMomentDescription = "Um grande aliado dos alunos e professores por apresentar o conteúdo com imagens, conteúdos sobre o tema, exercícios e linguagem apropriada.";
                DescriptionsInMission1.SecondMomentDescription = "O livro didátio traz informações em forma de imagem, textos e exercícios sobre todos os conteúdos das disciplinas.";
                DescriptionsInMission1.ThirdMomentDescription = "Apresenta conteúdo por imagem e texto de forma mais específica. Pode ser utilizado trechos do livro para complementar a exposição.";

                DescriptionsInMission2.FirstMomentDescription = "O Livro didático apresenta algumas informações mais gerais da história da época, dando acesso a todos os alunos.";
                DescriptionsInMission2.SecondMomentDescription = "Os conteúdos sistematizados do livro didático podem auxiliar bastante os alunos durante a discussão, por ser uma linguagem mais acessível.";
                DescriptionsInMission2.ThirdMomentDescription = "O livro não é muito interessante para produzir algo nesse momento da aula.";

                DescriptionsInMission3.FirstMomentDescription = "O Livro didático apresenta algumas informações mais gerais sobre o assunto.";
                DescriptionsInMission3.SecondMomentDescription = "O livro didático contêm muitas informações e é acessível aos alunos.";
                DescriptionsInMission3.ThirdMomentDescription = "O livro didático já faz parte do dia a dia da escola das crianças.";
                break;
            case ItemName.LivroIlustrado:
                FriendlyName = "Livro Ilustrado";
                Description = "Uaaaaau, quantas figuras lindas! Os alunos vão adorar!";
                FullDescription = "É um livro belamente ilustrado com desenhos, gravuras e fotos coloridas concatenadas a textos explicativos. Usa uma linguagem visual, que exprime certos conteúdos mais facilmente.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um livro ilustrado! Os alunos adoram ver as imagens que complementam os textos.";
                DescriptionsInMission1.InventoryDescription = "É um livro com imagens que ajudam o aluno a entender o conteúdo da aula. As ilustrações podem ser usadas nas aulas de forma a complementar outras mídias mais básicas.";
                DescriptionsInMission1.Geral = "Livro com textos e imagens que ajudam o aluno a entender o conteúdo da aula.";
                DescriptionsInMission1.Com = "As ilustrações do livro junto ao conteúdo podem ser usadas nas aulas de forma a complementar o livro didático e outras mídias mais básicas.";
                DescriptionsInMission1.Sobre = "Refletir sobre a diferença entre os livros ilustrados e os livros somente escritos no que se refere ao processo de imaginação. Analisar como imagens, textos, texturas, cores e volumes são combinados para contar as histórias.";
                DescriptionsInMission1.Atraves = "Construção de um livro coletivo com a turma com ilustrações ou fotos criadas por eles.";

                //DescriptionsInMission1.StandardDescription = "É um livro com imagens que ajudam o aluno a entender o conteúdo da aula. As ilustrações podem ser usadas nas aulas de forma a complementar outras mídias mais básicas.";
                DescriptionsInMission1.StandardDescription = "Um livro ilustrado! Os alunos adoram coisas coloridas como esse livro!";

                DescriptionsInMission1.FirstMomentDescription = "Traz informações e imagens ilustrando as diversas características dos pássaros, sendo um bom material de pesquisa.";
                DescriptionsInMission1.SecondMomentDescription = "O livro ilustrado possui além de texto, varias imagens sobre os conteúdos trabalhados em aula, auxiliando nas atividades de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "Pode ser usado para apresentar as características gerais dos pássaros e mostrar suas ilustrações para complementar a apresentação.";

                DescriptionsInMission2.FirstMomentDescription = "O Livro ilustrado possui muitas informações e imagens da época de forma didática e atrativa aos alunos.";
                DescriptionsInMission2.SecondMomentDescription = "Os conteúdos do livro, além das diversas ilustrações, são ótimos para gerar discussões.";
                DescriptionsInMission2.ThirdMomentDescription = "O livro não é muito interessante para produzir algo nesse momento da aula.";

                DescriptionsInMission3.FirstMomentDescription = "O livro ilustrado traz informações e imagens ilustrando as diversas regiões brasileiras e sendo um bom material de pesquisa.";
                DescriptionsInMission3.SecondMomentDescription = "O livro ilustrado traz imagens e informações importantes para a pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "O livro ilustrado já faz parte do dia a dia da escola das crianças.";

                UpgradeFrom.Add(ItemName.LivroDidatico);
                break;
            case ItemName.QuadroNegro:
                FriendlyName = "Quadro Negro";
                Description = "Hmmm, é bom lembrar que tanto o professor quanto os alunos podem usar essa mídia. O velho companheiro dos professores.";
                FullDescription = "É um espaço plano reutilizável geralmente riscado com giz branco para transmissão de conteúdo.  Exposição da matéria para grande quantidade de estudantes. Possibilita ao aluno expor suas elaborações sobre os conteúdos e experiências. Tanto o professor quanto o aluno podem utilizá-lo de forma visual, expondo o assunto trabalhado de forma expositiva e/ou colaborativa.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um quadro negro! Essa mídia é tão clássica e versátil! Os professores adoram ela!";
                DescriptionsInMission1.InventoryDescription = "Superfície reutilizável geralmente riscada com giz branco para exposição do conteúdo ou usado de forma colaborativa entre professor e estudante.";
                DescriptionsInMission1.Geral = "Superfície negra em que, tanto o professor quanto os alunos, podem escrever anotações sobre a aula.";
                DescriptionsInMission1.Com = "Permite a escrita e a correção pelo professor durante as explicações e sistematizações dos conteúdos.";
                DescriptionsInMission1.Sobre = "Reflexão sobre a história da educação e da representação do professor como centro do processo de aprendizagem.";
                DescriptionsInMission1.Atraves = "A utilização do quadro negro pelos alunos, de forma a quebrar a naturalização dos processos verticalizantes da educação entre aluno-professor.";

                //DescriptionsInMission1.StandardDescription = "Superfície reutilizável geralmente riscada com giz branco para exposição do conteúdo ou usado de forma colaborativa entre professor e estudante.";
                DescriptionsInMission1.StandardDescription = "Um quadro negro! Essa mídia é tão clássica! Os professores o adoram!";

                DescriptionsInMission1.FirstMomentDescription = "O quadro é a mídia mais comumente ultilizada em aulas expositivas, podendo ser utilizada pelo professor para  exposição e sistematização de conteúdos.";
                DescriptionsInMission1.SecondMomentDescription = "Com o quadro negro os alunos podem criar tabelas e esquemas para a sistematização do conteúdo da aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Os alunos podem usar o quadro negro como um recurso adicional ao apresentar suas falas durante a exposição.";

                DescriptionsInMission2.FirstMomentDescription = "O quadro negro é bastante útil para apresentações e sistematizações dos conteúdos, mas ele não é uma novidade para os alunos.";
                DescriptionsInMission2.SecondMomentDescription = "O quadro negro é bastante útil para a mediação das discussões, mas ele não é uma novidade para os alunos.";
                DescriptionsInMission2.ThirdMomentDescription = "O quadro negro, embora muito importante, acaba gerando uma centralidade de quem está escrevendo. A intenção da aula é a interação entre os indivíduos.";

                DescriptionsInMission3.FirstMomentDescription = "O quadro negro pode ser usado para exposição, anotações do conteúdo ou usado de forma colaborativa entre professor e estudante.";
                DescriptionsInMission3.SecondMomentDescription = "O quadro negro pode ser usado na criação de esquemas e sínteses das pesquisas dos alunos.";
                DescriptionsInMission3.ThirdMomentDescription = "O quadro negro é muito versátil e pode ser utilizado para a síntese dos conteúdos da aula.";
                break;
            case ItemName.TVComVHSPassaros:
                FriendlyName = "TV com VHS sobre pássaros";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.DialogueWhenAcquired = "Uma TV com VHS! Essa mídia vai prender a atenção dos alunos.";
                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) combinadas serão usadas para ver o documentário sobre pássaros.";
                DescriptionsInMission1.Geral = "O VHS capta e reproduz  vídeo e áudio, essas mídias combinadas permitem que o professor passe ofilme sobre pássaros.";
                DescriptionsInMission1.Com = "Assistir o filme para conhecer detalhessobre os diferentes tipos de pássaros.";
                DescriptionsInMission1.Sobre = "Analisar as diferenças de pássaros de cada região.";
                DescriptionsInMission1.Atraves = "Gravar ou fazer um vídeo dsobre o tema estudado.";

                DescriptionsInMission1.StandardDescription = "Uma TV com VHS! Os alunos adoram assistir alguma coisa na aula!";
                DescriptionsInMission1.FirstMomentDescription = "O VHS sobre pássaros trazido pelo professor pode ser uma boa fonte de pesquisa dos alunos.";
                DescriptionsInMission1.SecondMomentDescription = "O documentário possui muitas informações importantes sobre o tema estudado.";
                DescriptionsInMission1.ThirdMomentDescription = "Os alunos podem escolher algumas partes do VHS para apresentar para a turma.";
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.Cartazes:
                FriendlyName = "Cartazes";
                Description = "Um cartaz em branco, perfeito para criar algo durante a aula";
                FullDescription = "É uma litografia. Geralmente feitos pelos próprios estudantes, utilizados para atividades pedagógicas individuais ou em grupo para proporcionar a produção coletiva, apresentação e socialização do que foi produzido com o grupo escolar, produção de trabalhos escolares que permitem a pesquisa, produção textual, produção artística, expressar opiniões, elaboração dos conteúdos aprendidos e exposição visual de conteúdo pedagógico. Dependendo do uso pode ter um viés mais textual ou mais imagético, mas voltado para o lado visual, dado a sua possibilidade de exposição tende a ter um melhor uso para imagens trabalhadas pela turma.";

                DescriptionsInMission1.DialogueWhenAcquired = "Minha nossa, um cartaz! São tantos os jeitos que se pode usar um cartaz em sala de aula!";
                DescriptionsInMission1.InventoryDescription = "Anúncio ou aviso de dimensões variadas, geralmente ilustrado com desenhos ou fotografias, apropriado para ser afixado em lugares públicos.";
                DescriptionsInMission1.Geral = "Papel ou cartolina em branco, que pode ser usado pelos alunos durante uma atividade de aula.";
                DescriptionsInMission1.Com = "Mídia impressa que pode ser utilizada para passar informações, avisos ou apresentar trabalhos.";
                DescriptionsInMission1.Sobre = "É possível fazer uma análise crítica dos tipos que encontramos dentro e fora da escola, ou então fazer comparações dos que se referem ao mesmo tema.";
                DescriptionsInMission1.Atraves = "Os alunos podem utilizar essa mídia como conclusão de um trabalho, por ser de comunicação visual para socializar uma produção e trabalhar com a escrita e o desenho (ou com textos e imagens).";

                //DescriptionsInMission1.StandardDescription = "Anúncio ou aviso de dimensões variadas, geralmente ilustrado com desenhos ou fotografias, apropriado para ser afixado em lugares públicos.";
                DescriptionsInMission1.StandardDescription = "Minha nossa, um cartaz! São tantos os jeitos que se pode usar um cartaz em sala de aula!";

                DescriptionsInMission1.FirstMomentDescription = "Papel ou cartolina em branco, que pode ser usado pelo professor e pelos alunos.";
                DescriptionsInMission1.SecondMomentDescription = "O cartaz é uma mídia interessante para atividades práticas dos alunos por conta da versatilidade. Proporciona a escrita, colagem de imagens, desenho, etc.";
                DescriptionsInMission1.ThirdMomentDescription = "Os alunos podem elaborar tabelas e esquemas para a exposição do conteúdo da aula, além da colar imagens para ilustrar.";

                DescriptionsInMission2.FirstMomentDescription = "O cartaz possibilita a apresentação dos conteúdos para os alunos, mas alem de ele não ser uma novidade para eles não possibilita muita versatilidade no trabalho de exposição do professor.";
                DescriptionsInMission2.SecondMomentDescription = "O cartaz tem muitas possibilidades em meio a construção de uma apresentação ou sistematização, mas esse é um momento de discussões, talvez gere dispersão e seja melhor utilizado em outro momento.";
                DescriptionsInMission2.ThirdMomentDescription = "O cartaz é uma mídia bastante conhecida pelos alunos na elaboração de atividades, eles já estão acostumados com ela e sabem como utilizá-la, por isso pode ser uma boa escolha.";

                DescriptionsInMission3.FirstMomentDescription = "Papel ou cartolina em branco, que pode ser usado pelo professor e pelos alunos.";
                DescriptionsInMission3.SecondMomentDescription = "Papel ou cartolina em branco, que pode ser usado pelo professor e pelos alunos durante a aula para atividades de apresentação.";
                DescriptionsInMission3.ThirdMomentDescription = "O cartaz é uma mídia bastante conhecida pelos alunos na elaboração de atividades de síntese.";
                break;
            case ItemName.Mapa:
                FriendlyName = "Mapa";
                Description = "Um cartaz de um mapa";
                FullDescription = "Um cartaz de um mapa";

                DescriptionsInMission1.DialogueWhenAcquired = "Mapa da região atualizado, contendo uma representação cartográfica plana, em escala reduzida, de toda a superfície.";
                DescriptionsInMission1.InventoryDescription = "Uma representação gráfica e métrica de uma porção de território sobre uma superfície bidimensional, geralmente plana.";
                DescriptionsInMission1.Geral = "Representação gráfica e métrica de uma porção de território.";
                DescriptionsInMission1.Com = "É possível ser utilizado durante as aulas como recurso metodológico, principalmente em disciplinas de história e geografia, para auxiliar nas explicações do professor.";
                DescriptionsInMission1.Sobre = "Refletir sobre o processo cartográfico de elaboração de mapas durante diferentes tempos históricos.";
                DescriptionsInMission1.Atraves = "É possível construir um pequeno mapa do bairro ou cidade com os alunos.";

                DescriptionsInMission1.StandardDescription = "Um mapa-mundi atualizado, contendo uma representação cartográfica plana, em escala reduzida, de toda a superfície do planeta Terra.";
                DescriptionsInMission1.FirstMomentDescription = "O Mapa, por conta da leitura associada a imagens, pode ser interessante para uma pesquisa sobre os locais e regiões de onde vivem.";
                DescriptionsInMission1.SecondMomentDescription = "O Mapa pode auxiliar na atividade de sistematização.";
                DescriptionsInMission1.ThirdMomentDescription = "O mapa pode ser um bom aliado na exposição dos alunos. Auxiliando a demonstrar os locais e regiões onde os pássaros vivem.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.Caderno:
                FriendlyName = "Caderno";
                Description = "Na minha época não tinha tantas cores de canetas e adesivos divertidos…";
                FullDescription = "É um artefato histórico atualmente produzidos em papel. Utilizadas para a anotação, expressão textual e armazenamento de informações mais relevantes para os estudantes durante as aulas, serve também de registro histórico daquela época. É um instrumento de comunicação entre o professor e o estudante, de desenvolver a coordenação motora fina, cognitiva, raciocínio lógico, de audição, de visão e também como instrumento de avaliação e de caligrafia. Possui uma linguagem muito versátil, já que pode trabalhar com a forma textual, tanto verbal, sendo ditado pelo professor, como escrita, copiando o que está no quadro, ou em alguma outra mídia. Além do uso de textos, pode-se usar de elementos visuais.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um caderno! Alguns alunos adoram decorar e deixar suas anotações mais coloridas.";
                DescriptionsInMission1.InventoryDescription = "Conjunto de folhas de papel agrupadas num formato portátil, usado para registro, estudo ou anotações escritas, desenhos ou colagens com uso de diversos recursos e materiais.";
                DescriptionsInMission1.Geral = "Material do aluno para fazer anotações ou registro de atividades de estudo.";
                DescriptionsInMission1.Com = "Ferramenta de uso pessoal do aluno para registrar as informações referentes aos conteúdos trabalhados em sala de aula, realizar exercícios ou copiar informações do quadro.";
                DescriptionsInMission1.Sobre = "Refletir sobre a importância do registro pessoal e como os cadernos são uma mídia histórica utilizada tanto por alunos quanto professores.";
                DescriptionsInMission1.Atraves = "É possível construir um caderno da turma, com registros ou resumos dos conteúdos trabalhados em determinadas disciplinas para consultas.";

                //DescriptionsInMission1.StandardDescription = "Conjunto de folhas de papel agrupadas num formato portátil, usado para registro, estudo ou anotações escritas, desenhos ou colagens com uso de diversos recursos e materiais para tanto.";
                DescriptionsInMission1.StandardDescription = "Um caderno! Alguns alunos adoram decorar eles e deixar eles lindíssimos! Pena que outros alunos não gostam tanto dessa mídia…";

                DescriptionsInMission1.FirstMomentDescription = "Conjunto de folhas de papel em branco, uma ferramenta de uso pessoal do aluno.";
                DescriptionsInMission1.SecondMomentDescription = "É um ótimo aliado dos alunos para registro e sistematização dos conteúdos. É de fácil acesso e os usuários podem desenvolver esquemas e tabelas sobre o tema.";
                DescriptionsInMission1.ThirdMomentDescription = "O caderno pode ser utilizado como uma mídia de registro ou leitura de conteúdos.";

                DescriptionsInMission2.FirstMomentDescription = "Como o principal objetivo da aula nesse momento é expor os conteúdos, só o caderno não seria suficiente.";
                DescriptionsInMission2.SecondMomentDescription = "O caderno é bom para anotar pontos importantes da discussão, mas o processo de anotar enquanto prestam atenção pode deixar os alunos um pouco confusos.";
                DescriptionsInMission2.ThirdMomentDescription = "O caderno é potente na realização de atividades de síntese, porém é bastante individual e não proporciona muita interação entre os colegas.";

                DescriptionsInMission3.FirstMomentDescription = "O caderno pode ser usado para registro, estudo ou anotações escritas, desenhos ou colagens com uso de diversos recursos e materiais.";
                DescriptionsInMission3.SecondMomentDescription = "O caderno pode ser usado para registro, estudo ou anotações escritas, desenhos ou colagens com uso de diversos recursos e materiais.";
                DescriptionsInMission3.ThirdMomentDescription = "O caderno é útil para escrever esquemas e sínteses sobre o assunto.";
                break;
            case ItemName.Jornais:
                FriendlyName = "Jornais";
                Description = "A inflação subiu de novo… Desse jeito onde será que as coisas vão parar?";
                FullDescription = "Esta mídia teve seu início no formato impresso. É utilizada para transmitir informações, contém um gênero textual amplamente estudado em sala de aula, expressam diferentes perspectivas políticas, econômicas, culinárias, de lazer, entre outras que são produzidas socialmente, registram os acontecimentos históricos da época.  É acessível somente a pessoas letradas. Sua prática pedagógica serve para introduzir os estudantes nos conhecimentos da alfabetização, demonstra a diferença de produção textual na perspectiva da diversidade de opiniões, modos, vertentes no ensino e aprendizagem de conteúdos. Assim como os livros didáticos, traz texto escrito complementado por imagens, porém com um estilo de escrita geralmente diferente.";

                DescriptionsInMission1.DialogueWhenAcquired = "Jornais! Eles são mais do que notícias aglomeradas, quando se trata de usar eles na sala de aula!";
                DescriptionsInMission1.InventoryDescription = "São um meio de comunicação impresso e um produto derivado do conjunto de atividades denominado jornalismo.";
                DescriptionsInMission1.Geral = "Textos e imagens informativas em páginas organizadas juntas que pode ser usado como material de consulta, ou ainda, de recorte para atividades.";
                DescriptionsInMission1.Com = "Essa mídia impressa de circulação social traz as últimas notícias diariamente. Os alunos podem ler pela variedade de conteúdos atuais.";
                DescriptionsInMission1.Sobre = "Os conteúdos dessas mídias trazem vários pontos de vista, podendo ser utilizados para análise crítica desses diferentes olhares para um mesmo fato.";
                DescriptionsInMission1.Atraves = "Os alunos podem conhecer os diferentes gêneros textuais que compõem essa mídia e reproduzi-los através de uma versão escolar.";

                //DescriptionsInMission1.StandardDescription = "São um meio de comunicação impresso e um produto derivado do conjunto de atividades denominado jornalismo.";
                DescriptionsInMission1.StandardDescription = "Jornais! Eles são mais do que notícias aglomeradas, quando se trata de usar eles na sala de aula!";

                DescriptionsInMission1.FirstMomentDescription = "Os jornais podem possuir informações sobre pássaros.";
                DescriptionsInMission1.SecondMomentDescription = "Esses jornais contém imagens e textos que podem ser resignificados, recortados ou utilizados pelos alunos na elaboração da síntese.";
                DescriptionsInMission1.ThirdMomentDescription = "Jornais podem auxiliar na exposição por conter imagens e textos em linguagem mais simples. Mas é fundamental encontrar um jornal com reportagem sobre o tema de estudo.";

                DescriptionsInMission2.FirstMomentDescription = "Os jornais podem possuir informações sobre a Revolução Industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Os jornais podem possuir informações sobre o trabalho infantil e levantar questões para a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "Esses jornais contém imagens e textos que podem ser resignificados e que, depois podem ser recortados ou utilizados pelos alunos na elaboração da síntese.";

                DescriptionsInMission3.FirstMomentDescription = "Os jornais trazem algumas informações sobre palavras regionais em suas entrevistas e notícias.";
                DescriptionsInMission3.SecondMomentDescription = "Os jornais trazem informações sobre as palavras regionais e os costumes das diferentes regiões brasileiras, pode ser usado para a apresentação da pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "Os jornais trazem algumas informações sobre regionalismo em suas colunas.";
                break;
            case ItemName.JornaisERevistas:
                FriendlyName = "Jornais e Revistas";
                Description = "Uma coleção de jornais e revistas";
                FullDescription = "Uma coleção de jornais e revistas";

                DescriptionsInMission1.DialogueWhenAcquired = "Jornais e revistas! Podem ser muito úteis, principalmente nos momentos de pesquisa e atividades práticas.";
                DescriptionsInMission1.InventoryDescription = "Material impresso contendo textos e imagens com diversos assuntos. Podem conter entrevistas, comentários, matérias e propagandas.";
                DescriptionsInMission1.Geral = "";
                DescriptionsInMission1.Com = "";
                DescriptionsInMission1.Sobre = "";
                DescriptionsInMission1.Atraves = "";

                //DescriptionsInMission1.StandardDescription = "Material impresso contendo textos e imagens com diversos assuntos, dependendo do tipo de revista. Podem conter entrevistas, comentários, matérias e propagandas.";
                DescriptionsInMission1.StandardDescription = "Revistas! Justas com o jornal podem ser muito úteis, principalmente nos momentos de atividade de aprendizagem da aula.";

                DescriptionsInMission1.FirstMomentDescription = "Uma coleção de jornais e revistas com essa temática pode complementar a aula expositiva do professor com pequenos textos e ilustrações, com linguagem mais simples e de fácil acesso.";
                DescriptionsInMission1.SecondMomentDescription = "Se for possível encontra matérias específicas sobre os pássaros, se torna bastante rico para a utilização dos alunos em atividades dentro da sala de aula.";
                DescriptionsInMission1.ThirdMomentDescription = "Os jornais e, principalmente, as revistas sobre pássaros trazidas pelo professor podem ser aliadas importantes durante a sistematização de conteúdos. Mas não falam muito sobre as questões de regionalidade.";

                DescriptionsInMission2.FirstMomentDescription = "Uma coleção de jornais e revistas trazidas pelo professor com essa temática pode ajudar os alunos na pesquisa.";
                DescriptionsInMission2.SecondMomentDescription = "A coleção de jornais e revistas sobre pássaros pode auxiliar os alunos na atividade de sistematização.";
                DescriptionsInMission2.ThirdMomentDescription = "Essas mídias podem complementar a aula expositiva com pequenos textos e ilustrações.";

                DescriptionsInMission3.FirstMomentDescription = "As revistas e jornais possuem diversas entrevistas, matérias e notícias onde se é possível encontrar regionalismo.";
                DescriptionsInMission3.SecondMomentDescription = "As revistas e jornais possuem diversas entrevistas, matérias e notícias onde se é possível encontrar regionalismo, podendo servir de exemplo para a exposição do tema.";
                DescriptionsInMission3.ThirdMomentDescription = "As revistas e jornais trazem em suas colunas e entrevistas diversos exemplos de regionalismos.";
                UpgradeFrom.Add(ItemName.Jornais);
                break;
            case ItemName.Retroprojetor:
                FriendlyName = "Retroprojetor";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Aparelho óptico utilizado para a projeção de imagens  e textos por meio de transparências.";

                DescriptionsInMission2.DialogueWhenAcquired = "Um retroprojetor! Que maravilha! Agora eu só preciso dos slides para usar com ele! Mas onde posso achar?";
                DescriptionsInMission2.Geral = "Máquina que projeta em uma parede uma imagem, texto e gráficos.";
                DescriptionsInMission2.Com = "Esse recurso pode exibir imagens, textos impressos ou escritos com caneta hidrográfica em uma transparência.";
                DescriptionsInMission2.Sobre = "Refletir sobre os assuntos abordados com o auxílio do retroprojetor.";
                DescriptionsInMission2.Atraves = "Os estudantes podem fazer diferentes textos, e apresentá-los ao grupo.";

                //DescriptionsInMission1.StandardDescription = "Aparelho óptico utilizado para a projeção de imagens por meio de transparências.";
                DescriptionsInMission1.StandardDescription = "Um retroprojetor! Que maravilha! Agora eu só preciso dos slides para usar com ele! Mas onde posso achar elas?";

                DescriptionsInMission2.StandardDescription = DescriptionsInMission1.StandardDescription;
                DescriptionsInMission2.FirstMomentDescription = "O retroprojetor é uma ótima mídia, se aliada com bons slides.";
                DescriptionsInMission2.SecondMomentDescription = "O retroprojetor é uma ótima mídia, se aliada com bons slides.";
                DescriptionsInMission2.ThirdMomentDescription = "O retroprojetor é uma ótima mídia, se aliada com bons slides. Os alunos podem produzir sínteses do contéudo e depois apresentar para turma.";
                break;
            case ItemName.RetroprojetorSlideMapa:
                FriendlyName = "Retroprojetor com Slide e  Mapa ";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Transparência contendo um mapa na época da revolução industrial, que combinadas com um retroprojetor, podem ser refletidas na parede.";

                DescriptionsInMission2.DialogueWhenAcquired = "Um mapa vai ser muito útil para explicar mais sobre a Revolução Industrial!";
                DescriptionsInMission2.Geral = "Transparência, contendo um mapa da época, que combinadas com um retroprojetor, podem ser refletidas na parede.";
                DescriptionsInMission2.Com = "É possível ser utilizado durante as aulas como recurso metodológico, principalmente em disciplinas de história e geografia, para auxiliar nas explicações do professor.";
                DescriptionsInMission2.Sobre = "Refletir sobre o processo da Revolução Industrial em espaços e contextos diferentes.";
                DescriptionsInMission2.Atraves = "É possível construir um pequeno mapa, dando ênfase em lugares e acontecimentos marcantes daquele momento.";

                //DescriptionsInMission2.StandardDescription = "Transparência, contendo um mapa mundi, que combinadas com um retroprojetor, podem ser refletidas na parede.";
                DescriptionsInMission2.StandardDescription = "Um slide com mapa para mim usar com o retroprojetor! Um mapa vai ser muito útil para explicar mais sobre a revolução industrial!";

                DescriptionsInMission2.FirstMomentDescription = "O slide com mapa pode ser utilizado para ilustrar as regiões onde ocorreu a Revolução Industrial durante a explicação do professor, tornando mais rico o aprendizado do aluno.";
                DescriptionsInMission2.SecondMomentDescription = "Esse slide, embora trate de questões menos específicas, pode ser utilizado durante a  discussão para localizar os alunos sobre o tema e os locais mencionados durante a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de uma atividade de síntese sobre o trabalho no período estudado e na atualidade.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.RetroprojetorSlideLinhaTempo:
                FriendlyName = "Retroprojetor com Slide com Linha do Tempo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Slide contendo uma linha do tempo sobre as condições do trabalho desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";

                DescriptionsInMission2.DialogueWhenAcquired = "Essa página contém uma linha do tempo sobre a Revolução Industrial!";
                DescriptionsInMission2.Geral = "Transparência contendo uma linha do tempo sobre as condições do trabalho desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";
                DescriptionsInMission2.Com = "É possível ser utilizado durante as aulas como recurso metodológico, principalmente em disciplinas de história e geografia, para auxiliar nas explicações do professor.";
                DescriptionsInMission2.Sobre = "Refletir sobre as datas utilizadas como marco de fatos históricos e o entendimento desses marcos como um processo que não se inicia e termina em uma data específica, mas sim em um período.";
                DescriptionsInMission2.Atraves = "Elaboração de linhas do tempo com a turma sobre determinados assuntos, que podem ser escolhidos por eles ou pelo professor.";

                //DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre as condições do trabalho desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";
                DescriptionsInMission2.StandardDescription = "Um slide com uma linha do tempo para mim usar com o retroprojetor! Uma linha do tempo dessas vai ser muito útil para explicar mais sobre a revolução industrial!";

                DescriptionsInMission2.FirstMomentDescription = "O slide com linha do tempo ajuda na compreensão dos fatos trabalhados, de forma esquemática, auxiliando durante a explicação do professor.";
                DescriptionsInMission2.SecondMomentDescription = "Esse slide, embora trate de questões mais gerais, pode ser utilizado para ajudar na localização temporal dos alunos durante a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de um trabalho de síntese  sobre o trabalho no período estudado e na atualidade.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.RetroprojetorSlideCicloTrabalho:
                FriendlyName = "Retroprojetor com Slide com Ciclo do trabalho";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Transparência contendo uma linha do tempo sobre o ciclo do trabalho infantil desde a Revolução Industrial, que combinadas com o retroprojetor, são refletidas na parede.";

                DescriptionsInMission2.DialogueWhenAcquired = "Essa página contém um bom texto sobre o ciclo do trabalho infantil na Revolução Industrial. Pode ser útil para a aula de hoje.";
                DescriptionsInMission2.Geral = "Transparência contendo uma linha do tempo sobre o ciclo do trabalho infantil desde a  revolução industrial.";
                DescriptionsInMission2.Com = "Pode ser utilizada como recurso metodológico para aulas expositivas ou dialogadas sobre a temática do trabalho infantil.";
                DescriptionsInMission2.Sobre = "Reflexão sobre os processos de exploração infantil no mundo do trabalho e quais as mudanças desde a criação do Estatuto da Criança e do Adolescente.";
                DescriptionsInMission2.Atraves = "Elaborar uma pesquisa com a turma referente a temática, para saber se nos dias atuais ainda é possível encontrar menores de idade trabalhando.";

                //DescriptionsInMission2.StandardDescription = "Transparência contendo uma linha do tempo sobre o ciclo do trabalho infantil desde a  revolução industrial, que combinadas com o retroprojetor, são refletidas na parede.";
                DescriptionsInMission2.StandardDescription = "Um slide com um ciclo do trabalho infantil para mim usar com o retroprojetor! O ciclo do trabalho infantil vai ser muito útil para explicar mais sobre a revolução industrial!";

                DescriptionsInMission2.FirstMomentDescription = "Esse slide é específico sobre o Trabalho Infantil. Nesse momento o professor pretente fazer uma exposição sobre as características mais gerais da Revolução Industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um slide com informações sobre o ciclo do Trabalho Infantil pode ser usado para iniciar discussões sobre os tópicos específicos trabalhados nesse momento.";
                DescriptionsInMission2.ThirdMomentDescription = "A atenção dos alunos está voltada para a realização de um trabalho de síntese sobre o trabalho no período estudado e na atualidade.";
                UpgradeFrom.Add(ItemName.Retroprojetor);
                break;
            case ItemName.CartazComCanetas:
                FriendlyName = "Cartaz com canetas coloridas";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Um conjunto de canetas coloridas junto de cartazes, geralmente utilizados para trabalhos expositivos, atividades de síntese e sistematização de conteúdos.";

                DescriptionsInMission2.DialogueWhenAcquired = "Um cartaz com canetas coloridas! Podemos usar essa recurso nos momentos de atividades práticas e de exposição.";
                DescriptionsInMission2.Geral = "Instrumentos para fazer desenhos e textos coloridos em cartazes.";
                DescriptionsInMission2.Com = "É possível construir cartazes de sistematização de conteúdos ou informativos para os ambientes escolares e/ou fora da escola.";
                DescriptionsInMission2.Sobre = "Refletir sobre os tipos de cartazes que encontramos em nosso dia-a-dia e quais as estratégias utilizadas para que alcancem o maior número de visualizações possível.";
                DescriptionsInMission2.Atraves = "Elaboração de uma oficina de cartazes com os alunos sobre uma determinada temática, podendo ser exposto dentro e fora da escola.";

                //DescriptionsInMission2.StandardDescription = "Um conjunto de canetas coloridas junto de cartazes, geralmente utilizados para trabalhos expositivos.";
                DescriptionsInMission2.StandardDescription = "Um cartaz com canetas coloridas! Isso pode ser muito útil, principalmente nos momentos de atividade de aprendizagem!";

                DescriptionsInMission2.FirstMomentDescription = "Papel ou cartolina em branco e canetas coloridas que podem ser usados pelo professor e alunos.";
                DescriptionsInMission2.SecondMomentDescription = "Papel ou cartolina em branco e canetas coloridas que podem ser usados pelo professor e alunos.";
                DescriptionsInMission2.ThirdMomentDescription = "Papel ou cartolina em branco e canetas coloridas são muito atrativas para os alunos, e podem auxiliar nas atividades de síntese.";

                DescriptionsInMission3.FirstMomentDescription = "O cartaz em branco, junto das canetas coloridas, talvez sejam melhor utilizados em uma atividade de apresentação.";
                DescriptionsInMission3.SecondMomentDescription = "O cartaz em branco, junto das canetas coloridas, é uma mídia bastante utilizada e de domínio dos alunos da turma, onde podem expor os resultados de sua pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "O cartaz em branco com canetas coloridas é uma mídia bastante utilizada e de domínio dos alunos, mas uma mídia inovadora pode proporcionar uma atividade interessante.";
                UpgradeFrom.Add(ItemName.Cartazes);
                break;
            case ItemName.VhsEditado: //da missão 2
                FriendlyName = "VHS Editado";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) serão usadas para ver partes selecionadas do filme pelo professor sobre a Revolução Industrial.";

                DescriptionsInMission2.DialogueWhenAcquired = "Uma TV com VHS! Como o VHS é editado, e portanto mais curto, é mais fácil prender a atenção dos alunos!";
                DescriptionsInMission2.Geral = "O VHS capta e reproduz vídeo e áudio, contém na fita apenas as cenas mais importantes do filme sobre a Revolução Industrial.";
                DescriptionsInMission2.Com = "Assistir cenas do filme para compreender o momento histórico vivido.";
                DescriptionsInMission2.Sobre = "Analisar como as crianças eram exploradas e analisar o contexto histórico que elas viviam.";
                DescriptionsInMission2.Atraves = "Gravar depoimentos de crianças em situação de Trabalho Infantil, fazer um vídeo de conscientização a partir do tema estudado.";

                //DescriptionsInMission2.StandardDescription = "O VHS (Video Home System) consiste em um sistema de captação e reprodução de vídeo e áudio, contendo informações selecionadas sobre a revolução industrial.";
                DescriptionsInMission2.StandardDescription = "Um VHS editado! Ele é mais curto, assim fica mais fácil prender a atenção dos alunos!";

                DescriptionsInMission2.FirstMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";
                DescriptionsInMission2.ThirdMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";
                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.Diario:
                FriendlyName = "Diário";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Um diário de uma criança que trabalhava na época da Revolução Industrial! Isso pode ser muito útil para a aula!";

                DescriptionsInMission2.DialogueWhenAcquired = "Um livro para uma criança aprendendo a ler, deixa eu ver...";
                DescriptionsInMission2.Geral = "Caderno com textos escritos a mão por uma criança que trabalhava durante a revolução industrial.";
                DescriptionsInMission2.Com = "Os registros escritos de momentos históricos pode pode ser utilizado para auxiliar a entender como era a realidade das crianças e dos adultos daquela época.";
                DescriptionsInMission2.Sobre = "Refletir sobre as diferenças de um registro pessoal e de uma informação de mídia de massa, pois contém a subjetividade de quem escreve, sem o interesse de parecer imparcial.";
                DescriptionsInMission2.Atraves = "Produção de registros escritos pelos alunos daquele momento da aula ou a criação de um diário coletivo da turma.";

                //DescriptionsInMission2.StandardDescription = "Diário escrito à mão por uma criança que viveu no período da revolução industrial, contendo registros de suas vivências e seu trabalho na fábrica.";
                DescriptionsInMission2.StandardDescription = "Um diário! E um diário de uma criança que trabalhava na época da Revolução Industrial! Isso pode ser muito útil para a aula!";

                DescriptionsInMission2.FirstMomentDescription = "O diário possui muitas informações sobre o dia a dia de uma criança naquele período, o que pode ajudar na exposição do professor.";
                DescriptionsInMission2.SecondMomentDescription = "O diário tem informações pessoais e bem específicas sobre a condição do trabalho de uma criança durante esse período histórico.";
                DescriptionsInMission2.ThirdMomentDescription = "O diário possui muitas informações sobre o dia a dia de uma criança naquele período.";
                break;
            case ItemName.ReprodutorAudioComCDPassaros:
                FriendlyName = "Aparelho de Som + CD sobre Pássaros";
                Description = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";
                FullDescription = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";

                DescriptionsInMission1.DialogueWhenAcquired = "Um CD junto com o aparelho de som os alunos poderão ouvir o canto de pássaros de diferentes regiões!";
                DescriptionsInMission1.InventoryDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";
                DescriptionsInMission1.Geral = "Áudio com o canto de pássaros de diferentes regiões.";
                DescriptionsInMission1.Com = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";
                DescriptionsInMission1.Sobre = "Com o áudio contido nesse recurso, os estudantes podem refletir em grupos sobre os diferentes cantos que ouviram.";
                DescriptionsInMission1.Atraves = "Podem criar perguntas e fazer uma pesquisa sobre as suas dúvidas.";

                //DescriptionsInMission1.StandardDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com o som do canto dos pássaros.";
                DescriptionsInMission1.StandardDescription = "Um CD! Com o aparelho de som isso vai ser uma ótima mídia!";

                DescriptionsInMission1.FirstMomentDescription = "O CD com o som do canto dos pássaros pode ser um interessante meio de pesquisa para os alunos.";
                DescriptionsInMission1.SecondMomentDescription = "Um CD com o canto de pássaros de diferentes regiões.";
                DescriptionsInMission1.ThirdMomentDescription = "A reprodução do CD mesmo que não apresente imagens, pode ser uma boa ferramenta para complementar a explicação dos alunos.";
                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            case ItemName.FotografiaRevolucaoIndustrial:
                FriendlyName = "Fotografia da Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "A fotografia é o processo e a arte que permite registar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";

                DescriptionsInMission2.DialogueWhenAcquired = "Uma foto! Ela pode ser útil para a aula, só preciso pensar com cuidado o momento certo para usá-la!";
                DescriptionsInMission2.Geral = "Imagem impressa de uma criança em uma fábrica na época da Revolução Industrial.";
                DescriptionsInMission2.Com = "É um recurso interessante para ilustrar um determinado momento histórico. Com ela é possível analisar e entender o contexto ao qual as fotos foram tiradas.";
                DescriptionsInMission2.Sobre = "Refletir sobre como o registro fotográfico oferece mais do que a própria imagem em si, mas é uma ferramenta que comporta uma memória, sentimentos e percepções.";
                DescriptionsInMission2.Atraves = "Produzir registros fotográficos sobre as relações de trabalho na atualidade, comparando com as fotos antigas.";

                DescriptionsInMission2.StandardDescription = "Processo e arte de registrar e reproduzir, através de reações químicas e em superfícies preparadas para o efeito, as imagens que se tiram no fundo de uma câmara escura.";
                DescriptionsInMission2.FirstMomentDescription = "Fotografia de uma criança em situação de Trabalho Infantil pode sensibilizar os alunos";
                DescriptionsInMission2.SecondMomentDescription = "A fotografia de uma criança em situação de trabalho infantil na Revolução Industrial pode ilustrar muitas das condições apresentadas pelo professor e ajudar a sensibilizar os alunos para a discussão.";
                DescriptionsInMission2.ThirdMomentDescription = "A fotografia de uma criança em situação de Trabalho Infantil.";
                break;
            case ItemName.TVComVHSRevolucaoIndustrial:
                FriendlyName = "TV com VHS sobre Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) combinadas serão usadas para ver um documentário sobre a Revolução Industrial.";

                DescriptionsInMission2.DialogueWhenAcquired = "Aqui! Achei um documentário sobre a Revolução Industrial.  Mas ele me parece ser bem longo… Não daria tempo de mostrar o documentário todo...";
                DescriptionsInMission2.Geral = "Consiste em um sistema de captação e reprodução de vídeo e áudio. Vai ser usado para ver o filme sobre a revolução industrial.";
                DescriptionsInMission2.Com = "Assistir o filme para conhecer detalhes da época.";
                DescriptionsInMission2.Sobre = "Analisar como as crianças eram exploradas a partir das imagens e informações.";
                DescriptionsInMission2.Atraves = "Junto com a câmera poderia gravar depoimentos de crianças contando sobre situações vivenciadas de exploração infantil, se conhecem, já viveram ou impressões do filme.";

                //DescriptionsInMission2.StandardDescription = "Essas mídias(TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre a revolução industrial.";
                DescriptionsInMission2.StandardDescription = "Uma foto! Ela pode ser útil para a aula, só preciso pensar com cuidado na forma que posso usar essa mídia!";

                DescriptionsInMission2.FirstMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";
                DescriptionsInMission2.ThirdMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS  sobre a revolução industrial.";

                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.ReprodutorAudioComCDRevolucaoIndustrial:
                FriendlyName = "CD Revolução Industrial";
                Description = "Essas mídias combinadas possibilitam a reprodução do áudio com uma entrevista de um especialista sobre a revolução industrial.";
                FullDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com uma entrevista de um especialista sobre a revolução industrial.";

                DescriptionsInMission1.InventoryDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";

                DescriptionsInMission2.DialogueWhenAcquired = "Um CD com entrevista de um especialista vai ser muito importante para a aula!";
                DescriptionsInMission2.Geral = "Áudio com uma entrevista de um especialista sobre a revolução industrial.";
                DescriptionsInMission2.Com = "O CD pode ser ouvido pela turma para agregar conhecimento sobre o conteúdo estudado.";
                DescriptionsInMission2.Sobre = "Com a entrevista contida neste recurso, os estudantes terão que refletir em grupos e em seguida escrever um texto apontando o que significou mais para eles do que ouviram na entrevista.";
                DescriptionsInMission2.Atraves = "Os estudantes poderiam criar perguntas e fazer uma nova entrevista com algum outro professor de história.";

                //DescriptionsInMission2.StandardDescription = "Essas mídias combinadas possibilitam a reprodução do áudio com uma entrevista de um especialista sobre a revolução industrial.";
                DescriptionsInMission2.StandardDescription = "Um CD! Com o aparelho de som isso vai ser uma ótima mídia!";

                DescriptionsInMission2.FirstMomentDescription = "Levar uma entrevista com um especialista no assunto enriquece muito a exposição.";
                DescriptionsInMission2.SecondMomentDescription = "Esse é um momento de discussão entre a turma,  um áudio de entrevista pode ajudar a guiar a conversa.";
                DescriptionsInMission2.ThirdMomentDescription = "O CD traz muitas informações sobre o fato histórico escolhido.";
                UpgradeFrom.Add(ItemName.ReprodutorAudio);
                break;
            case ItemName.TVComVHSRevolucaoIndustrialEditado:
                FriendlyName = "TV com VHS Editado sobre Revolução Industrial";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) serão usadas para ver partes selecionadas do filme pelo professor sobre a Revolução Industrial. ";

                DescriptionsInMission2.DialogueWhenAcquired = "Uma TV com VHS! Como o VHS é editado, e portanto mais curto, é mais fácil prender a atenção dos alunos!";
                DescriptionsInMission2.Geral = "O VHS capta e reproduz vídeo e áudio, contém na fita apenas as cenas mais importantes do filme sobre a Revolução Industrial.";
                DescriptionsInMission2.Com = "Assistir cenas do filme para compreender o momento histórico vivido.";
                DescriptionsInMission2.Sobre = "Analisar como as crianças eram exploradas e analisar o contexto histórico que elas viviam.";
                DescriptionsInMission2.Atraves = "Gravar depoimentos de crianças em situação de Trabalho Infantil, fazer um vídeo de conscientização a partir do tema estudado.";

                //DescriptionsInMission2.StandardDescription = "Essas mídias(TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o filme sobre a revolução industrial.";
                DescriptionsInMission2.StandardDescription = "Um VHS editado! Ele é mais curto, assim fica mais fácil prender a atenção dos alunos!";

                DescriptionsInMission2.FirstMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";
                DescriptionsInMission2.SecondMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";
                DescriptionsInMission2.ThirdMomentDescription = "Um sistema de captação e reprodução de vídeo e áudio. VHS editado sobre a revolução industrial.";

                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.FolhaSulfite:
                FriendlyName = "Folha Sulfite";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Folhas brancas de dimensão A4, que pode ter diversos usos de acordo com a situação, sejam desenhos, anotações, avisos, dobraduras, etc.";

                DescriptionsInMission3.DialogueWhenAcquired = "Folhas sulfite! Com ela podemos fazer desenhos, anotações, avisos, dobraduras. Há tantas possibilidades!";
                DescriptionsInMission3.Geral = "Papel branco, que pode ser usado pelos alunos durante uma atividade de aula para desenhos, anotações, avisos, dobraduras, etc.";
                DescriptionsInMission3.Com = "Mídia impressa que pode ser utilizada para passar informações, avisos ou apresentar trabalhos.";
                DescriptionsInMission3.Sobre = "É possível fazer uma análise crítica das formas de comunicações que podem ser feitas por meio das folhas A4 como anúncios, propagandas e até cartazes.";
                DescriptionsInMission3.Atraves = "Os alunos podem utilizar essa mídia como conclusão de um trabalho, por ser de comunicação visual para socializar uma produção e trabalhar com a escrita e o desenho (ou com textos e imagens).";

                //DescriptionsInMission3.StandardDescription = "Folhas brancas de dimensão A4, que pode ter diversos usos de acordo com a situação, sejam desenhos, avisos, dobraduras, etc.";
                DescriptionsInMission3.StandardDescription = "Folhas sulfite! Há tantas coisas que podemos fazer usando elas!";

                DescriptionsInMission3.FirstMomentDescription = "A folha sulfite está em branco, podendo ser usada para registrar muitas informações.";
                DescriptionsInMission3.SecondMomentDescription = "A folha sulfite pode ser usada para expor o contéudo da pesquisa.";
                DescriptionsInMission3.ThirdMomentDescription = "O professor pode solicitar que os alunos escrevam diversas palavras regionais na folha sulfite ou que façam uma síntese do que foi discutido.";
                break;
            case ItemName.VHSregionalismo:
                FriendlyName = "VHS Documentário sobre Regionalismo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "As mídias (TV-VHS) combinadas serão usadas para ver o documentário sobre os diferentes costumes ou tradições regionais. ";

                DescriptionsInMission3.DialogueWhenAcquired = "Um VHS sobre regionalismo! Essa mídia chama muita atenção dos alunos.";
                DescriptionsInMission3.Geral = "O VHS capta e reproduz vídeo e áudio, essas mídias permite que o documentário sobre regionalismos possa ser exibido na sala de aula.";
                DescriptionsInMission3.Com = "Assistir o documentário para entender os diferentes traços culturais e linguísticos de cada região.";
                DescriptionsInMission3.Sobre = "Analisar como a cultura é apropriada e modificada ao mesmo tempo pelas pessoas que vivem em diferentes regiões.";
                DescriptionsInMission3.Atraves = "Produzir um vídeo em VHS com as crianças mostrando os diferentes traços culturais/regionais dos indivíduos da escola.";

                //DescriptionsInMission3.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver o documentário sobre os diferentes costumes ou tradições regionais.";
                DescriptionsInMission3.StandardDescription = "Um VHS!  Com a TV isso vai ser uma ótima mídia!";

                DescriptionsInMission3.FirstMomentDescription = "Um documentário sobre regionalismos trazido pelo professor  pode ser uma boa fonte para pesquisa dos alunos.";
                DescriptionsInMission3.SecondMomentDescription = "Os alunos podem escolher algumas partes documentário sobre regionalismo para enriquecer a exposição.";
                DescriptionsInMission3.ThirdMomentDescription = "O documentário possui muitas informações importantes sobre o tema estudado.";

                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.VHSregionalismoEditado:
                FriendlyName = "VHS Editado de Regionalismo";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Essas mídias (TV-VHS) combinadas serão usadas para ver partes selecionadas pelo professor do documentário sobre regionalismo.";

                DescriptionsInMission3.DialogueWhenAcquired = "Uma TV com VHS! Como o VHS é editado, e portanto mais curto, é mais fácil prender a atenção dos alunos!";
                DescriptionsInMission3.Geral = "O VHS capta e reproduz vídeo e áudio, contém na fita apenas as cenas mais importantes do filme sobre regionalismo.";
                DescriptionsInMission3.Com = "Assistir a edição do documentário para entender os diferentes traços culturais e linguísticos de cada região, de acordo com a seleção do professor.";
                DescriptionsInMission3.Sobre = " Analisar como a cultura é apropriada e modificada ao mesmo tempo pelas pessoas que vivem em diferentes regiões.";
                DescriptionsInMission3.Atraves = "Produzir um vídeo em VHS com as crianças mostrando os diferentes traços culturais/regionais das pessoas da escola.";

                //DescriptionsInMission3.StandardDescription = "Essas mídias (TV-VHS) combinadas com o aparelho reprodutor de VHS serão usadas para ver partes selecionadas pelo professor sobre o documentário sobre regionalismo.";
                DescriptionsInMission3.StandardDescription = "Um VHS editado! Ele é mais curto, assim fica mais fácil prender a atenção dos alunos!";

                DescriptionsInMission3.FirstMomentDescription = "O VHS editado pelo professor apresenta muitas informações pertinentes de forma sucinta, sendo uma boa fonte para a pesquisa dos alunos sobre a temática da aula";
                DescriptionsInMission3.SecondMomentDescription = "O VHS editado pelo professor apresenta as partes mais importantes do assunto abordado. Os alunos podem escolher alguns momentos para expor ao grupo.";
                DescriptionsInMission3.ThirdMomentDescription = "O documentário editado pelo professor compila muitas informações importantes.";

                UpgradeFrom.Add(ItemName.TVComVHS);
                break;
            case ItemName.Enciclopedia:
                FriendlyName = "Enciclopédia";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Coletânea de livros e textos que tratam com maior profundidade o conhecimento humano.";

                DescriptionsInMission3.DialogueWhenAcquired = "Uma enciclopédia! Os alunos podem achar muitos exemplos de regionalismo nela!";
                DescriptionsInMission3.Geral = "A Enciclopédia foi desenvolvida a partir do dicionário no século XVIII, em busca de aprofundar os conceitos para além das informações limitadas do dicionário.";
                DescriptionsInMission3.Com = "Pode ser utilizado como fonte de pesquisa em diversas disciplinas.";
                DescriptionsInMission3.Sobre = "Permite reflexão sobre o acúmulo do conhecimento adquirido pelo ser humano ao longo do tempo e sobre o trabalho de compilação dos mesmos.";
                DescriptionsInMission3.Atraves = "Realizar com a turma a criação de uma enciclopédia sobre tema específico, trazendo os conceitos abordados ao longo do ano.";

                //DescriptionsInMission3.StandardDescription = "Coletânea de livros e textos que tratam com maior profundidade o conhecimento humano.";
                DescriptionsInMission3.StandardDescription = "Uma enciclopédia! Os alunos podem achar muitas palavras regionais nela!";

                DescriptionsInMission3.FirstMomentDescription = "A enciclopédia compila muitas informações importantes para a realização da pesquisa dos alunos sobre regionalismo, possuindo imagens e textos sobre a temática.";
                DescriptionsInMission3.SecondMomentDescription = "A Enciclopédia possui textos e imagens sobre regionalismos.";
                DescriptionsInMission3.ThirdMomentDescription = "A Enciclopédia possui textos e imagens sobre regionalismos.";
                break;
            case ItemName.Adedonha:
                FriendlyName = "Jogo Adedonha (Stop)";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "Adedonha ou Stop! consiste em um jogo de conhecimentos gerais. Cada coluna da tabela recebe o nome de uma categoria de palavras como animais, carros, cidades, cores etc., e cada linha representa uma rodada do jogo.";

                DescriptionsInMission3.DialogueWhenAcquired = "Os alunos se divertem muito jogando Stop!";
                DescriptionsInMission3.Geral = "Adedonha ou Stop! consiste em um jogo de conhecimentos gerais. Cada coluna da tabela recebe o nome de uma categoria de palavras como animais, carros, cidades, cores etc., e cada linha representa uma rodada do jogo. No início da rodada é sorteado a letra inicial e o primeiro que terminar todas as colunas termina a rodada e os jogadores vão pontuando por resposta correta na coluna.";
                DescriptionsInMission3.Com = "O professor pode criar categorias dos conteúdos trabalhados na disciplina.";
                DescriptionsInMission3.Sobre = "Propor a reflexão sobre a difusão desse tipo de jogo no dia-a-dia da população e sua historicidade.";
                DescriptionsInMission3.Atraves = "Criação de jogos pelos alunos com conteúdos variados, para que os outros colegas tentem resolver.";

                //DescriptionsInMission3.StandardDescription = "Adedonha ou Stop! consiste em um jogo de conhecimentos gerais onde cada coluna da tabela recebe o nome de uma categoria de palavras como animais, carros, cidades, cores etc., e cada linha representa uma rodada do jogo.";
                DescriptionsInMission3.StandardDescription = "Um jogo de adedonha! Os alunos adoram jogos durante a aula!";

                DescriptionsInMission3.FirstMomentDescription = "O jogo de Stop é bastante rico, pode trabalhar e trazer a discussão sobre regionalismos.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo de Stop é bastante rico, pode trabalhar e trazer a discussão sobre regionalismos.";
                DescriptionsInMission3.ThirdMomentDescription = "Trazer a realidade dos alunos e incluir jogos educativos em atividade de síntese torna a aula mais divertida. É possível trabalhar diferentes palavras regionais com jogo.";
                break;
            case ItemName.Forca:
                FriendlyName = "Jogo da Forca";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "O jogo da forca é um jogo em que o jogador precisa acertar a palavra co, tendo somente o número de letras e o tema como dica, antes que o boneco seja enforcado. A cada erro é desenhado uma parte do corpo na forca.";

                DescriptionsInMission3.DialogueWhenAcquired = "Um jogo de forca! Mais uma novidade para levar para a aula.";
                DescriptionsInMission3.Geral = "O jogo da forca é um jogo que consiste na descoberta de uma palavra tendo somente o  número de letras e o tema ligado à palavra. A cada letra errada, é desenhada uma parte do corpo do enforcado. O jogo termina ou com o acerto da palavra ou com o término do preenchimento das partes corpóreas do enforcado.";
                DescriptionsInMission3.Com = "O professor pode criar o jogo com palavras chave do conteúdos trabalhados na disciplina.";
                DescriptionsInMission3.Sobre = "Propor a reflexão sobre a difusão desse tipo de jogo no dia-a-dia da população e sua historicidade.";
                DescriptionsInMission3.Atraves = "Criação de jogos pelos alunos com conteúdos variados, para que os outros colegas tentem resolver.";

                //DescriptionsInMission3.StandardDescription = "Este é um jogo em que o jogador precisa acertar a palavra, tendo somente o número de letras e o tema como dica, antes que o boneco seja enforcado, com um número limitado de erros.";
                DescriptionsInMission3.StandardDescription = "Um jogo de forca! Os alunos adoram jogos durante a aula!";

                DescriptionsInMission3.FirstMomentDescription = "O jogo da forca é bastante potente, pois trabalha com palavras e assim podem trazer a discussão sobre regionalismos.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo da forca é bastante potente, pois trabalha com palavras e as mesmas podem trazer a discussão sobre regionalismos.";
                DescriptionsInMission3.ThirdMomentDescription = "O jogo da forca pode ser utilizado como recurso para apresentar diversas palavras e informações para os alunos em uma atividade atrativa de síntese.";
                break;
            case ItemName.PalavrasCruzadas:
                FriendlyName = "Jogo Palavras Cruzadas";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "O jogo de palavras cruzadas é um passatempo bastante popular e consiste em linhas e colunas que devem ser preenchidas por palavras descobertas pelo jogador.";

                DescriptionsInMission3.DialogueWhenAcquired = "Um jogo de palavras cruzadas! Os alunos adoram jogos durante a aula!";
                DescriptionsInMission3.Geral = "O jogo de palavras cruzadas é um passatempo bastante popular e consiste em linhas e colunas que devem ser preenchidas por palavras descobertas pelo jogador. Ao se preencher uma das linhas, automaticamente se preenche alguns quadrados das outras linhas que a cruzam, tornando mais fácil sua resolução. É encontrada geralmente em jornais e revistas.";
                DescriptionsInMission3.Com = "O professor pode criar a própria palavra cruzada utilizando palavras chave dos conteúdos trabalhados na disciplina.";
                DescriptionsInMission3.Sobre = "Propor a reflexão sobre a difusão desse tipo de jogo no dia-a-dia da população e sua historicidade.";
                DescriptionsInMission3.Atraves = "Criação de palavras cruzadas pelos alunos com conteúdos variados, para que os outros colegas tentem resolver.";

                //DescriptionsInMission3.StandardDescription = "O jogo de palavras cruzadas é um passatempo bastante popular e consiste em linhas e colunas que devem ser preenchidas por palavras descobertas pelo jogador.";
                DescriptionsInMission3.StandardDescription = "Um jogo de palavras cruzadas! Os alunos adoram jogos durante a aula!";

                DescriptionsInMission3.FirstMomentDescription = "O jogo das Palavras Cruzadas é bastante rico, com ele podem ser abordados várias temáticas.";
                DescriptionsInMission3.SecondMomentDescription = "O jogo das Palavras Cruzadas é bastante rico, com ele podem ser abordados várias temáticas.";
                DescriptionsInMission3.ThirdMomentDescription = "O jogo das palavras cruzadas faz parte do dia a dia das crianças, trazer a ludicidade para a aula em momentos de atividade pode enriquecer a apropriação do conhecimento.";
                break;
            case ItemName.CDsotaques:
                FriendlyName = "CD Sotaques do Brasil";
                Description = "Sem descrição";
                FullDescription = "Sem texto";

                DescriptionsInMission1.InventoryDescription = "É um dispositivo eletrônico que armazena informações em formato compacto, que podem apenas ser lidas, mas seu conteúdo não poder ser alterado pelo usuário.";

                DescriptionsInMission3.DialogueWhenAcquired = "Um CD sobre sotaques do Brasil! Os alunos podem gostar.";
                DescriptionsInMission3.Geral = "Áudio explicando e dando exemplos dos diferentes sotaques do Brasil.";
                DescriptionsInMission3.Com = "Pode ser ouvido pela turma para que se tenha uma maior entendimento do tema.";
                DescriptionsInMission3.Sobre = "Depois de ouvir o conteúdo do CD analisar e refletir com o grupo, podem ser feitos debates e discussão apontando sua percepção sobre as diferenças e semelhanças.";
                DescriptionsInMission3.Atraves = "Fazer sínteses e  cartazes dos sotaques que ouviram.";

                //DescriptionsInMission3.StandardDescription = "Um CD com uma entrevista sobre o tema da aula, contendo uma entrevista falando sobre sotaques e regionalismos.";
                DescriptionsInMission3.StandardDescription = "Um CD! Com o aparelho de som isso vai ser uma ótima mídia!";

                DescriptionsInMission3.FirstMomentDescription = "O CD com uma entrevista sobre os sotaques do Basil pode ser um interessante meio de pesquisa para os alunos.";
                DescriptionsInMission3.SecondMomentDescription = "Os alunos podem escolher algumas partes do CD com uma entrevista sobre o tema para enriquecer a exposição.";
                DescriptionsInMission3.ThirdMomentDescription = "O CD com uma entrevista sobre o tema da aula.";
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

        //repetindo o processo para novas strings da classe
        if (DescriptionsInMission2.InventoryDescription.Equals("") && !DescriptionsInMission1.InventoryDescription.Equals(""))
            DescriptionsInMission2.InventoryDescription = DescriptionsInMission1.InventoryDescription;

        if (DescriptionsInMission3.InventoryDescription.Equals(""))
        {
            if (!DescriptionsInMission2.InventoryDescription.Equals(""))
                DescriptionsInMission3.InventoryDescription = DescriptionsInMission2.InventoryDescription;
            else if (!DescriptionsInMission1.InventoryDescription.Equals(""))
                DescriptionsInMission3.InventoryDescription = DescriptionsInMission1.InventoryDescription;
        }

        if (DescriptionsInMission2.DialogueWhenAcquired.Equals("") && !DescriptionsInMission1.DialogueWhenAcquired.Equals(""))
            DescriptionsInMission2.DialogueWhenAcquired = DescriptionsInMission1.DialogueWhenAcquired;

        if (DescriptionsInMission3.DialogueWhenAcquired.Equals(""))
        {
            if (!DescriptionsInMission2.DialogueWhenAcquired.Equals(""))
                DescriptionsInMission3.DialogueWhenAcquired = DescriptionsInMission2.DialogueWhenAcquired;
            else if (!DescriptionsInMission1.DialogueWhenAcquired.Equals(""))
                DescriptionsInMission3.DialogueWhenAcquired = DescriptionsInMission1.DialogueWhenAcquired;
        }

        if (DescriptionsInMission2.Geral.Equals("") && !DescriptionsInMission1.Geral.Equals(""))
            DescriptionsInMission2.Geral = DescriptionsInMission1.Geral;

        if (DescriptionsInMission3.Geral.Equals(""))
        {
            if (!DescriptionsInMission2.Geral.Equals(""))
                DescriptionsInMission3.Geral = DescriptionsInMission2.Geral;
            else if (!DescriptionsInMission1.Geral.Equals(""))
                DescriptionsInMission3.Geral = DescriptionsInMission1.Geral;
        }

        if (DescriptionsInMission2.Com.Equals("") && !DescriptionsInMission1.Com.Equals(""))
            DescriptionsInMission2.Com = DescriptionsInMission1.Com;

        if (DescriptionsInMission3.Com.Equals(""))
        {
            if (!DescriptionsInMission2.Com.Equals(""))
                DescriptionsInMission3.Com = DescriptionsInMission2.Com;
            else if (!DescriptionsInMission1.Com.Equals(""))
                DescriptionsInMission3.Com = DescriptionsInMission1.Com;
        }

        if (DescriptionsInMission2.Sobre.Equals("") && !DescriptionsInMission1.Sobre.Equals(""))
            DescriptionsInMission2.Sobre = DescriptionsInMission1.Sobre;

        if (DescriptionsInMission3.Sobre.Equals(""))
        {
            if (!DescriptionsInMission2.Sobre.Equals(""))
                DescriptionsInMission3.Sobre = DescriptionsInMission2.Sobre;
            else if (!DescriptionsInMission1.Sobre.Equals(""))
                DescriptionsInMission3.Sobre = DescriptionsInMission1.Sobre;
        }

        if (DescriptionsInMission2.Atraves.Equals("") && !DescriptionsInMission1.Atraves.Equals(""))
            DescriptionsInMission2.Atraves = DescriptionsInMission1.Atraves;

        if (DescriptionsInMission3.Atraves.Equals(""))
        {
            if (!DescriptionsInMission2.Atraves.Equals(""))
                DescriptionsInMission3.Atraves = DescriptionsInMission2.Atraves;
            else if (!DescriptionsInMission1.Atraves.Equals(""))
                DescriptionsInMission3.Atraves = DescriptionsInMission1.Atraves;
        }
    }
}