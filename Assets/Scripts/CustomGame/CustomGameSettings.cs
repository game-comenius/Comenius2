using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class CustomGameSettings {

    private static CustomGameSettings currentSettings;

    private static readonly string uploadURI = "http://gamecomenius.com/gamecomenius2/savecustom.php";
    private static readonly string downloadURI = "http://gamecomenius.com/gamecomenius2/loadcustom.php";

    private ItemName[] midiasDisponiveis;

    // Dados da tela customizar que serão salvos no disco
    public CharacterName Professor;
    public int localDaAula;
    public int NivelDeEnsino;
    public int AreaDeConhecimento;
    public string IntroducaoAula;
    public string DescricaoMomento1, DescricaoMomento2, DescricaoMomento3;
    public Procedimento Procedimento1, Procedimento2, Procedimento3;
    public Agrupamento Agrupamento1, Agrupamento2, Agrupamento3;
    public CreateCustomGamePanel.MidiaPoderFeedback[][] ArrayMidiaPoderFeedbackPorMomento;
    public string TituloDaAula;
    public string Autor;


    // Deve ser usada sempre como uma Coroutine porque faz uma requisição web e
    // não seria legal travar o jogo enquanto esperamos por essa requisição
    // Como usar: StartCoroutine(CustomGameSettings.LoadAndUseSettings(...));
    // O argumento callbackAction é a função que será chamada quando o este
    // método (LoadAndUseSettings) conseguir as Settings do servidor web, ou
    // seja, este método passará as Settings como parâmetro para callbackAction
    public static IEnumerator LoadAndUseSettings(Action<CustomGameSettings> callbackAction)
    {
        // Para economizar, existe uma variável que guarda a última leitura,
        // desse jeito, o jogo só lê 1 vez o arquivo no disco
        // Então, se currentSettings existir, executar a action usando
        // currentSettings como parâmetro para ela
        if (currentSettings != null)
        {
            Debug.Log("Configurações do custom já estão carregadas na memória, não é necessário fazer um download");
            callbackAction(currentSettings);
        }
        else
        {
            // Ler do disco

            // Descomentar caso a versão online falhe por algum motivo
            //// Esta região é executada apenas quando apertamos o play pelo editor
            //#if UNITY_EDITOR
            //// if (!File.Exists("data_file.dat")) return null;
            //using (FileStream fs = new FileStream("data-comenius.txt", FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    settings = (CustomGameSettings)formatter.Deserialize(fs);
            //}
            //#endif

            // A partir daqui fica o código que deve ser executado quando o jogo
            // estiver no servidor online
            // currentSettings =  ComeniusWebClient.RequestCustomGameSettings();
            var request = UnityWebRequest.Get(downloadURI);

            // Timeout, aborta requisição se X segundos passarem
            request.timeout = 10;

            Debug.Log("Resquest created");
            Debug.Log("Resquest timeout = " + request.timeout);
            Debug.Log("Resquest redirect limit = " + request.redirectLimit);

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
                using (var stream = new MemoryStream())
                {
                    var response = request.downloadHandler.data;
                    stream.Write(response, 0, response.Length);
                    stream.Seek(0, SeekOrigin.Begin);

                    var formatter = new BinaryFormatter();
                    try
                    {
                        var obj = formatter.Deserialize(stream);
                        currentSettings = (CustomGameSettings)obj;
                        Debug.Log("Desserialização foi um sucesso!");
                        // Se a requisição e a desserialização funcionaram,
                        // executar a action passando settings como parâmetro
                        callbackAction(currentSettings);
                    }
                    catch (SerializationException e)
                    {
                        Debug.Log("Desserialização falhou: " + e.Message);
                    }
                }
            }
        }
    }


    public void SaveCustomGameSettingsToDisk()
    {
        // Descomentar caso a versão online falhe por algum motivo
        //// Esta região é executada apenas quando apertamos o play pelo editor
        //#if UNITY_EDITOR
        //// Escrever no disco
        //using (FileStream fs = new FileStream("data-comenius.txt", FileMode.Create))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    formatter.Serialize(fs, this);
        //}
        //#endif

        // A partir daqui fica o código que deve ser executado quando o jogo
        // estiver no servidor online
        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("data", stream.ToArray()));

            UnityWebRequest webRequest = UnityWebRequest.Post(uploadURI, formData);
            webRequest.SendWebRequest();
            // ^
            // Verificar se o Post funcionou e impedir o save caso isso ocorra
        }
    }

    public ItemName[] MidiasDisponiveis()
    {
        if (midiasDisponiveis != null) return midiasDisponiveis;

        var a = ArrayMidiaPoderFeedbackPorMomento[0];
        var quantidade = a.Length;
        var disponiveis = new ItemName[quantidade];
        for (int i = 0; i < quantidade; i++)
        {
            disponiveis[i] = new ItemName();
            disponiveis[i] = a[i].Midia;
        }
        return midiasDisponiveis = disponiveis;
    }
}
