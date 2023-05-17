using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceBetween
{
    public GameObject target1;
    public GameObject target2;
    public float distanceX;
    public float distanceY;
    public float distanceZ;
    public float distanceTotal;

    public void GetDistance(GameObject target1, GameObject target2)
    {
        Vector3 delta = target2.transform.position - target1.transform.position;
        distanceX = delta.x;
        distanceY = delta.y;
        distanceZ = delta.z;
        distanceTotal = delta.magnitude;
    }
}
