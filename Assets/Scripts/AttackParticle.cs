using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackParticle : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public ParticleSystem HitParticle;
    private Enemy targetEnemy;
    private float damage;

    public void Setup(Enemy targetEnemy, float damage)
    {
        this.targetEnemy = targetEnemy;
        this.damage = damage;
    }

    private void Update()
    {
        if (targetEnemy)
        {
            NavMeshAgent.SetDestination(targetEnemy.transform.position);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (targetEnemy)
        {
            if (ReferenceEquals(collision.gameObject, targetEnemy.gameObject))
            {
                Instantiate(HitParticle, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                targetEnemy.Damage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
