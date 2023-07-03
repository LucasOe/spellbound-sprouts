using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Get the distance between to GameObjects
    public static float GetDistance<T, U>(this T self, U target)
        where T : MonoBehaviour
        where U : MonoBehaviour
    {
        Vector3 delta = self.transform.position - target.transform.position;
        return delta.magnitude;
    }

    // Get the closest object to self
    public static U GetClosestObject<T, U>(this T self, List<U> objects)
        where T : MonoBehaviour
        where U : MonoBehaviour
    {
        U bestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (U potentialTarget in objects)
        {
            float distance = self.GetDistance(potentialTarget);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    // Foreach lambda helper
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action(item);
        }
    }

    public static void ForEach(this int count, Action action)
    {
        for (int i = 0; i < count; i++)
        {
            action();
        }
    }

    // Creates a new Timer that runs for duration in seconds.
    // 0 cycles runs the Timer once. -1 will run the timer indefinitely.
    public static Timer CreateTimer(this MonoBehaviour where, float _duration = 1.0f, int _cycles = 0)
    {
        Timer timer = where.gameObject.AddComponent<Timer>();
        timer.Setup(new TimerState
        {
            Duration = _duration,
            RemainingDuration = _duration,
            Cycles = _cycles,
            RemainingCycles = _cycles,
        });
        return timer;
    }
}