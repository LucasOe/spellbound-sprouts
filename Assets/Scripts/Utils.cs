using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Get the distance between to GameObjects
    public static float GetDistance(GameObject target1, GameObject target2)
    {
        Vector3 delta = target2.transform.position - target1.transform.position;
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
            float distance = GetDistance(self.gameObject, potentialTarget.gameObject);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }
}