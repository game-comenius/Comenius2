using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Estante : QuestScript {

    [SerializeField]
    private GameObject modoEstanteAbertaUI;

	private EstanteUI estanteUI;

    public List<ItemName> Items { get; private set; }

    [SerializeField] private Vector3[] interactOffset = new Vector3[1];

    protected override void Awake()
    {
        base.Awake();

        Items = new List<ItemName>
        {
            // Itens que aparecem por padrão dentro da estante
            //ItemName.ReprodutorAudio,
            //ItemName.Gravador,
            //ItemName.CameraPolaroid,
            ItemName.TV,
            ItemName.Livro,
            ItemName.JornaisEResvistas
        };
    }

    protected override void Start()
    {
        base.Start();
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

        CompletarQuest();
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
        Vector3 position = (target as DoorTransition).transform.position;

        for (int i = 0; i < interactOffset.arraySize; i++)
        {
            interactOffset.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(interactOffset.GetArrayElementAtIndex(i).vector3Value + position, Quaternion.identity) - position;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
#endregion
