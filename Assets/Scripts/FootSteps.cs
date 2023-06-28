using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] Clips;
    public AudioSource AudioSource;
    public float Volume = 1.0f;

    private void StepEvent()
    {
        AudioClip clip = GetRandomClip();
        AudioSource.PlayOneShot(clip, Volume);
    }

    private AudioClip GetRandomClip()
    {
        return Clips[UnityEngine.Random.Range(0, Clips.Length)];
    }
}
