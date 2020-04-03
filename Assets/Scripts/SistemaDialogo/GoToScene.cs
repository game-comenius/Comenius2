using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    // Evento que quando disparado irá executar as Coroutines cadastradas nele
    // Será disparado antes da troca de cena, a troca de cena irá executar e
    // esperar pelo término das Coroutines cadastradas
    public delegate IEnumerator CoroutineFunction();
    public event CoroutineFunction AntesDeIrParaCenaEvent;
    
    public void IrParaCena() { StartCoroutine(LoadSceneAfterEvent()); }

    private IEnumerator LoadSceneAfterEvent()
    {
        // Se existem Coroutines cadastradas no evento, executar e esperar cada
        if (AntesDeIrParaCenaEvent != null)
        {
            foreach (var coroutine in AntesDeIrParaCenaEvent.GetInvocationList())
                yield return StartCoroutine((IEnumerator)coroutine.DynamicInvoke());
        }


        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
