using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : StatusEffect
{
    private float damage;
    private GameObject particle;
    private GameObject particleInstance;

    public Poisoned(Enemy enemy, float duration, float damage, GameObject particle) : base(enemy, duration)
    {
        this.damage = damage;
        this.particle = particle;

        particleInstance = GameObject.Instantiate(particle, new Vector3(0, 1, 0), Quaternion.identity);
        particleInstance.transform.SetParent(enemy.transform, false);
    }

    protected override void OnUpdate()
    {
        enemy.Damage(damage * Time.deltaTime);
    }

    protected override void OnEnd()
    {
        GameObject.Destroy(particleInstance);
    }
}
