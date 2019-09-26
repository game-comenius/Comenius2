#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
class MyClass
{
    [ExecuteInEditMode]
    static MyClass()
    {
        EditorApplication.update += Update;
    }

    static void Update()
    {
        if (!EditorApplication.isPlaying)
        {
            bool isOpen = false;

            string questManagerSubpath = "";

            string subpath = "";

            string ioSubpath = "";

            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {

                if (EditorSceneManager.GetSceneAt(i).name == "ManagerScene")
                {
                    isOpen = true;

                    int j = EditorSceneManager.GetSceneAt(i).path.LastIndexOf("/");
                    questManagerSubpath = EditorSceneManager.GetSceneAt(i).path.ToString().Substring(0, j + 1); //j+1 para o / entrar no subpath
                }
                else
                {
                    int j = EditorSceneManager.GetSceneAt(i).path.LastIndexOf("/");
                    subpath = EditorSceneManager.GetSceneAt(i).path.ToString().Substring(0, j + 1); //j+1 para o / entrar no subpath

                    j = Application.dataPath.LastIndexOf("/");
                    ioSubpath = Application.dataPath.ToString().Substring(0, j + 1); //k+1 para o / entrar no subpath
                }
            }

            if (isOpen && questManagerSubpath != subpath) 
            {
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    if (EditorSceneManager.GetSceneAt(i).name == "ManagerScene")
                    {
                        EditorSceneManager.CloseScene(EditorSceneManager.GetSceneAt(i), true);
                    }
                }
            }
            else if (File.Exists(ioSubpath + subpath + "ManagerScene.unity"))
            {
                EditorSceneManager.OpenScene(subpath + "ManagerScene.unity", OpenSceneMode.Additive);
            }
        }
    }
}

#endif