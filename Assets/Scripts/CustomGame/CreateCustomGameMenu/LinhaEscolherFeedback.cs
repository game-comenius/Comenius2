using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinhaEscolherFeedback : MonoBehaviour {

    private ItemName midia;
    public ItemName Midia
    {
        get { return midia; }
        set
        {
            midia = value;
            GetComponentInChildren<ItemInUserInterface>().ItemName = midia;
        }
    }

    private Poder poder;
    public Poder Poder
    {
        get { return poder; }
        set
        {
            poder = value;
            switch (poder)
            {
                default:
                    imagePoderMidia.sprite = spriteMidiaFraca; break;
                case Poder.Boa:
                    imagePoderMidia.sprite = spriteMidiaBoa; break;
                case Poder.MuitoBoa:
                    imagePoderMidia.sprite = spriteMidiaMuitoBoa; break;
                case Poder.Melhor:
                    imagePoderMidia.sprite = spriteMidiaMelhor; break;
            }
        }
    }
    private Poder[] poderes;

    public string Feedback
    {
        get { return placeholder.enabled ? placeholder.text : feedback.text; }
    }

    [SerializeField] private Image imagePoderMidia;
    [SerializeField] private Sprite spriteMidiaFraca;
    [SerializeField] private Sprite spriteMidiaBoa;
    [SerializeField] private Sprite spriteMidiaMuitoBoa;
    [SerializeField] private Sprite spriteMidiaMelhor;
    [SerializeField] private Button botaoDiminuirPoder;
    [SerializeField] private Button botaoAumentarPoder;

    [SerializeField] private TextMeshProUGUI placeholder;
    [SerializeField] private TextMeshProUGUI feedback;

    // Use this for initialization
    void Start () {
        poderes = new Poder[] { Poder.Fraca, Poder.Boa, Poder.MuitoBoa, Poder.Melhor };
        botaoDiminuirPoder.onClick.AddListener(DiminuirPoder);
        botaoAumentarPoder.onClick.AddListener(AumentarPoder);
        // Valor incial do poder da mídia
        Poder = Poder.Fraca;
        botaoDiminuirPoder.gameObject.SetActive(false);

        // Valor inicial do placeholder do feedback
        AtualizarPlaceholderFeedback(Poder);
    }

    private void DiminuirPoder()
    {
        var indicePoderDiminuido = Array.IndexOf(poderes, Poder) - 1;
        Poder = poderes[indicePoderDiminuido];
        AtualizarPlaceholderFeedback(Poder);

        // Ativar o botão de diminuir poder e desativar botão aumentar poder
        // se chegarmos no poder máximo
        botaoAumentarPoder.gameObject.SetActive(true);
        if (indicePoderDiminuido == 0)
            botaoDiminuirPoder.gameObject.SetActive(false);
    }

    private void AumentarPoder()
    {
        var indicePoderAumentado = Array.IndexOf(poderes, Poder) + 1;
        Poder = poderes[indicePoderAumentado];
        AtualizarPlaceholderFeedback(Poder);

        // Ativar o botão de diminuir poder e desativar botão aumentar poder
        // se chegarmos no poder máximo
        botaoDiminuirPoder.gameObject.SetActive(true);
        if (indicePoderAumentado == poderes.Length - 1)
            botaoAumentarPoder.gameObject.SetActive(false);
    }

    private void AtualizarPlaceholderFeedback(Poder novoPoder)
    {
        Poder = novoPoder;
        string f = Midia.ToString() + " é uma mídia ";
        switch (Poder)
        {
            case Poder.Fraca: f += "fraca"; break;
            case Poder.Boa: f += "boa"; break;
            case Poder.MuitoBoa: f += "muito boa"; break;
            case Poder.Melhor: f += "excelente"; break;
        }
        f += " para este momento da aula!";
        placeholder.SetText(f);
    }
}
