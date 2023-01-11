using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("cute");
    }

    public static void SoundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void SetMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }
}
