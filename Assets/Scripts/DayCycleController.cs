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

    
    public AudioClip NightAmbienceClip;
    public AudioClip DayAmbienceClip;

    void Start()
    {
        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;
    }

    public void OnDayStart(int day)
    {
        Animator.SetTrigger("SetDay");
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
        GameManager.Player.PlayAmbience(DayAmbienceClip);
        Fireflies.SetActive(false);

        SetFog(false);
    }

    public void OnNightStart(int day)
    {
        Animator.SetTrigger("SetNight");
        moon.shadows = LightShadows.Soft;
        sun.shadows = LightShadows.None;
        GameManager.Player.PlayAmbience(NightAmbienceClip);
        Fireflies.SetActive(true);

        SetFog(true);
    }

    private void SetFog(bool value)
    {
        VolumeProfile profile = volumeProfile.sharedProfile;
        if (profile.TryGet<Fog>(out var fog))
        {
            Timer timer = Timer.CreateTimer(this.gameObject, 1.5f);
            timer.OnUpdate += (timeRemaining) =>
            {
                var min = value ? 25 : 100;
                var max = value ? 100 : 25;
                var attenuation = Mathf.Lerp(min, max, timeRemaining / timer.duration);
                fog.meanFreePath.value = attenuation;
            };
        }
    }
}