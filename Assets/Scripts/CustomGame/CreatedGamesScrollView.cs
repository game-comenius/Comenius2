using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedGamesScrollView : MonoBehaviour {

    [SerializeField]
    private GameObject content;
    [SerializeField]
    private CreatedGameButton createdGameButtonPrefab;

    // Use this for initialization
    void Start () {
        StartCoroutine(CustomGameSettings.LoadAndUseSettings(AddCreatedGameButton));
    }

    public void AddCreatedGameButton(CustomGameSettings settings)
    {
        var button = Instantiate(createdGameButtonPrefab, content.transform);
        button.transform.localScale = Vector3.one;
        button.Configure(settings);
    }
}
