using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(DoorTransition))]
public class DoorTransitionEditor : Editor
{
    private int sceneCount;

    private string[] scenesName;

    private int index;

    private int indexBk;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DoorTransition myScript = (DoorTransition)target;

        sceneCount = SceneManager.sceneCountInBuildSettings;

        index = sceneCount;

        indexBk = sceneCount;

        scenesName = new string[0];

        scenesName = new string[sceneCount + 1];

        for (int i = 0; i < sceneCount; i++)
        {
            scenesName[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            if (scenesName[i] == myScript.sceneName)
            {
                index = i;

                indexBk = i;
            }
        }

        scenesName[sceneCount] = "-------------";

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
