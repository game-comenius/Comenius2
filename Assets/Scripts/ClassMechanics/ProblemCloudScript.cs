using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProblemCloudScript : MonoBehaviour
{
    [SerializeField] private Image mainProblem;

    [SerializeField] private Sprite[] problemSprites = new Sprite[3];

    [SerializeField] private Button[] solutionButtons = new Button[3];

    [SerializeField] private Image[] solutionAnswer = new Image[3];

    [SerializeField] private Sprite[] rightWrong = new Sprite[2];

    [SerializeField] private float waitToDestroy = 1f;

    [SerializeField] private Vector2 scaleDelta;

    [SerializeField] private float scalingDuration = 0.5f;

    [HideInInspector] public float problemDuration;

    [HideInInspector] public int studentIndex;

    private void Awake()
    {
        InitializeProblem();

        ClassManager.EndClass += WrongSolution;
    }

    private void OnDestroy()
    {
        ClassManager.EndClass -= WrongSolution;
    }

    private void InitializeProblem()
    {
        int i = problemSprites.Length;

        while (i == problemSprites.Length)
        {
            i = Mathf.FloorToInt(Random.Range(0, problemSprites.Length));
        }

        mainProblem.sprite = problemSprites[i];

        for (int j = 0; j < problemSprites.Length; j++)
        {
            if (j == i)
            {
                solutionButtons[j].onClick.AddListener(() => RightSolution());
                solutionAnswer[j].sprite = rightWrong[0];
            }
            else
            {
                solutionButtons[j].onClick.AddListener(() => WrongSolution());
                solutionAnswer[j].sprite = rightWrong[1];
            }
        }

        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float t = 0;

        while (t < scalingDuration)
        {
            yield return null;

            // Mexer nos atributos deste objeto aqui para criar uma animação

            t += Time.deltaTime;
        }

        foreach (Button button in solutionButtons)
        {
            button.interactable = true;
        }
    }

    private void RightSolution()
    {
        Debug.Log("Parabens");
        Player.Instance.SolvedProblem();
        StartCoroutine(WaitToDestroy());
    }

    public void WrongSolution()
    {
        Debug.Log("Oops");

        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        ClassManager.classManager.RemoveCloud(this);

        foreach (Image image in solutionAnswer)
        {
            image.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(waitToDestroy);

        Destroy(gameObject);
    }
}
