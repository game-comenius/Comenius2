using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private AudioSource _audioSource;
    
    void Awake () {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        instance._audioSource = GetComponent<AudioSource>();
        instance.GetComponent<AudioManager>().PlayMusic();
    }


    private void Start()
    {
        
    }

    public void PlayMusic()
    {
        if (instance._audioSource.isPlaying) return;
        instance._audioSource.Play();
    }

    
}
