using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameComenius.Dialogo;

public class NpcDialogo : QuestScript
{
    [SerializeField] private bool dialogoObrigatorio = false;

    [SerializeField] private float esperaDialogoObrigatorio = 2f;

    [SerializeField] private Dialogo dialogoPrincipal = new Dialogo();

    [SerializeField] private Dialogo[] dialogosSecundarios = new Dialogo[1];


    protected override void Start()
    {
        base.Start();

        if (dialogoObrigatorio && !QuestManager.GetQuestControl(_questInfo.questIndex))  
        {
            GameManager.uiSendoUsada = true;
            StartCoroutine(DialogoObrigatorio());
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada)
        {
            if (!QuestManager.GetQuestControl(_questInfo.questIndex))
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
        QuestManager.SetQuestControl(_questInfo.questIndex, true);
        ReavaliarTodasQuests();
    }
}
