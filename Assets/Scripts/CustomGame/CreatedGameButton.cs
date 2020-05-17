using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Globalization;

public class CreatedGameButton : MonoBehaviour, IPointerClickHandler
{
    private CustomGameSettings settings;
    private int index;

    [SerializeField]
    private TMP_Text tituloDaAula;
    [SerializeField]
    private TMP_Text autor;
    [SerializeField]
    private TMP_Text nivelDeEnsino;
    [SerializeField]
    private TMP_Text areaDeConhecimento;
    [SerializeField]
    private Image localImage;
    [SerializeField]
    private Button botaoExcluir;
    [SerializeField]
    private TMP_Text dataDaCriacao;


    private static CreatedGameButton currentlySelectedButton;
    private static Color offColor = new Color(0.7924528f, 0.7866229f, 0.5868636f, 0);
    private static Color onColor = new Color(0.7924528f, 0.7866229f, 0.5868636f, 0.2f);

    private void Awake()
    {
        // O padrão é botão excluir desativado
        DefinirVisibilidadeDoBotaoExcluir(false);
    }

    public void Configure(CustomGameSettings settings, int index)
    {
        this.settings = settings;

        tituloDaAula.text = settings.TituloDaAula;

        autor.text = settings.Autor;

        nivelDeEnsino.text = NivelDeEnsino.Get(settings.ValorNivelDeEnsino).nome;

        areaDeConhecimento.text = AreaDeConhecimento.Get(settings.ValorAreaDeConhecimento).nome;

        localImage.sprite = PlaceSpriteDatabase.SpriteOf(settings.Sala);
        localImage.preserveAspect = true;

        DateTime dateTime;
        if (DateTime.TryParse(settings.dataDeCriacao, out dateTime))
            dataDaCriacao.text = dateTime.ToString("d", CultureInfo.CreateSpecificCulture("pt-BR"));

        // Adicionar função ao OnClick do botão excluir, ela irá pedir para
        // o servidor excluir a aula com o índice deste botão
        this.index = index;
        botaoExcluir.onClick.AddListener(() =>
        {
            CustomGameSettings.DeleteFromServerByIndex(this.index);
            Destroy(this.gameObject);
        });
    }

    public void DefinirVisibilidadeDoBotaoExcluir(bool visibilidade)
    {
        botaoExcluir.gameObject.SetActive(visibilidade);
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
