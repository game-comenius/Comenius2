using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gerenciadorEfeitos : MonoBehaviour
{

    private static gerenciadorEfeitos instance = null;
    public static gerenciadorEfeitos Instance
    {
        get { return instance; }
    }

    private AudioSource fonteAudio;

    public void PlayMusic()
    {
        if (instance.fonteAudio.isPlaying) return;
        instance.fonteAudio.Play();
    }


}