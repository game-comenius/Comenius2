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

    [Header("Gizmos")]

    [SerializeField] bool drawDistFromPlayer;

    private void Start()
    {
        confirmarPlan.onClick.AddListener(() => GameManager.UINaoSendoUsada());

        //StartCoroutine(VerifyClick());
    }


    private void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)
        {
            planejamentoUi.SetActive(true);
            GameObject.Find("Fade").GetComponent<FadeEffect>().Fadeout();
            GameManager.UISendoUsada();
        }
    }

    private IEnumerator VerifyClick()
    {
        while(true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //verificar se o mouse esta sobre o sprite e, se tiver, comecar corrotina

                if ((Mathf.Abs(transform.position.x - mousePosition.x) < GetComponent<SpriteRenderer>().sprite.bounds.extents.x) && (Mathf.Abs(transform.position.y - mousePosition.y) < GetComponent<SpriteRenderer>().sprite.bounds.extents.y))
                {
                    StartCoroutine(VerifyDistance());

                    yield break;
                }
            }

            yield return null;
        }
    }

    private IEnumerator VerifyDistance()
    {
        while(true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //verificar se o mouse esta fora do sprite e, se tiver, voltar para corrotine VerifyClick()

                if ((Mathf.Abs(transform.position.x - mousePosition.x) > GetComponent<SpriteRenderer>().sprite.bounds.extents.x) || (Mathf.Abs(transform.position.y - mousePosition.y) > GetComponent<SpriteRenderer>().sprite.bounds.extents.y))
                {
                    StartCoroutine(VerifyClick());

                    yield break;
                }
            }

            if ((Player.Instance.transform.position - transform.position).magnitude < distFromPlayer)
            {
                planejamentoUi.SetActive(true);

                StartCoroutine(VerifyPlanUI());

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator VerifyPlanUI()
    {
        while (planejamentoUi.activeSelf) 
        {
            yield return null;
        }

        StartCoroutine(VerifyClick());
    }

    private void OnDrawGizmos()
    {
        if (drawDistFromPlayer)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, distFromPlayer);
        }
    }
}
