using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNewScene(string newSceneName)
    {
        SceneManager.LoadSceneAsync(newSceneName);
    }
}