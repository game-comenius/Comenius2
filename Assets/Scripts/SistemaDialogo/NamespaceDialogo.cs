using System;
using UnityEngine;
using System.Collections.Generic;

namespace GameComenius.Dialogo
{
    public enum Personagens
    {
        Lurdinha,
        Drica,
        Jean,
        Leitura,
        MeioAmbiente,
        Madá,
        Antonia,
        Alice,
        Diretor,
        Montanari,
        Comenius,
        AlunoTipo,
        Aluno,
        Esquisito,
        Vladmir,
        Paulino,
        Celestino,
    }

    public enum Expressao
    {
        Serio,
        Bravo,
        Sorrindo,
        CaraFechada,
        Rindo
    }

    [Serializable]
    public class Opacidade
    {
        [Range(0, 1)]
        public float alphaLigado;

        [Range(0, 1)]
        public float alphaDesligado;

        public Color Ligar()
        {
            return new Color(1, 1, 1, alphaLigado);
        }

        public Color Desligar()
        {
            return new Color(1, 1, 1, alphaDesligado);
        }
    }

    [Serializable]
    public class Fala
    {
        public Personagens personagem = Personagens.Diretor;

        public Expressao emocao = Expressao.Bravo;

        [TextArea(3, 3)]
        public string fala = "";
    }

    [Serializable]
    public class Resposta : Fala
    {
        public string resumo = "";
        public int conexao = 0;

        public QuestStruct questInfo = new QuestStruct
        {
            isQuest = false,
            questIndex = Vector2Int.zero,
            questDependencias = new Vector2Int[0]
        };
    }
    
    [Serializable]
    public class DialogoNodulo
    {
        public Fala[] falas = new Fala[0];

        [Tooltip("Resposta conter:\n" + "0 resposta indica que o dialogo será encerrado;\n" + "1 resposta indica que não haverá escolha e que este nodolo deve levar a outro;\n" + "2+ respostas indica que haverá escolha e cada escolha pode levar a um novo nodolo.")]
        public List<Resposta> respostas = new List<Resposta>();
    }

    [Serializable]
    public class Dialogo
    {
        public DialogoNodulo[] nodulos = new DialogoNodulo[0];

        public Dialogo Clone()
        {
            Dialogo dialogo = new Dialogo();

            dialogo.nodulos = new DialogoNodulo[nodulos.Length];

            for (int i = 0; i < dialogo.nodulos.Length; i++)
            {
                dialogo.nodulos[i] = new DialogoNodulo
                {
                    falas = new Fala[nodulos[i].falas.Length]
                };

                for (int j = 0; j < dialogo.nodulos[i].falas.Length; j++)
                {
                    dialogo.nodulos[i].falas[j] = new Fala
                    {
                        personagem = nodulos[i].falas[j].personagem,
                        emocao = nodulos[i].falas[j].emocao,
                        fala = nodulos[i].falas[j].fala
                    };
                }

                dialogo.nodulos[i].respostas = new List<Resposta>();
                dialogo.nodulos[i].respostas.Capacity = nodulos[i].respostas.Count;

                for (int j = 0; j < dialogo.nodulos[i].respostas.Capacity; j++)
                {
                    dialogo.nodulos[i].respostas.Add(new Resposta());

                    dialogo.nodulos[i].respostas[j] = new Resposta
                    {
                        personagem = nodulos[i].respostas[j].personagem,
                        emocao = nodulos[i].respostas[j].emocao,
                        resumo = nodulos[i].respostas[j].resumo,
                        fala = nodulos[i].respostas[j].fala,
                        conexao = nodulos[i].respostas [j].conexao,
                        questInfo = new QuestStruct
                        {
                            isQuest = nodulos[i].respostas[j].questInfo.isQuest,
                            questIndex = nodulos[i].respostas[j].questInfo.questIndex,
                            questDependencias = nodulos[i].respostas[j].questInfo.questDependencias
                        }
                    };
                }
            }

            return dialogo;
        }
    }

    [Serializable]
    public class Falador
    {
        public Sprite personagem;
        public string nome;

        static public Falador BuscarPolaroideNosAssets(Personagens _personagem, Expressao _emocao)
        {
            Falador personagem = new Falador();

            //string path = "Assets/Sprites/Personagem/Polaroides/";
            string path = "Polaroides/";
            string nome = "";
            string emocao = "";

            switch (_personagem)
            {
                case Personagens.Diretor:
                    nome = "Diretor";
                    personagem.nome = "Diretor";
                    break;
                case Personagens.Drica:
                    nome = "Drica";
                    personagem.nome = "Drica";
                    break;
                case Personagens.Lurdinha:
                    nome = "Lurdinha";
                    personagem.nome = "Lurdinha";
                    break;
                case Personagens.Jean:
                    nome = "Jean";
                    personagem.nome = "Jean";
                    break;
                case Personagens.Leitura:
                    nome = "Leitura";
                    personagem.nome = "Aluno";
                    break;
                case Personagens.MeioAmbiente:
                    nome = "MeioAmbiente";
                    personagem.nome = "Aluno";
                    break;
                case Personagens.Madá:
                    nome = "Mada";
                    personagem.nome = "Madá";
                    break;
                case Personagens.Antonia:
                    nome = "Antônia";
                    personagem.nome = "Antônia";
                    break;
                case Personagens.Alice:
                    nome = "Alice";
                    personagem.nome = "Alice";
                    break;
                case Personagens.Montanari:
                    nome = "MariaMontanari";
                    personagem.nome = "Maria Montanari";
                    break;
                case Personagens.Comenius:
                    nome = "Comenius";
                    personagem.nome = "Comenius";
                    break;
                case Personagens.AlunoTipo:
                    nome = "AlunoTipo";
                    personagem.nome = "Aluno";
                    break;
                case Personagens.Aluno:
                    nome = "Aluno";
                    personagem.nome = "Aluno";
                    break;
                case Personagens.Esquisito:
                    nome = "Esquisito";
                    personagem.nome = "Aluno";
                    break;
            }

            path = path + nome + "/";

            switch (_emocao)
            {
                case Expressao.Bravo:
                    emocao = "Bravo";
                    break;
                case Expressao.CaraFechada:
                    emocao = "CaraFechada";
                    break;
                case Expressao.Rindo:
                    emocao = "Rindo";
                    break;
                case Expressao.Serio:
                    emocao = "Serio";
                    break;
                case Expressao.Sorrindo:
                    emocao = "Sorrindo";
                    break;
            }

            path = path + emocao;

            Texture2D texture = Resources.Load(path) as Texture2D;

            try
            {
                personagem.personagem = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            catch(NullReferenceException)
            {
                Debug.LogWarning("Não foi encontrado sprite em " + path);
            }

            return personagem;
        }
    }
}
