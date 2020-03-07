using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(ItemSpriteDatabase))]
public class ItemSpriteDatabaseEditor : Editor {

    // Estão declarados aqui pra economizar processamento, são usados em
    // OnInspectorGUI(), que é executado frequentemente
    private ItemName oneItemName;
    private Sprite oneSprite;

    private SerializedProperty itemNameArrayProperty;
    private SerializedProperty spriteArrayProperty;

    // Campo para saber quantas linhas apresentadas no inspetor até o momento
    private int numberOfRowsDrawn;

    private void OnEnable()
    {
        var maxNumberOfItemsInDatabase = Enum.GetValues(typeof(ItemName)).Length;

        var db = (ItemSpriteDatabase)target;
        Array.Resize(ref db.itemNameArray, maxNumberOfItemsInDatabase);
        Array.Resize(ref db.spriteArray, maxNumberOfItemsInDatabase);

        itemNameArrayProperty = serializedObject.FindProperty("itemNameArray");
        spriteArrayProperty = serializedObject.FindProperty("spriteArray");
    }

    public override void OnInspectorGUI()
    {
        // Tirar o comentário da linha abaixo se quiser que o Unity Inspector
        // mostre os campos/propriedades do script ItemSpriteDatabase do jeito
        // padrão, ou seja, como se não estivéssemos reescrevendo este jeito
        //base.OnInspectorGUI();

        serializedObject.Update();

        numberOfRowsDrawn = 0;

        DrawCenteredHeader("Mídias de todas as missões");
        DrawRowsForTheseItems(GameManager.MidiasDisponiveisEmTodasAsMissoes);

        DrawCenteredHeader("Mídias exclusivas da missão 1");
        DrawRowsForTheseItems(GameManager.MidiasExclusivasDaMissao1);

        DrawCenteredHeader("Mídias exclusivas da missão 2");
        DrawRowsForTheseItems(GameManager.MidiasExclusivasDaMissao2);

        DrawCenteredHeader("Mídias exclusivas da missão 3");
        DrawRowsForTheseItems(GameManager.MidiasExclusivasDaMissao3);

        // Neste ponto do código, numberOfRowsDrawn deve ser igual ao
        // número total de mídias do jogo, i.e., GameManager.MidiasDoJogo.Length

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawCenteredHeader(string headerTitle)
    {
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(headerTitle);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void DrawRowsForTheseItems(ItemName[] itemsToDraw)
    {
        for (var i = 0; i < itemsToDraw.Length; i++)
        {
            oneItemName = itemsToDraw[i];

            EditorGUILayout.BeginHorizontal();
            itemNameArrayProperty.GetArrayElementAtIndex((int)oneItemName).enumValueIndex = (int)oneItemName;
            EditorGUILayout.LabelField(oneItemName.ToString());
            oneSprite = (Sprite)EditorGUILayout.ObjectField(spriteArrayProperty.GetArrayElementAtIndex((int)oneItemName).objectReferenceValue, typeof(Sprite), false);
            spriteArrayProperty.GetArrayElementAtIndex((int)oneItemName).objectReferenceValue = oneSprite;
            EditorGUILayout.EndHorizontal();

            numberOfRowsDrawn++;
        }
    }
}
