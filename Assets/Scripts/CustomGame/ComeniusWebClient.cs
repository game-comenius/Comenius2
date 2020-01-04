using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// Classe de utilidades para estabelecer comunicações com o servidor web
public static class ComeniusWebClient {

    private static readonly string uploadURI = "http://gamecomenius.com/gamecomenius2/custom/savecustom.php";
    private static readonly string downloadURI = "http://gamecomenius.com/gamecomenius2/custom/loadcustom.php";

    public static IEnumerator RequestAllGames(List<CustomGameSettings> gameListToBePopulated)
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

            string converted = Encoding.Default.GetString(response, 0, response.Length);
            string[] separator = { "$$$" };
            var objsStrings = converted.Split(separator, StringSplitOptions.None);

            Debug.Log("HTTP response length = " + response.Length);
            Debug.Log("Quantidade de jogos criados = " + objsStrings.Length);
            Debug.Log("Bytes de um jogo criado = " + objsStrings[0].Length);

            var allGames = new CustomGameSettings[objsStrings.Length];
            for (int i = 0; i < allGames.Length; i++)
            {
                var objBytes = Encoding.Default.GetBytes(objsStrings[i]);
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
            gameListToBePopulated.Clear();
            gameListToBePopulated.AddRange(allGames);
        }
    }

    public static void PostGame(CustomGameSettings gameSettings)
    {
        var fieldName = "data";

        using (var stream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, gameSettings);

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection(fieldName, stream.ToArray()));

            UnityWebRequest webRequest = UnityWebRequest.Post(uploadURI, formData);
            webRequest.SendWebRequest();
            // ^
            // Verificar se o Post funcionou e impedir o save caso isso ocorra
        }
    }
}
