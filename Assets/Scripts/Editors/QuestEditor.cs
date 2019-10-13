#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestScript))]
public class QuestEditor : Editor
{
    private QuestManager questManager = null;

    static private bool editorMode;

    private SerializedProperty sp_isQuest = null;
    private SerializedProperty sp_questIndex = null;
    private SerializedProperty sp_questDependencias = null;
    private SerializedProperty sp_questFeita = null;
    private SerializedProperty sp_dependenciasFeitas = null;
    private SerializedProperty sp_dependenciasNaoFeitas = null;


    private bool fazendoQuest = false;

    private QuestStruct questInfo = new QuestStruct();

    private int questDependSize = 0;

    static private bool questInfoFouldout = false;

    static private bool questDependenciasFoldout = false;

    GUIStyle style;

    Vector2 offset;

    private void OnEnable()
    {
        if (questManager == null)
        {
            int i = FindObjectsOfType<QuestManager>().Length;

            if (i == 1)
            {
                questManager = FindObjectOfType<QuestManager>();
                Debug.Log("QuestManager ok");
            }
            else if (i == 0)
            {
                Debug.Log("No QuestManager found.");
            }
            else
            {
                Debug.Log("More than one QuestManager found.");
            }
        }

        sp_isQuest = serializedObject.FindProperty("questInfo.isQuest");

        questInfo.isQuest = sp_isQuest.boolValue;

        sp_questIndex = serializedObject.FindProperty("questInfo.questIndex");

        questInfo.questIndex = sp_questIndex.vector2IntValue;

        sp_questDependencias = serializedObject.FindProperty("questInfo.questDependencias");

        questDependSize = sp_questDependencias.arraySize;

        questInfo.questDependencias = new Vector2Int[questDependSize];

        for (int i = 0; i < questInfo.questDependencias.Length; i++)
        {
            questInfo.questDependencias[i] = sp_questDependencias.GetArrayElementAtIndex(i).vector2IntValue;
        }

        sp_questFeita = serializedObject.FindProperty("questFeita");
        sp_dependenciasFeitas = serializedObject.FindProperty("dependenciasFeitas");
        sp_dependenciasNaoFeitas = serializedObject.FindProperty("dependenciasNaoFeitas");
    }

