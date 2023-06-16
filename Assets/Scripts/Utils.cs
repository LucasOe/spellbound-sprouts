using UnityEngine;

public static class Utils
{
    // Get the distance between to GameObjects
    public static float GetDistance(GameObject target1, GameObject target2)
    {
        Vector3 delta = target2.transform.position - target1.transform.position;
        return delta.magnitude;
    }
}