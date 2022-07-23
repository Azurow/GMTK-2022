using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{

    public static AudioManagerScript instance;
    public AudioSource audioSource;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
