using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotaoAbrirFichario : MonoBehaviour, IPointerClickHandler {

    private bool ativo;
    public bool Ativo
    {
        get { return ativo; }

        set
        {
            ativo = value;
            DefinirVisibilidade(ativo);
        }
    }

    private Fichario fichario;

    // UI
    private Image image;
    private Button button;

    void Start () {
        fichario = FindObjectOfType<Fichario>();

        image = GetComponent<Image>();
        button = GetComponent<Button>();

        // Essa linha está aqui para facilitar o desenvolvimento do jogo.
        // Se o jogador começa o jogo desde o início, o botão para abrir o
        // fichário estará escondido até a dica para abrir o fichário na sala
        // multimeios.
        // Porém, durante o desenvolvimento, ou seja, executar a partir de
        // qualquer cena, é interessante que este botão apareça.
        if (SceneManager.GetActiveScene().name == "M1_Intro") Ativo = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable) fichario.Abrir();
    }

    private void DefinirVisibilidade(bool visivel)
    {
        image.enabled = visivel;
    }
}
