using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProblemCloudScript : MonoBehaviour
{
    [SerializeField] Image mainProblem;

    [SerializeField] Sprite[] problemSprites = new Sprite[3];

    [SerializeField] Button[] solutionButtons = new Button[3];

    [HideInInspector] public float problemDuration;

    [HideInInspector] public int studentIndex;

    private void Awake()
    {
        InitializeProblem();
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
            }
            else
            {
                solutionButtons[j].onClick.AddListener(() => WrongSolution());
            }
        }

    }

    private void RightSolution()
    {
        Debug.Log("Parabens");

        ClassManager.classManager.RemoveCloud(this);

        Destroy(gameObject);
    }

    public void WrongSolution()
    {
        Debug.Log("Oops");

        ClassManager.classManager.RemoveCloud(this);

        Destroy(gameObject);
    }
}
