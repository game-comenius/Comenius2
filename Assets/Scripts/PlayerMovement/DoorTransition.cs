using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    [HideInInspector] public string sceneName;

    private AsyncOperation sceneLoad;

    private float timerMax = 1.5f;

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)
        {
            AudioSource asource = GetComponent<AudioSource>();
            GameManager.UISendoUsada();
            asource.Play();
            StartCoroutine(CarregarProximaSala());
        }
    }

    private IEnumerator CarregarProximaSala()
    {
        sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(timerMax);

        if (GetComponent<QuestScript>()) 
        {
            QuestManager.SetQuestControl(GetComponent<QuestScript>().questInfo.questIndex, true);
        }

        sceneLoad.allowSceneActivation = true;
        GameManager.UINaoSendoUsada();
    }
}
