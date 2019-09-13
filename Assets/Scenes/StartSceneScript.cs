using System.Collections;
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

            sendoUsado = true;
        }
    }

    private IEnumerator InicializarManagerSceneEJogo()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);

        yield return new WaitUntil(() => operation.progress == 1);

        yield return null;

        SceneManager.LoadScene(firstSceneName);
    }
}
