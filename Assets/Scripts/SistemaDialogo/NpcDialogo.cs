using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;

public class NpcDialogo : QuestScript
{
    public bool dialogoObrigatorio = false;

    public float esperaDialogoObrigatorio = 2f;

    public Dialogo dialogoPrincipal = new Dialogo();

    public Dialogo[] dialogosSecundarios = new Dialogo[0];

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

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

            PathFinder.gotToInteractable += ComecarDialogoObrigatorio;

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point);
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
        if (!GameManager.uiSendoUsada && !Player.Instance.GetComponent<PathFinder>().hasTarget)
        {
            Player.Instance.GetComponent<PathFinder>().hasTarget = true;

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

            PathFinder.gotToInteractable += ComecarDialogoPrincipal;

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point);
        }
        else if (dialogosSecundarios.Length > 0)
        {
            Vector3[] point = new Vector3[interactOffset.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = transform.position + interactOffset[i];
            }

            Player.Instance.GetComponent<PathFinder>().NullifyGotToInteractable();

            PathFinder.gotToInteractable += ComecarDialogoSecundario;

            Player.Instance.GetComponent<PathFinder>().WalkToInteractable(point);
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

        int i = Random.Range(0, dialogosSecundarios.Length);

        SistemaDialogo.sistemaDialogo.dialogo = dialogosSecundarios[i];
        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogosSecundarios[i].Clone(), this);
    }

    public void SetQuestControl()
    {
        QuestManager.SetQuestControl(questInfo.questIndex, true);

        ReavaliarTodasQuests();
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
