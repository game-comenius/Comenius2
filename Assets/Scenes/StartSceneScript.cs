using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    private bool sendoUsado = false;

    public void ComecarJogo()
    {
        if (!sendoUsado) 
        {
            StartCoroutine(InicializarManagerSceneEJogo());

            DontDestroyOnLoad(gameObject);

            sendoUsado = true;
        }
    }

    private IEnumerator InicializarManagerSceneEJogo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        yield return new WaitUntil(() => operation.isDone);

        operation = SceneManager.LoadSceneAsync(firstSceneName, LoadSceneMode.Additive);

        yield return new WaitUntil(() => operation.isDone);

        Scene scene = SceneManager.GetSceneByBuildIndex(1);

        SceneManager.UnloadSceneAsync(scene);

        Destroy(gameObject);
    }
}
