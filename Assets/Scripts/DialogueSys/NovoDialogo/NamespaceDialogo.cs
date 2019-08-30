﻿using System;
using UnityEngine;
using UnityEditor;

namespace GameComenius.Dialogo
{
    public enum Personagens
    {
        Lurdinha,
        Drika,
        Diretor
    }

    public enum Emocoes
    {
        Neutro,
        Feliz,
        Triste,
        Assustado,
        Impressionado
    }

    [Serializable]
    public class Fala
    {
        public Personagens personagem = Personagens.Diretor;

        public Emocoes emocao = Emocoes.Assustado;

        [TextArea(3, 3)]
        public string fala = "";
    }

    [Serializable]
    public class Resposta : Fala
    {
        public string resumo = "";
        public int conexao = 0;
    }
    
    [Serializable]
    public class DialogoQuizzNodulo
    {
        public Fala[] falas = new Fala[0];

        [Tooltip("Resposta conter:\n" + "0 resposta indica que o dialogo será encerrado;\n" + "1 resposta indica que não haverá escolha e que este nodolo deve levar a outro;" + "2+ respostas indica que haverá escolha e cada escolha pode levar a um novo nodolo.")]
        public Resposta[] respostas = new Resposta[0];
    }

    [Serializable]
    public class DialogoQuizz
    {
        public DialogoQuizzNodulo[] nodulos = new DialogoQuizzNodulo[0];
    }

    [Serializable]
    public class Falador
    {
        public Sprite personagem;
        public string nome;

        static public Falador BuscarPolaroideNosAssets(Personagens _personagem, Emocoes _emocao)
        {
            Falador personagem = new Falador();

            string path = "Assets/Sprites/Personagem/";
            string nome = "";
            string emocao = "";

            switch (_personagem)
            {
                case Personagens.Diretor:
                    path = path + "Diretor";
                    nome = "Diretor";
                    break;
                case Personagens.Drika:
                    path = path + "Drika";
                    nome = "Drika";
                    break;
                case Personagens.Lurdinha:
                    path = path + "Lurdinha";
                    nome = "Lurdinha";
                    break;
            }

            path = path + ".png";

            switch (_emocao)
            {
                case Emocoes.Assustado:
                    emocao = "Assustado";
                    break;
                case Emocoes.Feliz:
                    emocao = "Feliz";
                    break;
                case Emocoes.Impressionado:
                    emocao = "Impressionado";
                    break;
                case Emocoes.Neutro:
                    emocao = "Neutro";
                    break;
                case Emocoes.Triste:
                    emocao = "Triste";
                    break;
            }

            UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (UnityEngine.Object obj in objects)
            {
                if (obj.GetType() == typeof(Sprite) && obj.name == emocao)
                {
                    personagem.nome = nome;
                    personagem.personagem = (Sprite)obj;

                    return personagem;
                }
            }

            Debug.Log("Não foi encontrado " + emocao + " em " + path + ".");

            return null;
        }
    }
}
