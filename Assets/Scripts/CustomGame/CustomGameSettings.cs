using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class CustomGameSettings {

    // Esta variável estática guarda as configurações do jogo atual,
    // ela é atribuída quando o jogador seleciona um custom game para jogar
    private static CustomGameSettings currentSettings;
    public static CustomGameSettings CurrentSettings
    {
        get
        {
            if (currentSettings == null)
            {
                // De preferência, currentSettings nunca será igual a null em
                // circunstâncias normais e fluxo de jogo normal
                // O objeto cgs que está sendo criado aqui é para testes
                // Quando damos play diretamente de uma das 2 cenas do custom:
                // sala dos professores e sala de aula, currentSettings será
                // igual a null e precisaremos de um objeto de testes
                // Este será o nosso objeto com campos pré-determinados
                var cgs = new CustomGameSettings();
                cgs.TituloDaAula = "Aula Custom Teste";
                cgs.Autor = "Grupo Comenius";
                cgs.Professor = CharacterName.Montanari;
                cgs.Sala = SalaDeAula.SalaDeCiencias;
                cgs.IntroducaoAula = "Introdução do custom";
                cgs.DescricaoMomento1 = "Descrição do momento 1";
                cgs.DescricaoMomento2 = "Descrição do momento 2";
                cgs.DescricaoMomento3 = "Descrição do momento 3";
                cgs.Procedimento1 = Procedimento.DiscussaoEntreAlunos;
                cgs.Procedimento2 = Procedimento.Atividade;
                cgs.Procedimento3 = Procedimento.Pesquisa;
                cgs.Agrupamento1 = Agrupamento.Duplas;
                cgs.Agrupamento2 = Agrupamento.Individual;
                cgs.Agrupamento3 = Agrupamento.Grupos;
                var midiasSelecionadas = new List<ItemName> { ItemName.Caderno, ItemName.TVComVHS };
                var quantidadeMidias = midiasSelecionadas.Count;
                cgs.ArrayMidiaPoderFeedbackMomento1 = new CreateCustomGamePanel.MidiaPoderFeedback[quantidadeMidias];
                cgs.ArrayMidiaPoderFeedbackMomento2 = new CreateCustomGamePanel.MidiaPoderFeedback[quantidadeMidias];
                cgs.ArrayMidiaPoderFeedbackMomento3 = new CreateCustomGamePanel.MidiaPoderFeedback[quantidadeMidias];
                // Momento 1
                for (int i = 0; i < quantidadeMidias; i++)
                {
                    cgs.ArrayMidiaPoderFeedbackMomento1[i] = new CreateCustomGamePanel.MidiaPoderFeedback();
                    cgs.ArrayMidiaPoderFeedbackMomento1[i].Midia = midiasSelecionadas[i];
                    cgs.ArrayMidiaPoderFeedbackMomento1[i].Poder = Poder.MuitoBoa;
                    cgs.ArrayMidiaPoderFeedbackMomento1[i].Feedback = "Incrível!";
                }
                // Momento 2
                for (int i = 0; i < quantidadeMidias; i++)
                {
                    cgs.ArrayMidiaPoderFeedbackMomento2[i] = new CreateCustomGamePanel.MidiaPoderFeedback();
                    cgs.ArrayMidiaPoderFeedbackMomento2[i].Midia = midiasSelecionadas[i];
                    cgs.ArrayMidiaPoderFeedbackMomento2[i].Poder = Poder.MuitoBoa;
                    cgs.ArrayMidiaPoderFeedbackMomento2[i].Feedback = "Incrível 2!";
                }
                // Momento 3
                for (int i = 0; i < quantidadeMidias; i++)
                {
                    cgs.ArrayMidiaPoderFeedbackMomento3[i] = new CreateCustomGamePanel.MidiaPoderFeedback();
                    cgs.ArrayMidiaPoderFeedbackMomento3[i].Midia = midiasSelecionadas[i];
                    cgs.ArrayMidiaPoderFeedbackMomento3[i].Poder = Poder.MuitoBoa;
                    cgs.ArrayMidiaPoderFeedbackMomento3[i].Feedback = "Incrível 3!";
                }
                return cgs;
            }
            else
                return currentSettings;
        }
        set { currentSettings = value; }
    }

    private static readonly string uploadURI = "http://gamecomenius.com/gamecomenius2/custom/savecustom.php";
    private static readonly string downloadURI = "http://gamecomenius.com/gamecomenius2/custom/loadcustom.php";

    private ItemName[] midiasDisponiveis;

    // Dados da tela customizar que serão salvos no disco
    public CharacterName Professor;
    public SalaDeAula Sala;
    public int ValorNivelDeEnsino;
    public int ValorAreaDeConhecimento;
    public string IntroducaoAula;
    public string DescricaoMomento1, DescricaoMomento2, DescricaoMomento3;
    public Procedimento Procedimento1, Procedimento2, Procedimento3;
    public Agrupamento Agrupamento1, Agrupamento2, Agrupamento3;
    public CreateCustomGamePanel.MidiaPoderFeedback[] ArrayMidiaPoderFeedbackMomento1;
    public CreateCustomGamePanel.MidiaPoderFeedback[] ArrayMidiaPoderFeedbackMomento2;
    public CreateCustomGamePanel.MidiaPoderFeedback[] ArrayMidiaPoderFeedbackMomento3;
    public string TituloDaAula;
    public string Autor;

    // Deve ser usada sempre como uma Coroutine porque faz uma requisição web e
    // não seria legal travar o jogo enquanto esperamos por essa requisição
    // Como usar: StartCoroutine(CustomGameSettings.LoadAndUseAllSettings(...));
    // O argumento callbackAction é a função que será chamada quando o este
    // método (LoadAndUseSettings) conseguir as Settings do servidor web, ou
    // seja, este método passará as Settings como parâmetro para callbackAction
    public static IEnumerator LoadAndUseAllSettings(Action<CustomGameSettings[]> callbackAction)
    {
        var request = UnityWebRequest.Get(downloadURI);

        // Timeout, aborta requisição se X segundos passarem
        request.timeout = 10;
        Debug.Log("Resquest created - Timeout = " + request.timeout);

        // Enviar o HTTP Get e esperar pela resposta ou pelo erro
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network error: " + request.error);
        }
        else if (request.isHttpError)
        {
            Debug.Log("HTTP error: " + request.error);
        }
        else
        {
            var response = request.downloadHandler.data;
            // Se o servidor não tiver jogos para mostrar
            if (response.Length == 0) yield break;

            string converted = Encoding.ASCII.GetString(response, 0, response.Length);

            string[] separator = { "$$$" };

            var objsStrings = converted.Split(separator, StringSplitOptions.None);

            Debug.Log("HTTP response length = " + response.Length);
            Debug.Log("Quantidade de jogos criados = " + objsStrings.Length);
            Debug.Log("Bytes de um jogo criado = " + objsStrings[0].Length);

            var allGames = new CustomGameSettings[objsStrings.Length];
            for (int i = 0; i < allGames.Length; i++)
            {
                var objBytes = Encoding.ASCII.GetBytes(objsStrings[i]);
                using (var stream = new MemoryStream())
                {
                    stream.Write(objBytes, 0, objBytes.Length);
                    stream.Seek(0, SeekOrigin.Begin);

                    var formatter = new BinaryFormatter();
                    try
                    {
                        var obj = formatter.Deserialize(stream);
                        Debug.Log("Desserialização foi um sucesso!");
                        allGames[i] = (CustomGameSettings)obj;
                    }
                    catch (SerializationException e)
                    {
                        Debug.Log("Desserialização falhou: " + e.Message);
                    }
                }
            }
            // Se a requisição HTTP funcionou e a desserialização também,
            // chamar a callbackAction passada como argumento para esta função
            callbackAction(allGames);
        }
    }

    public void SaveToDisk()
    {
        var PostFieldName = "data";

        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection(PostFieldName, stream.ToArray()));

            UnityWebRequest webRequest = UnityWebRequest.Post(uploadURI, formData);
            webRequest.SendWebRequest();
            // ^
            // Verificar se o Post funcionou e impedir o save caso isso ocorra
        }
    }

    public ItemName[] MidiasDisponiveis()
    {
        if (midiasDisponiveis != null) return midiasDisponiveis;

        var a = ArrayMidiaPoderFeedbackMomento1;
        var quantidade = a.Length;
        var disponiveis = new ItemName[quantidade];
        for (int i = 0; i < quantidade; i++)
        {
            disponiveis[i] = new ItemName();
            disponiveis[i] = a[i].Midia;
        }
        return midiasDisponiveis = disponiveis;
    }

    public string ToHTMLString()
    {
        string s = "";

        s += "<h2>" + TituloDaAula + "</h2>";
        s += "Autor: " + Autor;
        s += "</br></br>";

        s += "Nível de ensino: " + NivelDeEnsino.Get(ValorNivelDeEnsino).nome;
        s += "</br>";
        s += "Área de conhecimento: " + AreaDeConhecimento.Get(ValorAreaDeConhecimento).nome;
        s += "</br></br>";

        s += "Professor(a) selecionado(a): " + Professor.NomeCompleto();
        s += "</br>";
        s += "Sala selecionada: " + Sala.NomeCompleto();
        s += "</br>";

        s += "<h3>== Introdução da Aula ==</h3>";
        s += IntroducaoAula;
        s += "</br>";

        s += "<h3>== Momento 1 da Aula ==</h3>";
        s += DescricaoMomento1;
        s += "</br></br>";
        s += "Procedimento: " + Procedimento1.Nome() + "   " + "Agrupamento: " + Agrupamento1.Nome();
        s += "</br>";

        s += "<h3>== Momento 2 da Aula ==</h3>";
        s += DescricaoMomento2;
        s += "</br></br>";
        s += "Procedimento: " + Procedimento2.Nome() + "   " + "Agrupamento: " + Agrupamento2.Nome();
        s += "</br>";

        s += "<h3>== Momento 3 da Aula ==</h3>";
        s += DescricaoMomento3;
        s += "</br></br>";
        s += "Procedimento: " + Procedimento3.Nome() + "   " + "Agrupamento: " + Agrupamento3.Nome();
        s += "</br>";

        s += "<h3>== Mídias disponíveis ==</h3>";
        s += "<ul>";
        foreach (var midia in MidiasDisponiveis())
            s += "<li>" + (new Item(midia).FriendlyName) + "</li>";
        s += "</ul>";

        return s;
    }
}
