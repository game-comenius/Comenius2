using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FichaDesempenhoAula : MonoBehaviour {

    public TextMeshProUGUI Titulo;
    public string textoTituloPadrao;

    public CharacterName Professor;
    public Image ImageFotoProfessor;

    public TextMeshProUGUI FeedbackProfessor;

    [SerializeField] private Sprite spriteMidiaFraca;
    [SerializeField] private Sprite spriteMidiaBoa;
    [SerializeField] private Sprite spriteMidiaMuitoBoa;
    [SerializeField] private Sprite spriteMidiaMelhor;

    [Serializable] public struct MidiaPoderObjects
    {
        public ItemInUserInterface ImageMidia;
        public Image ImagePoder;
    }
    public MidiaPoderObjects MidiaPoder1;
    public MidiaPoderObjects MidiaPoder2;
    public MidiaPoderObjects MidiaPoder3;

    [SerializeField] private Sprite spriteResultadoAulaFraca;
    [SerializeField] private Sprite spriteResultadoAulaBoa;
    [SerializeField] private Sprite spriteResultadoAulaMuitoBoa;
    [SerializeField] private Sprite spriteResultadoAulaMelhor;

    public Image imageResultadoAula;

    private void Start()
    {
        textoTituloPadrao = "Ficha de Desempenho: Aula de ";
        switch (Player.Instance.missionID)
        {
            case 0: textoTituloPadrao += "Ciências"; break;
            case 1: textoTituloPadrao += "História"; break;
            case 2: textoTituloPadrao += "Português"; break;
        }
        DefinirTitulo(textoTituloPadrao);
    }

    public void Mostrar()
    {
        DefinirFotoProfessor();
        DefinirMidiasESeusPoderes();
    }

    public void DefinirTitulo(string texto)
    {
        Titulo.text = texto;
    }

    private void DefinirFotoProfessor()
    {
        ImageFotoProfessor.sprite = CharacterSpriteDatabase.Foto(Professor);
        ImageFotoProfessor.preserveAspect = true;
    }

    private void DefinirMidiasESeusPoderes()
    {
        var p = Player.Instance;
        var historico = p.MissionHistory[p.missionID];

        MidiaPoderObjects[] ImagesMidiaPoder = { MidiaPoder1, MidiaPoder2, MidiaPoder3 };

        for (var i = 0; i < historico.chosenMedia.Length; i++)
        {
            // Definir sprite da mídia escolhida pelo jogador para o momento
            ImagesMidiaPoder[i].ImageMidia.ItemName = historico.chosenMedia[i];

            // Definir sprite do poder da mídia para este momento
            var image = ImagesMidiaPoder[i].ImagePoder;
            switch (historico.points[i])
            {
                default: image.sprite = spriteMidiaFraca; break;
                case 7: image.sprite = spriteMidiaBoa; break;
                case 8: image.sprite = spriteMidiaMuitoBoa; break;
                case 10: image.sprite = spriteMidiaMelhor; break;
            }
        }

        ApresentarPontuacaoFinalDaAula(historico.totalMissionPoints);
    }

    private void ApresentarPontuacaoFinalDaAula(int pontuacaoTotal)
    {
        Func<int, int, bool> PontuacaoEntre = (a, b) =>
        {
            if (pontuacaoTotal >= a && pontuacaoTotal <= b) return true;
            else return false;
        };

        if (PontuacaoEntre(26, 30))
        {
            imageResultadoAula.sprite = spriteResultadoAulaMelhor;
            FeedbackProfessor.text = "Você encontrou as mídias audiovisuais pela escola e conseguiu usá-las da melhor forma possível na elaboração do seu planejamento deixando-o mais diversificado e atrativo.  Parabéns, os alunos aprenderam muito com a sua aula!";
        }
        else if (PontuacaoEntre(21, 25))
        {
            imageResultadoAula.sprite = spriteResultadoAulaMuitoBoa;
            FeedbackProfessor.text = "Você demonstrou um ótimo domínio na utilização das mídias audiovisuais na elaboração do seu planejamento, e os alunos gostaram da aula que você planejou! Continue se dedicando em aprender mais sobre o uso das mídias na educação.";
        }
        else if (PontuacaoEntre(14, 20))
        {
            imageResultadoAula.sprite = spriteResultadoAulaBoa;
            FeedbackProfessor.text = "Faltou você analisar melhor os conteúdos e a metodologia, para assim poder utilizar as melhores mídias audiovisuais para deixar seu planejamento mais interessante para seus alunos. Você está no caminho, não desista!";
        }
        else
        {
            imageResultadoAula.sprite = spriteResultadoAulaFraca;
            FeedbackProfessor.text = "Talvez você precise se aprofundar mais nas potencialidades que as mídias audiovisuais oferecem. Elas podem te ajudar a planejar aulas mais atrativas e despertar a curiosidade e a atenção dos seus alunos, motivando-os a aprender mais! O melhor professor é aquele que não desiste nunca!";
        }
    }
}
