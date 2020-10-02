using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyBolt"))
        {
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Enemy"))
        {
            EnemyHealth EnemyHealth = other.GetComponent<EnemyHealth>();
            EnemyHealth.TakeDamage(40);
        }
    }
}
