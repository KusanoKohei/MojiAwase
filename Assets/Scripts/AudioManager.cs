using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public AudioSource audioSource;
    public AudioClip[] voiceClips;
    public AudioClip[] bgmClips;
    public AudioClip[] seClips;


    private void Start()
    {
        audioSource = GameObject.FindObjectOfType<AudioSource>();
    }

}
