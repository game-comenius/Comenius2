using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedGamesScrollView : MonoBehaviour {

    [SerializeField]
    private GameObject content;
    [SerializeField]
    private CreatedGameButton createdGameButtonPrefab;

    private List<CreatedGameButton> createdGameButtons;

    private void Awake()
    {
        createdGameButtons = new List<CreatedGameButton>();
    }

    private IEnumerator Start () {
        yield return StartCoroutine(CustomGameSettings.LoadAndUseAllSettings(AddCreatedGameButtons));

        StartCoroutine(AguardarSenhaELiberarExclusao());
    }

    public void AddCreatedGameButtons(CustomGameSettings[] gameSettingsArray)
    {
        if (createdGameButtons.Count > 0) createdGameButtons.Clear();

        for (int i = 0; i < gameSettingsArray.Length; i++)
            AddCreatedGameButton(gameSettingsArray[i], i);
    }

    public void AddCreatedGameButton(CustomGameSettings settings, int index)
    {
        var button = Instantiate(createdGameButtonPrefab, content.transform);
        button.transform.localScale = Vector3.one;
        button.Configure(settings, index);

        createdGameButtons.Add(button);
    }

    // Senha = del
    private IEnumerator AguardarSenhaELiberarExclusao()
    {
        bool senhaInserida = false;
        while (!senhaInserida)
        {
            // Aguardar letra d
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.D));

            string input = string.Empty;
            yield return new WaitForEndOfFrame();

            // Aguardar letra e, se o usuário digitar qualquer outra letra,
            // quebrar o loop e aguardar novamente a letra d
            yield return new WaitUntil(() =>
            {
                input = Input.inputString;
                return !string.IsNullOrEmpty(input);
            });
            if (input != "e" && input != "E") continue;

            input = string.Empty;
            yield return new WaitForEndOfFrame();

            // Aguardar letra l, se o usuário digitar qualquer outra letra,
            // quebrar o loop e aguardar novamente a letra d
            yield return new WaitUntil(() =>
            {
                input = Input.inputString;
                return !string.IsNullOrEmpty(input);
            });
            if (input != "l" && input != "L") continue;

            senhaInserida = true;
        }

        foreach (var button in createdGameButtons)
            button.DefinirVisibilidadeDoBotaoExcluir(true);
    }
}
