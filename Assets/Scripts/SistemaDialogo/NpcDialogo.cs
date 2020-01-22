using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;
using System;

[RequireComponent(typeof(DynamicCursor))]
public class NpcDialogo : QuestScript
{
    public bool dialogoObrigatorio = false;

    public float esperaDialogoObrigatorio = 0f;

    public Dialogo dialogoPrincipal = new Dialogo();

    public Dialogo[] dialogosSecundarios = new Dialogo[0];

    [SerializeField] private Vector3[] interactOffset = { Vector3.zero };


    // Sistema para executar outras funções após o término do diálogo
    // Por exemplo, o comenius aparece logo após o diálogo para falar algo
    // para o jogador
    // As funções que serão chamadas quando o diálogo terminar devem ser do
    // tipo Action, ou seja, "retorno void e nenhum parâmetro, i.e., void ()"
    public event Action OnEndDialogueEvent;


    protected override void Start()
    {
        base.Start();

        if (dialogoObrigatorio && Analise())  
        {
            GameManager.UISendoUsada();

            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point, ComecarDialogoObrigatorio);
        }
    }

    public void Restart() 
    {
        if (dialogoObrigatorio && Analise()) 
        {
            GameManager.UISendoUsada();
            StartCoroutine(DialogoObrigatorio());
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)// && !Player.Instance.GetComponent<PathFinder>().hasTarget)
        {
            //Player.Instance.GetComponent<PathFinder>().hasTarget = true;

            StartCoroutine(Test());
        }
    }

    private IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();

        if (!QuestManager.GetQuestControl(questInfo.questIndex) || !questInfo.isQuest)
        {
            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point, ComecarDialogoPrincipal);
        }
        else if (dialogosSecundarios.Length > 0)
        {
            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point, ComecarDialogoSecundario);
        }
    }

    private void ComecarDialogoObrigatorio()
    {
        StartCoroutine(DialogoObrigatorio());
    }

    private IEnumerator DialogoObrigatorio()
    {
        yield return new WaitForSeconds(esperaDialogoObrigatorio);

        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);
    }

    private void ComecarDialogoPrincipal()
    {
        GameManager.UISendoUsada();

        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal.Clone(), this);
    }

    private void ComecarDialogoSecundario()
    {
        GameManager.UISendoUsada();

        int i = UnityEngine.Random.Range(0, dialogosSecundarios.Length);

        SistemaDialogo.sistemaDialogo.dialogo = dialogosSecundarios[i];
        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogosSecundarios[i].Clone(), this);
    }

    public void SetQuestControl()
    {
        QuestManager.SetQuestControl(questInfo.questIndex, true);

        ReavaliarTodasQuests();
    }

    public void OnEndDialogue()
    {
        if (OnEndDialogueEvent != null) OnEndDialogueEvent();
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
