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
    public static AudioClip alarmClip;
    public static AudioClip shotClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("cute");
        fastClip = Resources.Load<AudioClip>("get_fast");
        healClip = Resources.Load<AudioClip>("get_healitem");
        alarmClip = Resources.Load<AudioClip>("alarm");
        shotClip = Resources.Load<AudioClip>("shot");
    }

    public static void SoundPlay() //색칠 소리
    {
        audioSource.PlayOneShot(audioClip);
    }
    public static void itemFast() //달리기 소리
    {
        audioSource.PlayOneShot(fastClip);
    }
    public static void itemHeal() // 회복 소리
    {
        audioSource.PlayOneShot(healClip);
    }
    
    public static void alarmPlay() // 60초 남았을때 알람소리
    {
        audioSource.PlayOneShot(alarmClip);
    }
    public static void shotPlay()
    {
        audioSource.PlayOneShot(shotClip);
    }
    public void SetMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }
}
