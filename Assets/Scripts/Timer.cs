using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TimerState
{
    public bool IsRunning;

    public float Duration;
    public float RemainingDuration;

    public int Cycles;
    public int RemainingCycles;
}

public class Timer : MonoBehaviour
{
    private TimerState TimerState;
    private Action<TimerState> OnFinish;
    private Action<TimerState> OnUpdate;

    // Creates a new Timer that runs for duration in seconds.
    // 0 cycles runs the Timer once. -1 will run the timer indefinitely.
    public void Setup(TimerState state)
    {
        this.TimerState = state;
    }

    void Update()
    {
        if (this.TimerState.IsRunning)
        {
            if (this.TimerState.RemainingDuration >= 0)
            {
                this.TimerState.RemainingDuration -= Time.deltaTime;
                OnUpdate?.Invoke(this.TimerState);
            }
            else
            {
                SkipTimer();
            }
        }
    }

    public void StartTimer()
    {
        this.TimerState.IsRunning = true;
    }

    public void StopTimer()
    {
        this.TimerState.IsRunning = false;
    }

    // Skip to the end of the current cycle
    public void SkipTimer()
    {
        if (this.TimerState.RemainingCycles == 0)
        {
            Destroy(this);
        }
        else
        {
            this.TimerState.RemainingDuration = this.TimerState.Duration;
            this.TimerState.RemainingCycles -= 1;
        }
        OnFinish?.Invoke(this.TimerState);
    }

    public void RunOnFinish(Action<TimerState> callback)
    {
        this.OnFinish += callback;
    }

    public void RunOnUpdate(Action<TimerState> callback)
    {
        this.OnUpdate += callback;
    }

    public float GetDuration()
    {
        return this.TimerState.Duration;
    }

    public float GetRemainingDuration()
    {
        return this.TimerState.RemainingDuration;
    }

    public float GetCycles()
    {
        return this.TimerState.Cycles;
    }

    public float GetRemainingCycles()
    {
        return this.TimerState.RemainingCycles;
    }

    public float GetPercent()
    {
        return this.TimerState.RemainingDuration / this.TimerState.Duration;
    }

    public float GetMinutes()
    {
        return Mathf.FloorToInt(this.TimerState.RemainingDuration / 60);
    }

    public float GetSeconds()
    {
        return Mathf.FloorToInt(this.TimerState.RemainingDuration % 60);
    }

    public string DisplayTime()
    {
        return TimerState.IsRunning ? string.Format("{0:00}:{1:00}", GetMinutes(), GetSeconds()) : "00:00";
    }
}
