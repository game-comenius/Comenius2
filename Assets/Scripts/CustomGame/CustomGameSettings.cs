using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class CustomGameSettings {

    private static CustomGameSettings currentSettings;

    // Dados da tela customizar que serão salvos no disco
    public CharacterName Professor;
    public string FalaProfessorSalaProfessores;

    public static CustomGameSettings ReadCustomGameSettingsFromDisk()
    {
        // Para economizar, existe uma variável que guarda a leitura do disco
        // O jogo só lê uma vez o arquivo no disco
        if (currentSettings != null) return currentSettings;

        // Criar objeto para receber informações lidas do disco
        CustomGameSettings settings = null;

        // Ler do disco
        // Para salvar no servidor web é preciso fazer outra coisa
        // Estamos salvando apenas quando o jogo é executado através do editor
        #if UNITY_EDITOR
        // if (!File.Exists("data_file.dat")) return null;
        using (FileStream fs = new FileStream("data-comenius.txt", FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            settings = (CustomGameSettings)formatter.Deserialize(fs);
        }
        #endif
         
        return currentSettings = settings;
    }

    public void SaveCustomGameSettingsToDisk()
    {
        // Para salvar no servidor web é preciso fazer outra coisa
        // Estamos salvando apenas quando o jogo é executado através do editor
        #if UNITY_EDITOR
        // Escrever no disco
        using (FileStream fs = new FileStream("data-comenius.txt", FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, this);
        }
        #endif
    }

}
