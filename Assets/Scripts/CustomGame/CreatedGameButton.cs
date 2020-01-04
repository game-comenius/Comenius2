using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class CreatedGameButton : MonoBehaviour, IPointerClickHandler
{
    private CustomGameSettings settings;

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

    private static CreatedGameButton currentlySelectedButton;
    private static Color offColor = new Color(0.7924528f, 0.7866229f, 0.5868636f, 0);
    private static Color onColor = new Color(0.7924528f, 0.7866229f, 0.5868636f, 0.2f);

    public void Configure(CustomGameSettings settings)
    {
        this.settings = settings;

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

        localImage.sprite = PlaceSpriteDatabase.SpriteOf(settings.Sala);
        localImage.preserveAspect = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentlySelectedButton != null)
        {
            var offBackground = currentlySelectedButton.transform.GetChild(0);
            offBackground.GetComponent<Image>().color = offColor;
        }

        currentlySelectedButton = this;
        var onBackground = this.transform.GetChild(0);
        onBackground.GetComponent<Image>().color = onColor;

        CustomGameSettings.CurrentSettings = this.settings;
    }
}
