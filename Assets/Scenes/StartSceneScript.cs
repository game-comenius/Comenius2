using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    public void ComecarJogo()
    {
        SceneManager.LoadScene(firstSceneName);
    }
}
