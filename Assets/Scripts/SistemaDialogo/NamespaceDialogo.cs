using System;
using UnityEngine;
using UnityEditor;

namespace GameComenius.Dialogo
{
    public enum Personagens
    {
        Lurdinha,
        Drica,
        Jean,
        Diretor
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
    }
    
    [Serializable]
    public class DialogoNodulo
    {
        public Fala[] falas = new Fala[0];

        [Tooltip("Resposta conter:\n" + "0 resposta indica que o dialogo será encerrado;\n" + "1 resposta indica que não haverá escolha e que este nodolo deve levar a outro;\n" + "2+ respostas indica que haverá escolha e cada escolha pode levar a um novo nodolo.")]
        public Resposta[] respostas = new Resposta[0];
    }

    [Serializable]
    public class Dialogo
    {
        public DialogoNodulo[] nodulos = new DialogoNodulo[0];
    }

    [Serializable]
    public class Falador
    {
        public Sprite personagem;
        public string nome;

        static public Falador BuscarPolaroideNosAssets(Personagens _personagem, Expressao _emocao)
        {
            Falador personagem = new Falador();

            string path = "Assets/Sprites/Personagem/Polaroides/";
            string nome = "";
            string emocao = "";

            switch (_personagem)
            {
                case Personagens.Diretor:
                    nome = "Diretor";
                    break;
                case Personagens.Drica:
                    nome = "Drica";
                    break;
                case Personagens.Lurdinha:
                    nome = "Lurdinha";
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

            path = path + emocao + ".png";

            UnityEngine.Object[] objects = AssetDatabase.LoadAllAssetsAtPath(path);

            //Debug.Log(objects.Length);

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

            return personagem;
        }
    }
}
