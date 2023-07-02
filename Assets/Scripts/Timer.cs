using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float duration;
    public float timeRemaining;
    public int RepeatCount;
    private bool isRunning;
    private Action callback;
    public Action<float> OnUpdate;

    public static Timer CreateTimer(GameObject where, float duration, Action callback = null, int repeat = 0)
    {
        Timer timer = where.AddComponent<Timer>();
        timer.duration = duration;
        timer.timeRemaining = duration;
        timer.callback = callback;
        timer.RepeatCount = repeat;
        timer.isRunning = true;
        return timer;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                OnUpdate?.Invoke(timeRemaining);
            }
            else
            {
                SkipTimer();
            }
        }
    }

    // Skip remaining time and execute the callback
    public void SkipTimer()
    {
        if (RepeatCount <= 0)
        {
            Destroy(this);
        }
        else
        {
            timeRemaining = duration;
            RepeatCount--;
        }
        callback?.Invoke();
    }

    public float GetPercent()
    {
        return duration / timeRemaining;
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
