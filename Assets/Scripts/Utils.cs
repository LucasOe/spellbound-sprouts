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
}