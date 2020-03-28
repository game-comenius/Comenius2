﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProfessor : MonoBehaviour
{
    private enum Direction { NW, NE, SE, SW }
    [SerializeField]
    private Direction facingDirection;

    private CharacterName professor;

    private string introducaoAula;
    private string descricaoMomento1;
    private string descricaoMomento2;
    private string descricaoMomento3;

    private Sprite spriteNW;
    private Sprite spriteNE;
    private Sprite spriteSE;
    private Sprite spriteSW;


    public void ConfigurarProfessor(GameObject professorGO, CustomGameSettings settings)
    {
        professor = settings.Professor;

        introducaoAula = settings.IntroducaoAula;
        descricaoMomento1 = settings.DescricaoMomento1;
        descricaoMomento2 = settings.DescricaoMomento2;
        descricaoMomento3 = settings.DescricaoMomento3;
        DividirEInserirFalas(professorGO);

        // Definir o meu sprite como o sprite do professor escolhido e na
        // direção correta definida no Unity inspector
        spriteNW = CharacterSpriteDatabase.SpriteNW(professor);
        spriteNE = CharacterSpriteDatabase.SpriteNE(professor);
        spriteSE = CharacterSpriteDatabase.SpriteSE(professor);
        spriteSW = CharacterSpriteDatabase.SpriteSW(professor);

        SpriteRenderer mySP = professorGO.GetComponent<SpriteRenderer>();
        switch (facingDirection)
        {
            case Direction.NW:
                mySP.sprite = spriteNW;
                break;
            case Direction.NE:
                mySP.sprite = spriteNE;
                break;
            case Direction.SE:
                mySP.sprite = spriteSE;
                break;
            case Direction.SW:
                mySP.sprite = spriteSW;
                break;
        }

        // Alguns casos especiais, não quero mexer no .png
        // Basicamente, alterar o tamanho de alguns professores
        var professorTransform = professorGO.transform;
        switch (professor)
        {
            case CharacterName.Vladmir:
            case CharacterName.Paulino:
                professorTransform.localScale = Vector3.one * 0.7f;
                break;
        }
    }

    private void DividirEInserirFalas(GameObject professorGO)
    {
        var paragrafos = new string[4];
        paragrafos[0] = introducaoAula;
        paragrafos[1] = "Momento 1: " + descricaoMomento1;
        paragrafos[2] = "Momento 2: " + descricaoMomento2;
        paragrafos[3] = "Momento 3: " + descricaoMomento3;

        // Adicionar falas ao diálogo do professor
        var npc = professorGO.GetComponent<NpcDialogo>();
        var originalInteractOffset = npc.interactOffset;
        Destroy(npc);
        Destroy(professorGO.GetComponent<QuestGuest>());
        professorGO.AddComponent<QuestGuest>();
        npc = professorGO.AddComponent<NpcDialogo>();
        npc.interactOffset = originalInteractOffset;
        npc.dialogoPrincipal = new GameComenius.Dialogo.Dialogo();
        npc.dialogoPrincipal.nodulos = new GameComenius.Dialogo.DialogoNodulo[1];
        npc.dialogoPrincipal.nodulos[0] = new GameComenius.Dialogo.DialogoNodulo();
        npc.dialogoPrincipal.nodulos[0].falas = new GameComenius.Dialogo.Fala[1 + paragrafos.Length];

        // Fala da Lurdinha, obrigatoriamente ela deve iniciar um diálogo
        npc.dialogoPrincipal.nodulos[0].falas[0] = new GameComenius.Dialogo.Fala
        {
            // Uma fala em branco da Lurdinha é suficiente para o jogo fazer
            // um "skip" da fala dela mesmo que precise aqui no código
            fala = "",
            personagem = GameComenius.Dialogo.Personagens.Lurdinha,
            emocao = GameComenius.Dialogo.Expressao.Sorrindo
        };

        // Adicionar falas do professor
        for (int i = 0; i < paragrafos.Length; i++)
        {
            npc.dialogoPrincipal.nodulos[0].falas[i + 1] = new GameComenius.Dialogo.Fala();
            npc.dialogoPrincipal.nodulos[0].falas[i + 1].fala = paragrafos[i];
            var a = Enum.Parse(typeof(GameComenius.Dialogo.Personagens), professor.ToString(), true);
            npc.dialogoPrincipal.nodulos[0].falas[i + 1].personagem = (GameComenius.Dialogo.Personagens)a;
            npc.dialogoPrincipal.nodulos[0].falas[i + 1].emocao = GameComenius.Dialogo.Expressao.Sorrindo;
        }

        // Versão anterior que dividia o parágrafo caso ele fosse muito grande

        //// Número máximo de characteres em um parágrafo do jogo
        //var maxChars = 300;

        //// Dividir fala muito grande criada pelo jogador em parágrafos
        //List<string> listaDeFalas = new List<string>();

        //var startIndex = 0;
        //while (startIndex < novaFala.Length)
        //{
        //    var length = Math.Min(maxChars, (novaFala.Length - startIndex));
        //    var paragrafo = novaFala.Substring(startIndex, length).Trim();
        //    startIndex += length;

        //    // Consertando palavras cortadas ao meio...
        //    try
        //    {
        //        var nextChar = novaFala[startIndex];
        //        if (!char.IsWhiteSpace(nextChar))
        //        {
        //            var ultimoEspaco = paragrafo.LastIndexOf(' ');
        //            if (ultimoEspaco > 0)
        //            {
        //                paragrafo = paragrafo.Substring(0, ultimoEspaco).Trim();
        //                startIndex -= (length - ultimoEspaco);
        //            }
        //        }
        //    }
        //    // try ~ catch vazio para ignorar "array index out of bounds"
        //    catch { /* Do nothing */ }

        //    listaDeFalas.Add(paragrafo);
        //}

        //// Adicionar falas ao diálogo do professor
        //var npc = professorGO.GetComponent<NpcDialogo>();
        //npc.dialogoPrincipal = new GameComenius.Dialogo.Dialogo();
        //npc.dialogoPrincipal.nodulos = new GameComenius.Dialogo.DialogoNodulo[1];
        //npc.dialogoPrincipal.nodulos[0] = new GameComenius.Dialogo.DialogoNodulo();
        //npc.dialogoPrincipal.nodulos[0].falas = new GameComenius.Dialogo.Fala[1 + listaDeFalas.Count];

        //// Fala da Lurdinha, obrigatoriamente ela deve iniciar um diálogo
        //npc.dialogoPrincipal.nodulos[0].falas[0] = new GameComenius.Dialogo.Fala
        //{
        //    // Uma fala em branco da Lurdinha é suficiente para o jogo fazer
        //    // um "skip" da fala dela mesmo que precise aqui no código
        //    fala = "",
        //    personagem = GameComenius.Dialogo.Personagens.Lurdinha,
        //    emocao = GameComenius.Dialogo.Expressao.Sorrindo
        //};

        //// Adicionar falas do professor
        //for (var i = 0; i < listaDeFalas.Count; i++)
        //{
        //    npc.dialogoPrincipal.nodulos[0].falas[i + 1] = new GameComenius.Dialogo.Fala();
        //    npc.dialogoPrincipal.nodulos[0].falas[i + 1].fala = listaDeFalas[i];
        //    var a = Enum.Parse(typeof(GameComenius.Dialogo.Personagens), professor.ToString(), true);
        //    npc.dialogoPrincipal.nodulos[0].falas[i + 1].personagem = (GameComenius.Dialogo.Personagens) a;
        //    npc.dialogoPrincipal.nodulos[0].falas[i + 1].emocao = GameComenius.Dialogo.Expressao.Sorrindo;
        //}


    }
}
