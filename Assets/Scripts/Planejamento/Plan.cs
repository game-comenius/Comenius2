using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Plan : MonoBehaviour
//{
//    [SerializeField] GameObject planejamentoUi;

//    private Vector2 mousePosition;

//    [Header("Gizmos")]

//    [SerializeField] bool drawClickableArea;

//    private void Update()
//    {
//        if (Input.GetMouseButtonUp(0)) 
//        {
//            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

//            //verificar se o mouse esta sobre o sprite e, se tiver, ativar o UI do planejamento

//            if ((Mathf.Abs(transform.position.x - mousePosition.x) < GetComponent<SpriteRenderer>().sprite.bounds.extents.x) && (Mathf.Abs(transform.position.y - mousePosition.y) < GetComponent<SpriteRenderer>().sprite.bounds.extents.y))
//            {
//                planejamentoUi.SetActive(true);
//            }
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        if (drawClickableArea)
//        {
//            Gizmos.color = new Color (Color.blue.r, Color.blue.g, Color.blue.b, 0.3f);

//            Gizmos.DrawCube(transform.position, 2 * GetComponent<SpriteRenderer>().sprite.bounds.extents);
//        }
//    }
//}


public class Plan : MonoBehaviour
{
    [SerializeField] GameObject planejamentoUi;

    [SerializeField] [Range(0, 10)] float distFromPlayer;

    private Vector2 mousePosition;

    [Header("Gizmos")]

    [SerializeField] bool drawClickableArea;

    [SerializeField] bool drawDistFromPlayer;

    private void Start()
    {
        StartCoroutine(VerifyClick());
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
        if (drawClickableArea)
        {
            Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.3f);

            Gizmos.DrawCube(transform.position, 2 * GetComponent<SpriteRenderer>().sprite.bounds.extents);
        }
        if (drawDistFromPlayer)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, distFromPlayer);
        }
    }
}
