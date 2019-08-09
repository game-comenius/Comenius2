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

    public override void OnInspectorGUI()
    {
        DoorTransition myScript = (DoorTransition)target;

        sceneCount = SceneManager.sceneCountInBuildSettings;

        scenesName = new string[0];

        scenesName = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++) 
        {
            scenesName[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        myScript.sceneIndex = EditorGUILayout.Popup("Nome Da Cena", myScript.sceneIndex , scenesName);

        if (GUILayout.Button("Usar Porta"))
        {
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(myScript.sceneIndex), UnityEditor.SceneManagement.OpenSceneMode.Single);
        }

    }
}

