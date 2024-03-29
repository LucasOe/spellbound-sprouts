using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleController : MonoBehaviour
{
    public GameManager GameManager;
    public Animator Animator;
    public Light sun;
    public Light moon;
    public GameObject Fireflies;
    public Volume volumeProfile;
    public float DayFogDensity = 100;
    public float NightFogDensity = 25;

    public AudioSource AudioSource;
    //Necessary, because an AudioSource can only loop one file
    public AudioSource AudioAmbientSource;
    public AudioClip[] DayMusic;
    public AudioClip[] NightMusic;
    public AudioClip BossMusic;
    public AudioClip NightAmbienceClip;
    public AudioClip DayAmbienceClip;

    void Start()
    {
        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;

        PlayAmbience(DayAmbienceClip);
        PlayMusic();

        VolumeProfile profile = volumeProfile.sharedProfile;
        if (profile.TryGet<Fog>(out var fog))
        {
            fog.meanFreePath.value = DayFogDensity;
        }
    }

    public void OnDayStart(int day)
    {
        Animator.SetTrigger("SetDay");
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
        PlayAmbience(DayAmbienceClip);
        PlayMusic();
        Fireflies.SetActive(false);

        SetFog(false);
    }

    public void OnNightStart(int day)
    {
        Animator.SetTrigger("SetNight");
        moon.shadows = LightShadows.Soft;
        sun.shadows = LightShadows.None;
        PlayAmbience(NightAmbienceClip);
        PlayNightMusic(day);
        Fireflies.SetActive(true);

        SetFog(true);
    }

    private void SetFog(bool value)
    {
        VolumeProfile profile = volumeProfile.sharedProfile;
        if (profile.TryGet<Fog>(out var fog))
        {
            Timer timer = this.CreateTimer(1.5f);
            timer.RunOnUpdate((state) =>
            {
                var min = value ? NightFogDensity : DayFogDensity;
                var max = value ? DayFogDensity : NightFogDensity;
                var attenuation = Mathf.Lerp(min, max, state.RemainingDuration / state.Duration);
                fog.meanFreePath.value = attenuation;
            });
            timer.StartTimer();
        }
    }

    public void PlayAmbience(AudioClip audioclip)
    {
        AudioAmbientSource.Stop();
        AudioAmbientSource.clip = audioclip;
        AudioAmbientSource.loop = true;
        AudioAmbientSource.Play();
    }

    public void PlayMusic()
    {
        int i = UnityEngine.Random.Range(0, DayMusic.Length);
        AudioSource.clip = DayMusic[i];
        AudioSource.loop = true;
        AudioSource.Play();
    }

    public void PlayNightMusic(int day)
    {
        int i = UnityEngine.Random.Range(0, NightMusic.Length);
        AudioSource.clip = NightMusic[i];
        if (day == 5 || day == 10 || day == 20 || day == 30 || day == 35)
        {
            AudioSource.clip = BossMusic;
        }
        AudioSource.loop = true;
        AudioSource.Play();
    }


    public void StopAmbience()
    {
        AudioAmbientSource.Stop();
    }
}