using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton Pattern
public class CustomGameLoadedSettings : MonoBehaviour {

    public static CustomGameLoadedSettings Instance { get; private set; }

    private CustomGameSettings settings;
    public static CustomGameSettings Settings
    {
        get
        {
            if (Instance == null) return null;
            return Instance.settings;
        }
    }

    private void Awake () {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            settings = CustomGameSettings.ReadCustomGameSettingsFromDisk();
        }
        else
        {
            Destroy(this.gameObject);
        }
	}
}