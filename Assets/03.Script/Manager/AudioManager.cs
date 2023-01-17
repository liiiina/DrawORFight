using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip audioClip;
    public AudioSource musicSource;
    public static AudioClip fastClip;
    public static AudioClip healClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("cute");
        fastClip = Resources.Load<AudioClip>("get_fast");
        healClip = Resources.Load<AudioClip>("get_healitem");
    }

    public static void SoundPlay()
    {
        audioSource.PlayOneShot(audioClip);
    }
    public static void itemFast()
    {
        audioSource.PlayOneShot(fastClip);
    }
    public static void itemHeal()
    {
        audioSource.PlayOneShot(healClip);
    }
    public void SetMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }
}
