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
                // O que está comentado aqui é para testes, o correto é retornar
                // a propriedade CurrentSettings mesmo
                var cgs = new CustomGameSettings();
                cgs.Professor = CharacterName.Montanari;
                cgs.Sala = SalaDeAula.SalaDeCiencias;
                cgs.IntroducaoAula = "Introdução do custom";
                cgs.DescricaoMomento1 = "Descrição do momento 1";
                cgs.DescricaoMomento2 = "Descrição do momento 2";
                cgs.DescricaoMomento3 = "Descrição do momento 3";
                //cgs.ArrayMidiaPoderFeedbackPorMomento = new CreateCustomGamePanel.MidiaPoderFeedback[3][];
                //cgs.ArrayMidiaPoderFeedbackPorMomento[0] = new CreateCustomGamePanel.MidiaPoderFeedback[2];
                //cgs.ArrayMidiaPoderFeedbackPorMomento[0][0] = new CreateCustomGamePanel.MidiaPoderFeedback();
                //cgs.ArrayMidiaPoderFeedbackPorMomento[0][0].Midia = ItemName.Caderno;
                //cgs.ArrayMidiaPoderFeedbackPorMomento[0][1].Midia = ItemName.CameraPolaroid;
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
    public int NivelDeEnsino;
    public int AreaDeConhecimento;
    public string IntroducaoAula;
    public string DescricaoMomento1, DescricaoMomento2, DescricaoMomento3;
    public Procedimento Procedimento1, Procedimento2, Procedimento3;
    public Agrupamento Agrupamento1, Agrupamento2, Agrupamento3;
    //public CreateCustomGamePanel.MidiaPoderFeedback[][] ArrayMidiaPoderFeedbackPorMomento;
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
        //if (midiasDisponiveis != null) return midiasDisponiveis;

        //var a = ArrayMidiaPoderFeedbackPorMomento[0];
        //var quantidade = a.Length;
        //var disponiveis = new ItemName[quantidade];
        //for (int i = 0; i < quantidade; i++)
        //{
        //    disponiveis[i] = new ItemName();
        //    disponiveis[i] = a[i].Midia;
        //}
        //return midiasDisponiveis = disponiveis;
        return new ItemName[3] { ItemName.CameraPolaroid, ItemName.Caderno, ItemName.TVComVHS };
    }
}
