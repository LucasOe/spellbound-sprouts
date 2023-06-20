using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManger : MonoBehaviour
{
    public GameManager GameManager;
    public float cooldownTime = 60.0f;
    public Text timerText;
    private Timer timer;

    private void Start()
    {
        timer = Timer.CreateTimer(this, cooldownTime, () =>
        {
            ChangeTime(GameManager.Day);
        }, true);
    }

    private void Update()
    {
        if (timer.IsRunning)
        {
            timerText.text = timer.DisplayTime();
        }
    }

    private void ChangeTime(int day)
    {
        // Note: Currently the first day isn't counted, the first Action that
        // will be invoked is NightStart(0).
        if (GameManager.IsNight)
        {
            Debug.Log("Starting Day: " + day);
            GameManager.DayStart.Invoke(day);
            GameManager.IsNight = false;

            GameManager.Day += 1;
        }
        else
        {
            Debug.Log("Starting Night: " + day);
            GameManager.NightStart.Invoke(day);
            GameManager.IsNight = true;

        }
    }
}
