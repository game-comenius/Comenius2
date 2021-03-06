﻿using System.Collections;
using UnityEngine;
using GameComenius.Dialogo;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(QuestGuest))]
[RequireComponent(typeof(DynamicCursor))]
public class NpcDialogo : MonoBehaviour
{
    private int _questIndex;
    public int questIndex
    {
        get
        {
            return _questIndex;
        }
    }

    public bool dialogoObrigatorio = false;

    public float esperaDialogoObrigatorio = 0f;

    public Dialogo dialogoPrincipal = new Dialogo();

    public Dialogo[] dialogosSecundarios = new Dialogo[0];

    public Vector3[] interactOffset = { Vector3.zero };

    // Sistema para executar outras funções após o término do diálogo
    // Por exemplo, o comenius aparece logo após o diálogo para falar algo
    // para o jogador
    // As funções que serão chamadas quando o diálogo terminar devem ser do
    // tipo Action, ou seja, "retorno void e nenhum parâmetro, ou seja, void ()"
    public event Action OnEndDialogueEvent;
    // As funções que serão chamadas quando o jogador escolher uma resposta
    // devem ser do tipo Action<int>, ou seja, "retorno void e 1 parâmetro do
    // tipo int, ou seja, void (int)" onde o int é o índice da resposta, por
    // exemplo... Índice 0 é a primeira resposta, o índice 1 é a segunda e etc.
    public event Action<int> QuandoEscolherRespostaEvent;

    private void Awake()
    {
        _questIndex = GetComponent<QuestGuest>().index;
    }

    private void Start()
    {
        Restart();
    }

    public void Restart() 
    {
        if (ManagerQuest.VerifyQuestIsAvailable(_questIndex))
        {
            if (dialogoObrigatorio && !ManagerQuest.VerifyQuestIsComplete(_questIndex)) 
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
    }

    public void OnMouseUp()
    {
        if (!GameManager.uiSendoUsada && !Player.Instance.GetComponent<PathFinder>().hasTarget)
        {
            Player.Instance.GetComponent<PathFinder>().hasTarget = true;

            StartCoroutine(Interact());
        }
    }

    public bool WantsToTalk
    {
        get
        {
            if (GetComponent<Collider2D>().enabled == false)
                return false;
            if (!ManagerQuest.VerifyQuestIsComplete(_questIndex) || _questIndex == 0 || dialogosSecundarios.Length > 0)
                return true;
            return false;
        }
    }

    public IEnumerator Interact()
    {
        yield return new WaitForEndOfFrame();

        if (!ManagerQuest.VerifyQuestIsComplete(_questIndex) || _questIndex == 0) //quest.index = 0 indica que não é quest
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

    public void OnEndDialogue()
    {
        if (OnEndDialogueEvent != null) OnEndDialogueEvent();
    }

    // Método que invoca as funções dentro do evento QuandoEscolherRespostaEvent
    public void QuandoEscolherResposta(int resposta)
    {
        if (QuandoEscolherRespostaEvent != null) QuandoEscolherRespostaEvent(resposta);
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

#region Editor
#if UNITY_EDITOR

//[CustomEditor(typeof(NpcDialogo))]
//public class NcpDialogoEditor : Editor
//{
//    private NpcDialogo instance = null;

//    private void OnEnable()
//    {
//        instance = target as NpcDialogo;
//    }

//    void OnSceneGUI()
//    {
//        Vector3 position = instance.transform.position;

//        for (int i = 0; i < instance.interactOffset.Length; i++)
//        {
//            instance.interactOffset[i] = Handles.PositionHandle(instance.interactOffset[i] + position, Quaternion.identity) - position;
//        }
//    }
//}

[CustomEditor(typeof(NpcDialogo))]
public class NcpDialogoEditor : Editor
{
    SerializedProperty interactOffset = null;

    private void OnEnable()
    {
        interactOffset = serializedObject.FindProperty("interactOffset");
    }

    void OnSceneGUI()
    {
        Vector3 position = (target as NpcDialogo).transform.position;

        for (int i = 0; i < interactOffset.arraySize; i++)
        {
            interactOffset.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(interactOffset.GetArrayElementAtIndex(i).vector3Value + position, Quaternion.identity) - position;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
#endregion