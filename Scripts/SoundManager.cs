using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip GetClip;
    public AudioClip PunchClip;
    public AudioClip SledClip;
    public AudioClip BoomClip;

    public static SoundManager instance;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    public void PlayGetSound()
    {
        audioSource.PlayOneShot(GetClip);
    }

    public void PlayPunchSound()
    {
        audioSource.PlayOneShot(PunchClip);
    }

    public void PlaySledSound()
    {
        audioSource.PlayOneShot(SledClip);
    }

    public void PlayBoomSound()
    {
        audioSource.PlayOneShot(BoomClip);
    }
}
