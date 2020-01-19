#if UNITY_EDITOR

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CustomEditor(typeof(GridScript))]
public class GridScriptEditor : Editor
{
    static bool[,] vacancy; //Guarda os valores de GridScript.vacancy para ser manipulados mais facilmente usando EditorGUILayout.Toggle 

    SerializedProperty vacancySP;

    SerializedProperty gridDimSP;

    bool open = true;

    private void OnEnable()
    {
        gridDimSP = serializedObject.FindProperty("gridDim");

        vacancySP = serializedObject.FindProperty("vacancy");

        vacancy = new bool[gridDimSP.vector2IntValue.x, gridDimSP.vector2IntValue.y];

        if (vacancy.GetLength(1) <= 32)
        {
            for (int i = 0; i < vacancy.GetLength(0); i++)
            {
                for (int j = 0; j < vacancy.GetLength(1); j++)
                {
                    vacancy[i, j] = (target as GridScript).IsOccupied(new Vector2Int(i, j));
                }
            }
        }
        else
        {
            Debug.Log("Grid grande demais");
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        GUILayout.Space(10);

        open = EditorGUILayout.Foldout(open, new GUIContent { text = "Tabela de Booleans" }, true, new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold });

        if (open)
        {
            //Desenha o grid no Inspector.
            if (vacancy != null && vacancy.GetLength(0) != 0 && vacancy.GetLength(1) != 0)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label(new GUIContent { text = "" }, GUILayout.Width(20));

                    for (int i = 0; i < vacancy.GetLength(1); i++)
                    {
                        GUILayout.Label(new GUIContent { text = (i + 1).ToString() }, GUILayout.Width(20));
                    }
                }
                EditorGUILayout.EndHorizontal();

                for (int j = 0; j < vacancy.GetLength(0); j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(new GUIContent { text = (j + 1).ToString() }, GUILayout.Width(20), GUILayout.Height(20));

                        for (int i = 0; i < vacancy.GetLength(1); i++)
                        {
                            vacancy[j, i] = EditorGUILayout.Toggle(vacancy[j, i], GUILayout.Width(20), GUILayout.Height(20));
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                GUILayout.Space(15);
            }
            else
            {
                GUILayout.Space(10);

                string text = "   A variável \"vacancy\" tem valor nulo.\n   Verifique se a variável \"gridDim\" ou \"Grid Dim\" tem\n   tanto \"X\" como \"Y\" maiores que zero e aperte o botão \"Reset\"";

                GUILayout.Label(new GUIContent { text = text });

                GUILayout.Space(5);
            }

            //Botões e funcionalidades no Inspector
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Reset", GUILayout.Width(45)))
                {
                    vacancy = new bool[gridDimSP.vector2IntValue.x, gridDimSP.vector2IntValue.y];
                }

                GUILayout.Label("", GUILayout.Width(15));

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = gridDimSP.vector2IntValue + Vector2Int.left;
                }

                GUIStyle style = GUI.skin.button;

                style.fontSize = 10;

                GUILayout.Label("X", style, GUILayout.Width(20));

                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = gridDimSP.vector2IntValue + Vector2Int.right;
                }

                GUILayout.Label("", GUILayout.Width(15));

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = gridDimSP.vector2IntValue + Vector2Int.down;
                }

                GUILayout.Label("Y", style, GUILayout.Width(20));

                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = gridDimSP.vector2IntValue + Vector2Int.up;
                }

                GUILayout.Label("", GUILayout.Width(15));
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);

            //Atualizações do grid
            if (vacancy != null)
            {
                if (new Vector2Int(vacancy.GetLength(0), vacancy.GetLength(1)) != gridDimSP.vector2IntValue)
                {
                    Vector2Int oldSize = new Vector2Int(vacancy.GetLength(0), vacancy.GetLength(1));

                    vacancy = new bool[gridDimSP.vector2IntValue.x, gridDimSP.vector2IntValue.y];

                    for (int j = 0; j < Mathf.Min(oldSize.y, vacancy.GetLength(1)); j++)
                    {
                        for (int i = 0; i < Mathf.Min(oldSize.x, vacancy.GetLength(0)); i++)
                        {
                            vacancy[i, j] = (target as GridScript).IsOccupied(new Vector2Int(i, j));
                        }
                    }
                }

                vacancySP.ClearArray();

                vacancySP.arraySize = vacancy.GetLength(0);

                if (vacancy.GetLength(1) <= 32)
                {
                    for (int i = 0; i < vacancy.GetLength(0); i++)
                    {
                        for (int j = 0; j < vacancy.GetLength(1); j++)
                        {
                            if (vacancy[i, j])
                            {
                                vacancySP.GetArrayElementAtIndex(i).intValue = (vacancySP.GetArrayElementAtIndex(i).intValue | (1 << j));
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Grid grande demais");
                }
            }

            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif