using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowed : StatusEffect
{
    private float strength;
    private GameObject particle;
    private GameObject particleInstance;

    public Slowed(Enemy enemy, float duration, float strength, GameObject particle) : base(enemy, duration)
    {
        this.strength = strength;
        this.particle = particle;

        particleInstance = GameObject.Instantiate(particle, new Vector3(0, 1, 0), Quaternion.identity);
        particleInstance.transform.SetParent(enemy.transform, false);
        enemy.Agent.speed *= 1.0f / strength;
    }

    protected override void OnUpdate()
    {

    }

    protected override void OnEnd()
    {
        GameObject.Destroy(particleInstance);
        enemy.Agent.speed *= strength;
    }
}
