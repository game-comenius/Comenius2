using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FichaDesempenhoAula : MonoBehaviour {

    public TextMeshProUGUI Titulo;

    public CharacterName Professor;
    public Image ImageFotoProfessor;

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

    public void Mostrar()
    {
        DefinirTitulo();
        DefinirFotoProfessor();
        DefinirMidiasESeusPoderes();
    }

    private void DefinirTitulo()
    {
        var @string = "Ficha de Desempenho: Aula de ";
        switch (Player.Instance.missionID)
        {
            case 0: @string += "Ciências"; break;
            case 1: @string += "História"; break;
            case 2: @string += "Português"; break;
        }
        Titulo.text = @string;
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
            imageResultadoAula.sprite = spriteResultadoAulaMelhor;
        else if (PontuacaoEntre(21, 25))
            imageResultadoAula.sprite = spriteResultadoAulaMuitoBoa;
        else if (PontuacaoEntre(14, 20))
            imageResultadoAula.sprite = spriteResultadoAulaBoa;
        else
            imageResultadoAula.sprite = spriteResultadoAulaFraca;
    }
}
