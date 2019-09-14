using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    [SerializeField] private bool isQuest;
    [SerializeField] private Vector2Int questControlIndex;

    [SerializeField] private GameObject seta;

    [HideInInspector] public string sceneName;

    private AsyncOperation sceneLoad;

    private float timerMax = 3.5f;

    private void Start()
    {
        if (isQuest && QuestManager.GetQuestControl(questControlIndex)) 
        {
            Destroy(seta);
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)
        {
            AudioSource asource = GetComponent<AudioSource>();
            GameManager.uiSendoUsada = true;
            asource.Play();
            StartCoroutine(CarregarProximaSala());
        }
    }

    private IEnumerator CarregarProximaSala()
    {
        sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(timerMax);
        if (isQuest)
        {
            QuestManager.SetQuestControl(questControlIndex, true);
        }

        sceneLoad.allowSceneActivation = true;
        GameManager.uiSendoUsada = false;
    }
}
