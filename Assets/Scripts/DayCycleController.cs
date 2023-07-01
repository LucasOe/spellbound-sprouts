using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    public GameManager GameManager;
    public Animator Animator;
    public Light sun;
    public Light moon;

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
    }

    public void OnNightStart(int day)
    {
        Animator.SetTrigger("SetNight");
        moon.shadows = LightShadows.Soft;
        sun.shadows = LightShadows.None;
    }
}