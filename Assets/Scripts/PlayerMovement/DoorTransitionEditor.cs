#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(DoorTransition))]
public class DoorTransitionEditor : UnityEditor.Editor
{
    private int sceneCount;

    private string[] scenesName;

    private int index;

    private int indexBk;

    private void OnEnable()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;

        index = sceneCount;

        indexBk = sceneCount;

        scenesName = new string[sceneCount + 1];

        for (int i = 0; i < sceneCount; i++)
        {
            scenesName[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        scenesName[sceneCount] = "-------------";
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DoorTransition myScript = (DoorTransition)target;

        if (SceneManager.sceneCountInBuildSettings + 1 != scenesName.Length)
        {
            OnEnable();
        }

        for (int i = 0; i < sceneCount; i++)
        {
            if (scenesName[i] == myScript.sceneName)
            {
                index = i;

                indexBk = i;
            }
        }

        index = EditorGUILayout.Popup("Nome Da Cena", index, scenesName);

        if (index != indexBk)
        {
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
        }

        myScript.sceneName = scenesName[index];

        if (index != sceneCount)
        {
            if (GUILayout.Button("Ir para a cena " + scenesName[index]))
            {
                UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(index), UnityEditor.SceneManagement.OpenSceneMode.Single);
            }
        }
    }
}

#endif