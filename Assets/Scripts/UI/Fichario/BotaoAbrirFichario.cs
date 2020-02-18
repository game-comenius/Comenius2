using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotaoAbrirFichario : MonoBehaviour, IPointerClickHandler {

    private bool visivel;
    public bool Visivel
    {
        get { return visivel; }

        set
        {
            visivel = value;
            image.enabled = visivel;
        }
    }

    private bool ativo;
    public bool Ativo
    {
        get { return ativo; }

        set
        {
            ativo = value;
            image.color = ativo ? Color.white : corDesativado;
        }
    }

    private Fichario fichario;

    // UI
    private Image image;
    [SerializeField]
    private Color corDesativado;

    void Start() {
        fichario = FindObjectOfType<Fichario>();

        image = GetComponent<Image>();

        // Essa linha está aqui para facilitar o desenvolvimento do jogo.
        // Se o jogador começa o jogo desde o início, o botão para abrir o
        // fichário estará escondido até a dica para abrir o fichário na sala
        // multimeios.
        // Porém, durante o desenvolvimento, ou seja, executar a partir de
        // qualquer cena, é interessante que este botão apareça.
        if (SceneManager.GetActiveScene().name == "StartScene") Visivel = false;

        // Botão fica desativado quando alguma coisa está acontecendo na UI
        // como por exemplo quando o fichário está aberto
        GameManager.uiSendoUsadaEvent += () => { Ativo = false; };
        GameManager.uiNaoSendoUsadaEvent += () => { Ativo = true; };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Ativo) fichario.Abrir();
    }
}
