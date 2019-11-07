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
    static bool[,] vacancy;

    Vector2Int gridDim;

    SerializedProperty gridDimSP;

    bool open = true;

    private void OnEnable()
    {
        vacancy = (target as GridScript).vacancy;

        gridDimSP = serializedObject.FindProperty("gridDim");

        gridDim = gridDimSP.vector2IntValue;

        Load();

        (target as GridScript).vacancy = vacancy;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        GUILayout.Space(10);

        open = EditorGUILayout.Foldout(open, new GUIContent { text = "Tabela de Booleans" }, true, new GUIStyle(EditorStyles.foldout) { fontStyle = FontStyle.Bold });

        if (open)
        {
            if (vacancy != null)
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

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Reset", GUILayout.Width(45)))
                {
                    vacancy = new bool[gridDim.x, gridDim.y];
                }

                GUILayout.Label("", GUILayout.Width(15));

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = new Vector2Int(gridDim.x - 1, gridDim.y);
                }

                GUIStyle style = GUI.skin.button;

                style.fontSize = 10;

                GUILayout.Label("X", style, GUILayout.Width(20));

                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = new Vector2Int(gridDim.x + 1, gridDim.y);
                }

                GUILayout.Label("", GUILayout.Width(15));

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = new Vector2Int(gridDim.x, gridDim.y - 1);
                }

                GUILayout.Label("Y", style, GUILayout.Width(20));

                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    gridDimSP.vector2IntValue = new Vector2Int(gridDim.x, gridDim.y + 1);
                }

                GUILayout.Label("", GUILayout.Width(15));

                if (GUILayout.Button("Save", GUILayout.Width(45)))
                {
                    Save();
                }

                if (GUILayout.Button("Load", GUILayout.Width(45)))
                {
                    Load();
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);

            if (gridDim != gridDimSP.vector2IntValue)
            {
                gridDim = gridDimSP.vector2IntValue;

                vacancy = new bool[gridDim.x, gridDim.y];

                for (int j = 0; j < Mathf.Min((target as GridScript).vacancy.GetLength(1), vacancy.GetLength(1)); j++)
                {
                    for (int i = 0; i < Mathf.Min((target as GridScript).vacancy.GetLength(0), vacancy.GetLength(0)); i++)
                    {
                        vacancy[i, j] = (target as GridScript).vacancy[i, j];
                    }
                }
            }

            (target as GridScript).vacancy = vacancy;

            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

        if (serializedObject.FindProperty("save") != null)
        {
            EditorGUILayout.LabelField(serializedObject.FindProperty("save").stringValue);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void Save()
    {
        string path = (serializedObject.targetObject as GridScript).gameObject.scene.path;

        path = path.Substring(0, path.LastIndexOf('.'));

        string name = path.Substring(path.LastIndexOf('/') + 1);

        path = path.Substring(0, path.LastIndexOf('/'));

        string mission = path.Substring(path.LastIndexOf('/') + 1);

        path = path.Substring(0, path.LastIndexOf('/'));

        path = Application.streamingAssetsPath + "/Grids/" + mission + "/" + name + ".txt";

        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(path);

        bf.Serialize(file, vacancy);

        file.Close();

        SerializedProperty sp = serializedObject.FindProperty("save");

        sp.stringValue = path;

        serializedObject.ApplyModifiedProperties();
    }

    private void Load()
    {
        if (File.Exists(serializedObject.FindProperty("save").stringValue))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(serializedObject.FindProperty("save").stringValue, FileMode.Open);

            vacancy = bf.Deserialize(file) as bool[,];

            file.Close();
        }
    }

    //private static void Load(SerializedObject so)
    //{
    //    if (File.Exists(so.FindProperty("save").stringValue))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();

    //        FileStream file = File.Open(so.FindProperty("save").stringValue, FileMode.Open);

    //        vacancy = bf.Deserialize(file) as bool[,];

    //        file.Close();

    //        (so.targetObject as GridScript).vacancy = vacancy;
    //    }
    //}
}

#endif