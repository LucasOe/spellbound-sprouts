using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    public GameManager GameManager;
    public Animator Animator;

    void Start()
    {
        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;
    }

    public void OnDayStart(int day)
    {
        Animator.SetTrigger("SetDay");
    }

    public void OnNightStart(int day)
    {
        Animator.SetTrigger("SetNight");
    }
}