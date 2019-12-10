using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CreatedGameButton : MonoBehaviour {

    [SerializeField]
    private TMP_Text tituloDaAula;
    [SerializeField]
    private TMP_Text autor;
    [SerializeField]
    private TMP_Text nivelDeEnsino;
    [SerializeField]
    private TMP_Text areaDeConhecimento;
    [SerializeField]
    private Image professorImage;
    [SerializeField]
    private Image localImage;

    public void Configure(CustomGameSettings settings)
    {
        tituloDaAula.text = settings.TituloDaAula;

        autor.text = "Autor(a): " + settings.Autor;

        var nivelDeEnsinoDesc = "Nível de Ensino: ";
        nivelDeEnsinoDesc += NivelDeEnsino.Get(settings.NivelDeEnsino).nome;
        nivelDeEnsino.text = nivelDeEnsinoDesc;

        var areaDesc = "Área de Conhecimento: ";
        areaDesc += AreaDeConhecimento.Get(settings.AreaDeConhecimento).nome;
        areaDeConhecimento.text = areaDesc;

        professorImage.sprite = CharacterSpriteDatabase.SpriteSW(settings.Professor);
        professorImage.preserveAspect = true;
    }
}
