using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public string sceneName;
    float timer = 0.0f;
    float timerMax = 3.5f;
    bool comecarTimer = false;

    private void LateUpdate()
    {
        if (comecarTimer)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                LoadProximaSala();
            }
        }

    }

    public void OnMouseUp()
    {
        AudioSource asource = GetComponent<AudioSource>();
        asource.Play();
        comecarTimer = true;
    }


    public void LoadProximaSala()
    {
        SceneManager.LoadScene(sceneName);
    }
}
