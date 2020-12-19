using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltDamage : MonoBehaviour
{

    public int damage;
    public ParticleSystem Explosion;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth EnemyHealth = other.GetComponent<EnemyHealth>();
            if (EnemyHealth)
            {
                EnemyHealth.TakeDamage(damage);
            }
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        else if(other.CompareTag("Boss"))
        {
            BossHealth BossHealth = other.GetComponent<BossHealth>();
            if (BossHealth)
            {
                BossHealth.TakeDamage(damage);
            }
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        else if(other.CompareTag("BossShield"))
        {
            BossShield health = other.GetComponent<BossShield>();
            if (health)
            {
                health.TakeDamage(damage);
            }
            if (Explosion != null)// its say if Explosion field in unity inspector in scripts IS not empty, so go on and if not, so skip this if.
            {
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        else return;
    }
}
