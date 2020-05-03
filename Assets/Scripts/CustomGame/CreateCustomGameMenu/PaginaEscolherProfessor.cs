using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginaEscolherProfessor : MonoBehaviour {

    [HideInInspector]
    public CharacterName ProfessorSelecionado
    {
        get { return professorSelecionado != null ? professorSelecionado.Value : CharacterName.Jean; }
    }
    [SerializeField] private Image imageJean;
    [SerializeField] private Image imageAlice;
    [SerializeField] private Image imageVladmir;
    [SerializeField] private Image imagePaulino;
    [SerializeField] private Image imageCelestino;
    [SerializeField] private Image imageAntonia;
    [SerializeField] private Image imageMontanari;
    [SerializeField] private Image imageDiretor;
    private List<Image> imagesDosProfessores;

    [SerializeField] private TextMeshProUGUI balaoTexto1;
    [SerializeField] private TextMeshProUGUI balaoTexto2;

    private LinkedList<CharacterName> professoresDisponiveis;
    private LinkedListNode<CharacterName> professorSelecionado;

	// Use this for initialization
	void Start () {
        // Definir quais professores estão disponíveis para serem escolhidos
        professoresDisponiveis = new LinkedList<CharacterName>();
        professoresDisponiveis.AddLast(CharacterName.Jean);
        professoresDisponiveis.AddLast(CharacterName.Antonia);
        professoresDisponiveis.AddLast(CharacterName.Vladmir);
        professoresDisponiveis.AddLast(CharacterName.Montanari);
        professoresDisponiveis.AddLast(CharacterName.Paulino);
        professoresDisponiveis.AddLast(CharacterName.Alice);
        professoresDisponiveis.AddLast(CharacterName.Celestino);
        professoresDisponiveis.AddLast(CharacterName.Diretor);

        imagesDosProfessores = new List<Image>()
        {
            imageJean, imageAlice, imageVladmir, imagePaulino,
            imageCelestino, imageAntonia, imageMontanari, imageDiretor,
        };
        foreach (var image in imagesDosProfessores) image.preserveAspect = true;

        SelecionarProfessor(professoresDisponiveis.First);
    }

    public void SelecionarProfessor(LinkedListNode<CharacterName> professor)
    {
        professorSelecionado = professor;
        foreach (var image in imagesDosProfessores) image.enabled = false;

        balaoTexto1.text = "Olá jogador(a), muito prazer! Me chamo " + professor.Value.NomeCompleto() + " e irei auxiliar você durante a customização de uma missão do Game Comenius - Módulo 2,  onde eu serei o professor. Você é livre para escolher as características da aula, mas gostaria de falar um pouco de como eu penso educação para você.";
        switch (professor.Value)
        {
            case CharacterName.Jean:
                imageJean.enabled = true;
                balaoTexto2.text = "Eu sou um professor progressivista. Acredito que devemos proporcionar vivências com desafios e situações-problema que estimulem os alunos a buscarem soluções. Os alunos devem aprender fazendo, de acordo com o seu tempo e desenvolvimento, por isso dou espaço para tentativas experimentais, pesquisa e descoberta.";
                break;
            case CharacterName.Vladmir:
                imageVladmir.enabled = true;
                balaoTexto2.text = "Eu sou um professor progressivista. Acredito que devemos proporcionar vivências com desafios e situações-problema que estimulem os alunos a buscarem soluções. Os alunos devem aprender fazendo, por isso dou espaço para tentativas experimentais, pesquisa e descoberta.";
                break;
            case CharacterName.Alice:
                imageAlice.enabled = true;
                balaoTexto2.text = "Eu costumo pensar nos alunos como centrais no processo de ensino-aprendizagem. Para mim é primordial trazer a cultura diversificada dos nossos alunos para a sala de aula, além de deixá-los livres para se relacionar com o conhecimento e seus colegas. Minhas aulas têm como a foco a formação de atitudes. Por isso prefiro atividades que envolvem grandes grupos e discussões, para que todos possam interagir entre si.";
                break;
            case CharacterName.Montanari:
                imageMontanari.enabled = true;
                balaoTexto2.text = "O meu papel como professor é o de preparar os alunos, por meio do conhecimento científico, para a sociedade em que eles vivem, sem a necessidade de afeto. Debates, dis­cussões, questionamentos são desnecessários. Importante é transmitir oconhecimento visando o aprendizado de todos, assegurando a recepção de informações. Dou muita importância para a tecnologia educacional.";
                break;
            case CharacterName.Paulino:
                imagePaulino.enabled = true;
                balaoTexto2.text = "Penso que a minha relação com os alunos deve ser horizontal, pois aprendemos de forma dialética. Além disso, a educação deve fazer sentido para eles, portanto, sempre trago a realidade deles para dentro de sala de aula, buscando estimular o pensamento crítico e a idéia de que eles também são produtores do conhecimento, pois não transfiro meus conhecimentos para eles, nós aprendemos juntos.";
                break;
            case CharacterName.Celestino:
                imageCelestino.enabled = true;
                balaoTexto2.text = "A escola é um espaço político, e por isso acredito que os alunos devam participar e levar suas aprendizagens para os grupos externos e institucionais. Os conteúdos devem estar a serviço dos alunos, que se apropriam de maneira crítica. O processo de ensino-aprendizagem deve ocorrer pelas participações em grupos, pela autonomia progressiva e pela própria iniciativa do aluno.";
                break;
            case CharacterName.Antonia:
                imageAntonia.enabled = true;
                balaoTexto2.text = "Acredito que o aprendizado é indissociável da realidade social. A escola é um potente transformador da sociedade, então os conteúdos escolares devem ter ressonância na vida dos alunos. O conhecimento novo se apóia no que o aluno já sabe, no seu próprio contexto, e a partir daí eu sistematizo uma sequência de aprendizagem cada vez mais complexa.";
                break;
            case CharacterName.Diretor:
                imageDiretor.enabled = true;
                balaoTexto2.text = "Eu sou um professor tradicional, penso que sou uma figura central no processo de ensino-aprendizagem, pois sou aquele que detém o conhecimento que os alunos aprenderão. Dessa forma, sinto-me confortável quando os alunos têm sua atenção focada em mim, para que não percam conteúdos e não se dispersem.";
                break;
            default:
                balaoTexto2.text = "";
                break;
        }
    }

    public void SelecionarProfessorSeguinte()
    {
        var proximoProfessor = professorSelecionado.Next ?? professoresDisponiveis.First;
        SelecionarProfessor(proximoProfessor);
    }

    public void SelecionarProfessorAnterior()
    {
        var professorAnterior = professorSelecionado.Previous ?? professoresDisponiveis.Last;
        SelecionarProfessor(professorAnterior);
    }
}
