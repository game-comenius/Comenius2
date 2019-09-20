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

    protected override void Start()
    {
        base.Start();

        if (dialogoObrigatorio && Analise())  
        {
            GameManager.uiSendoUsada = true;
            StartCoroutine(DialogoObrigatorio());
        }
    }

    public void Restart()
    {
        if (dialogoObrigatorio && Analise())
        {
            GameManager.uiSendoUsada = true;
            StartCoroutine(DialogoObrigatorio());
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)
        {
            if (!QuestManager.GetQuestControl(questInfo.questIndex))
            {
                SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);
            }
            else if (dialogosSecundarios.Length > 0) 
            {
                int i = Random.Range(0, dialogosSecundarios.Length);

                SistemaDialogo.sistemaDialogo.dialogo = dialogosSecundarios[i];
                SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogosSecundarios[i], this);
            }
        }
    }

    private IEnumerator DialogoObrigatorio()
    {
        yield return new WaitForSeconds(esperaDialogoObrigatorio);

        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogoPrincipal, this);
    }

    public void SetQuestControl()
    {
        QuestManager.SetQuestControl(questInfo.questIndex, true);
        ReavaliarTodasQuests();
    }
}
