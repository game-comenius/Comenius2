using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

// Classe de utilidades para estabelecer comunicações com o servidor web
public static class ComeniusWebClient {

    private static readonly string uploadURI = "http://gamecomenius.com/gamecomenius2/savecustom.php";
    private static readonly string downloadURI = "http://gamecomenius.com/gamecomenius2/loadcustom.php";

    public static CustomGameSettings RequestCustomGameSettings()
    {
        var request = UnityWebRequest.Get(downloadURI);

        var success = SendWebRequest(request);

        if (success)
        {
            using (var stream = new MemoryStream())
            {
                var response = request.downloadHandler.data;
                stream.Write(response, 0, response.Length);
                stream.Seek(0, SeekOrigin.Begin);

                var formatter = new BinaryFormatter();
                try
                {
                    var settings = formatter.Deserialize(stream);
                    return (CustomGameSettings) settings;
                }
                catch (SerializationException e)
                {
                    Debug.Log("Deserialization Failed: " + e.Message);
                }
            }
        }
        // Falhou
        return null;
    }

    private static bool SendWebRequest(UnityWebRequest request)
    {
        var tries = 3;
        var timeout = 3000;

        request.SendWebRequest();

        for (int i = 1; i <= tries; i++)
        {
            if (request.isDone)
                return true;
            else
                Thread.Sleep(timeout);
        }

        // Se o código chegou até aqui, a requisição web falhou
        Debug.Log("UnityWebRequest falhou! Servidor não respondeu!");

        return false;
    }
}
