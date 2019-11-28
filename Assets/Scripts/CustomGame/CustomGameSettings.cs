using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class CustomGameSettings {

    private static CustomGameSettings currentSettings;

    private ItemName[] midiasDisponiveis;

    private static readonly string uploadURI = "http://gamecomenius.com/gamecomenius2/savecustom.php";
    private static readonly string downloadURI = "http://gamecomenius.com/gamecomenius2/loadcustom.php";

    // Dados da tela customizar que serão salvos no disco
    public CharacterName Professor;
    public NivelDeEnsino nivelDeEnsino;
    public AreaDeConhecimento areaDeConhecimento;
    public string introducaoAula;
    public string descricaoMomento1, descricaoMomento2, descricaoMomento3;
    public CreateCustomGamePanel.MidiaPoderFeedback[][] arrayMidiaPoderFeedbackPorMomento;
    public Procedimento Procedimento1, Procedimento2, Procedimento3;
    public Agrupamento Agrupamento1, Agrupamento2, Agrupamento3;


    public static CustomGameSettings ReadCustomGameSettingsFromDisk()
    {
        // Para economizar, existe uma variável que guarda a leitura do disco
        // O jogo só lê uma vez o arquivo no disco
        if (currentSettings != null) return currentSettings;

        // Criar objeto para receber informações lidas do disco
        CustomGameSettings settings = null;

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
        var webRequest = UnityWebRequest.Get(downloadURI);
        webRequest.SendWebRequest();

        // Trocar isso por uma solução assíncrona
        while (!webRequest.isDone) { }

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log("Object download complete!");

            using (var stream = new MemoryStream())
            {
                var response = webRequest.downloadHandler.data;
                stream.Write(response, 0, response.Length);
                stream.Seek(0, SeekOrigin.Begin);

                var formatter = new BinaryFormatter();
                try
                {
                    var obj = formatter.Deserialize(stream);
                    settings = (CustomGameSettings)obj;
                }
                catch (SerializationException e)
                {
                    Debug.Log("Deserialization Failed: " + e.Message);
                }
            }
        }

        return currentSettings = settings;
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

        var a = arrayMidiaPoderFeedbackPorMomento[0];
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