    public override void OnInspectorGUI()
    {
        editorMode = EditorGUILayout.ToggleLeft(new GUIContent { text = "Editor Mode" }, editorMode);

        if (editorMode)
        {
            serializedObject.Update();

            questInfoFouldout = EditorGUILayout.Foldout(questInfoFouldout, "Quest Info", true);

            if (questInfoFouldout)
            {
                style = new GUIStyle();

                style.contentOffset = new Vector2(10, 0);

                if (questInfo.isQuest)
                {
                    GUIContent content = new GUIContent
                    {
                        text = "Quest:\n", // + questManager.quests[questInfo.questIndex.x].descriptions[questInfo.questIndex.y],
                        image = null,
                        tooltip = "Quest Index é " + questInfo.questIndex
                    };

                    questManager.quests[questInfo.questIndex.x].descriptions[questInfo.questIndex.y] =
                        EditorGUILayout.TextField(content, questManager.quests[questInfo.questIndex.x].descriptions[questInfo.questIndex.y]);

                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button(new GUIContent { text = "Deletar", tooltip = "Faz com que esta deixe de ser uma quest e a remove do QuestManager." }))
                    {
                        questManager.quests[questInfo.questIndex.x].descriptions[questInfo.questIndex.y] = "";

                        questInfo.isQuest = false;

                        questInfo.questIndex = Vector2Int.zero;
                    }

                    EditorGUILayout.EndHorizontal();
                }
                else if (!fazendoQuest) 
                {
                    if (GUILayout.Button(new GUIContent { text = "Fazer Quest" }))
                    {
                        fazendoQuest = true;

                        QuestManager qm = FindObjectOfType<QuestManager>();

                        bool vagaEncontrada = false;

                        for (int i = 0; i < qm.quests.Length; i++)
                        {
                            for (int j = 0; j < qm.quests[i].descriptions.Length; j++)
                            {
                                if (qm.quests[i].descriptions[j] == "") 
                                {
                                    questInfo.questIndex = new Vector2Int(i, j);

                                    vagaEncontrada = true;

                                    j = qm.quests[i].descriptions.Length;

                                    i = qm.quests.Length;
                                }
                            }
                        }

                        if (!vagaEncontrada)
                        {
                            QuestManager.QuestDescription[] qd = new QuestManager.QuestDescription[qm.quests.Length];

                            //qd = qm.quests.Clone();
                        }
                    }
                }
                else
                {

                }

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("", GUILayout.Width(25));

                style = new GUIStyle(EditorStyles.foldout);
                style.fixedWidth = 400f;

                questDependenciasFoldout = EditorGUILayout.Foldout(questDependenciasFoldout, new GUIContent { text = "Dependencias" }, true, style);

                style = new GUIStyle(EditorStyles.numberField) { margin = new RectOffset(75, 0, 0, 0), alignment = TextAnchor.MiddleRight };

                questDependSize = EditorGUILayout.IntField(GUIContent.none, questDependSize, style, GUILayout.MinWidth(50f), GUILayout.MaxWidth(50f));

                if (questDependSize != questInfo.questDependencias.Length)
                {
                    ResizeDependencias();
                }

                EditorGUILayout.EndHorizontal();

                style = new GUIStyle { contentOffset = new Vector2(35, 0) };

                if (questDependenciasFoldout) 
                {
                    for (int i = 0; i < questInfo.questDependencias.Length; i++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.LabelField(new GUIContent { text = "Quest " + i }, style, GUILayout.MinWidth(70f), GUILayout.MaxWidth(70f));

                        GUILayout.Space(20);

                        questInfo.questDependencias[i] = EditorGUILayout.Vector2IntField(GUIContent.none, questInfo.questDependencias[i], GUILayout.MinWidth(200f), GUILayout.MaxWidth(200f));

                        EditorGUILayout.EndHorizontal();
                    }
                }

                EditorGUILayout.PropertyField(sp_questFeita, new GUIContent { text = "Quest Feita" });
                EditorGUILayout.PropertyField(sp_dependenciasFeitas, new GUIContent { text = "Dependencias Feitas" });
                EditorGUILayout.PropertyField(sp_dependenciasNaoFeitas, new GUIContent { text = "Dependencias Não Feitas" });
            }

            sp_isQuest.boolValue = questInfo.isQuest;

            sp_questIndex.vector2IntValue = questInfo.questIndex;

            sp_questDependencias.arraySize = questInfo.questDependencias.Length;

            for (int i = 0; i < questInfo.questDependencias.Length; i++)
            {
                sp_questDependencias.GetArrayElementAtIndex(i).vector2IntValue = questInfo.questDependencias[i];
            }

            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            base.OnInspectorGUI();
            UpdateValues();
        }
    }

    private void UpdateValues()
    {
        questInfo.isQuest = sp_isQuest.boolValue;

        questInfo.questIndex = sp_questIndex.vector2IntValue;

        questDependSize = sp_questDependencias.arraySize;

        questInfo.questDependencias = new Vector2Int[questDependSize];

        for (int i = 0; i < questInfo.questDependencias.Length; i++)
        {
            questInfo.questDependencias[i] = sp_questDependencias.GetArrayElementAtIndex(i).vector2IntValue;
        }
    }

    private void ResizeDependencias()
    {
        Vector2Int[] array = questInfo.questDependencias;

        questInfo.questDependencias = new Vector2Int[questDependSize];

        for (int i = 0; i < array.Length; i++)
        {
            questInfo.questDependencias[i] = array[i];
        }
    }
}
#endif