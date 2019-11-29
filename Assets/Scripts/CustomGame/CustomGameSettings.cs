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

    private ItemName[] midiasDisponiveis;

    // Dados da tela customizar que serão salvos no disco
    public CharacterName Professor;
    public NivelDeEnsino nivelDeEnsino;
    public AreaDeConhecimento areaDeConhecimento;
    public string introducaoAula;
    public string descricaoMomento1, descricaoMomento2, descricaoMomento3;
    public CreateCustomGamePanel.MidiaPoderFeedback[][] arrayMidiaPoderFeedbackPorMomento;
    public Procedimento Procedimento1, Procedimento2, Procedimento3;
    public Agrupamento Agrupamento1, Agrupamento2, Agrupamento3;


    public static CustomGameSettings LoadCustomGameSettings()
    {
        // Para economizar, existe uma variável que guarda a última leitura,
        // desse jeito o jogo só lê 1 vez o arquivo no disco
        if (currentSettings != null) return currentSettings;

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
        currentSettings =  ComeniusWebClient.RequestCustomGameSettings();

        return currentSettings;
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
