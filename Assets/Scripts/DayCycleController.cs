using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    public GameManager GameManager;

    [Range(0, 24)] public float timeOfDay;
    public LightingPreset Preset;
    public Light sun;
    public Light moon;

    void Start()
    {
        // Set day on start
        SetTime(12f);

        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;
    }

    public void OnDayStart(int day)
    {
        SetTime(12f);
    }

    public void OnNightStart(int day)
    {
        SetTime(0f);
    }

    public void SetTime(float time)
    {
        this.timeOfDay = time;
        float timePercent = timeOfDay / 24f;

        // Set sun and moon rotation
        float sunRotation = Mathf.Lerp(-90, 270, timePercent);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, -150f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -150f, 0);
    }
}