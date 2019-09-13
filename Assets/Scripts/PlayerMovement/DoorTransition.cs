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
        if (QuestManager.GetQuestControl(questControlIndex))
        {
            Destroy(seta);
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada )
        {
            AudioSource asource = GetComponent<AudioSource>();
            asource.Play();
            StartCoroutine(CarregarProximaSala());
        }
    }

    private IEnumerator CarregarProximaSala()
    {
        sceneLoad = SceneManager.LoadSceneAsync(sceneName);
        sceneLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(timerMax);

        QuestManager.SetQuestControl(questControlIndex, true);
        sceneLoad.allowSceneActivation = true;
    }
}
