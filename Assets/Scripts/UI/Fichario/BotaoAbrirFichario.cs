﻿using System.Collections;
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

            if (!GameManager.IsCustomGame && visivel && nuncaEsteveVisivelAntes)
            {
                MostrarAjudaFichario();
                nuncaEsteveVisivelAntes = false;
            }
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

    private bool nuncaEsteveVisivelAntes = true;
    [SerializeField] private AjudaComeniusFichario prefabAjudaComeniusFichario;

    // UI
    private Image image;
    [SerializeField]
    private Color corDesativado;

    private void Awake()
    {
        image = GetComponent<Image>();

        Visivel = false;
        // Essa linha está aqui para facilitar o desenvolvimento do jogo.
        // Se o jogador começa o jogo desde o início, o botão para abrir o
        // fichário estará escondido até a dica para abrir o fichário na sala
        // multimeios.
        // Porém, durante o desenvolvimento, ou seja, executar a partir de
        // qualquer cena, é interessante que este botão apareça.
        #if UNITY_EDITOR
        visivel = true;
        image.enabled = visivel;
        Ativo = true;
        #endif
        // Essa linha está aqui para facilitar o desenvolvimento do jogo.
        // Ativar este botão se a equipe de desenvolvimento executar o jogo
        // direto da missão 2 ou direto da missão 3.
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Equals("M2_Patio1_inicio") || currentScene.Equals("M3_Patio1_Chamado"))
        {
            visivel = true;
            image.enabled = visivel;
            Ativo = true;
        }
    }

    void Start() {
        fichario = FindObjectOfType<Fichario>();

        // Botão fica desativado quando alguma coisa está acontecendo na UI
        // como por exemplo quando o fichário está aberto
        GameManager.uiSendoUsadaEvent += () => { Ativo = false; };
        GameManager.uiNaoSendoUsadaEvent += () => { Ativo = true; };
    }

    public void MostrarAjudaFichario()
    {
        Instantiate(prefabAjudaComeniusFichario).Mostrar();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Ativo) fichario.Abrir();
    }
}
