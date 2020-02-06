using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plan : MonoBehaviour
{
    [SerializeField] GameObject planejamentoUi;

    [SerializeField] private Button confirmarPlan;

    [SerializeField] [Range(0, 10)] float distFromPlayer;

    private Vector2 mousePosition;

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

    private void Start()
    {
        confirmarPlan.onClick.AddListener(() => GameManager.UINaoSendoUsada());
    }

    //private void OnMouseUp()
    //{
    //    if (!GameManager.uiSendoUsada)
    //    {
    //        planejamentoUi.SetActive(true);
    //        GameObject.Find("Fade").GetComponent<FadeEffect>().Fadeout();
    //        GameManager.UISendoUsada();
    //    }
    //}

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
        planejamentoUi.SetActive(true);
        GameObject.Find("Fade").GetComponent<FadeEffect>().Fadeout();
        GameManager.UISendoUsada();
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        for (int i = 0; i < interactOffset.Length; i++)
        {
            Gizmos.DrawSphere(transform.position + interactOffset[i], 0.07f);
        }
    }
#endif
}
