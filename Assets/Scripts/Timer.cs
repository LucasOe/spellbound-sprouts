using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float duration;
    public float timeRemaining;
    public bool IsRepeating;
    public bool IsRunning;
    private Action callback;

    public static Timer CreateTimer(GameObject where, float duration, Action callback, bool repeat = false)
    {
        Timer timer = where.AddComponent<Timer>();
        timer.duration = duration;
        timer.timeRemaining = duration;
        timer.callback = callback;
        timer.IsRepeating = repeat;
        timer.IsRunning = true;
        return timer;
    }

    void Update()
    {
        if (IsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                if (!IsRepeating)
                {
                    timeRemaining = 0; // avoid values below zero
                    IsRunning = false;
                }
                else
                {
                    timeRemaining = duration;
                }
                callback();
            }
        }
    }

    public float GetMinutes()
    {
        return Mathf.FloorToInt(timeRemaining / 60);
    }

    public float GetSeconds()
    {
        return Mathf.FloorToInt(timeRemaining % 60);
    }

    public string DisplayTime()
    {
        return string.Format("{0:00}:{1:00}", GetMinutes(), GetSeconds());
    }
}
