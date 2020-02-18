using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void IrParaCena()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
