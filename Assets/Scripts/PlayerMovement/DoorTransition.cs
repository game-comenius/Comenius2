using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DynamicCursorForDoors))]
public class DoorTransition : MonoBehaviour
{
    [HideInInspector] public string sceneName;

    private AsyncOperation sceneLoad;

    private float timerMax = 1.5f;

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

    private void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada && !Player.Instance.GetComponent<PathFinder>().hasTarget)
        {
            Player.Instance.GetComponent<PathFinder>().hasTarget = true;

            StartCoroutine(MoveToInteract());
        }
    }

    private IEnumerator MoveToInteract()
    {
        yield return new WaitForEndOfFrame();

        if (!GameManager.uiSendoUsada)
        {
            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point, Interact);
        }
    }

    private void Interact()
    {
        AudioSource asource = GetComponent<AudioSource>();

        GameManager.UISendoUsada();

        asource.Play();

        StartCoroutine(CarregarProximaSala());
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

        //Cursor.SetCursor(GameObject.FindObjectOfType<CursorInfos>().cursorImage, GameObject.FindObjectOfType<CursorInfos>().hotspot, GameObject.FindObjectOfType<CursorInfos>().curmode);

        sceneLoad.allowSceneActivation = true;

        GameManager.UINaoSendoUsada();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        for (int i = 0; i < interactOffset.Length; i++)
        {
            Gizmos.DrawSphere(transform.position + interactOffset[i], 0.07f);
        }
    }
}