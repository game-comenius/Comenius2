﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    private bool sendoUsado = false;

    public void ComecarJogo()
    {
        if (!sendoUsado) 
        {
            StartCoroutine(InicializarManagerSceneEJogo());

            transform.SetParent(null);

            DontDestroyOnLoad(gameObject);

            sendoUsado = true;
        }
    }

    private IEnumerator InicializarManagerSceneEJogo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Single);

        yield return new WaitUntil(() => operation.isDone);

        operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        yield return new WaitUntil(() => operation.isDone);

        Scene scene = SceneManager.GetSceneByBuildIndex(1);

        SceneManager.UnloadSceneAsync(scene);

        Destroy(gameObject);
    }
}
