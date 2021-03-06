﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(QuestGuest))]
public class Estante : MonoBehaviour
{
    private QuestGuest quest;

    [SerializeField]
    private GameObject modoEstanteAbertaUI;

    [SerializeField]
    private ItemName[] itensIniciais;

	private EstanteUI estanteUI;

    public List<ItemName> Items { get; private set; }

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

    private void Awake()
    {
        quest = GetComponent<QuestGuest>();

        Items = new List<ItemName>(itensIniciais);
    }

    private void Start()
    {
        estanteUI = modoEstanteAbertaUI.GetComponentInChildren<EstanteUI>();
        modoEstanteAbertaUI.SetActive(false);

        // Remover items da estante que o jogador já coletou
        var playerInventory = Player.Instance.Inventory;
        Items.RemoveAll(playerInventory.Contains);
    }

    public void Remove(ItemName item)
    {
        Items.Remove(item);
        estanteUI.DisplayItems();
    }

    private void OnMouseUpAsButton()
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

        ManagerQuest.QuestTakeStep(quest.index);
    }

    private void Interact()
    {
        GameObject.Find("Fade").GetComponent<FadeEffect>().Fadeout();
        modoEstanteAbertaUI.SetActive(true);
        estanteUI.DisplayItems();
        GameManager.UISendoUsada();
        
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

[CustomEditor(typeof(Estante))]
public class EstanteEditor : Editor
{
    SerializedProperty interactOffset = null;

    private void OnEnable()
    {
        interactOffset = serializedObject.FindProperty("interactOffset");
    }

    void OnSceneGUI()
    {
        Vector3 position = (target as Estante).transform.position;

        for (int i = 0; i < interactOffset.arraySize; i++)
        {
            interactOffset.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(interactOffset.GetArrayElementAtIndex(i).vector3Value + position, Quaternion.identity) - position;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
#endregion
